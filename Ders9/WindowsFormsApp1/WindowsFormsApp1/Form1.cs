using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
   

    public partial class Form1 : Form
    {
       
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateBoard();
            PlacePieces();
        }

        private Panel[,] squares = new Panel[8, 8];
        private Label[,] pieces = new Label[8, 8];

        private Label selectedPiece = null;
        private int selectedRow = -1;
        private int selectedCol = -1;

        private bool isBlackTurn = true;
        private int blackScore = 0;
        private int whiteScore = 0;
      
      
        
        
        private void CreateBoard()
        {
            int tileSize = 70;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Panel panel = new Panel();
                    panel.Size = new Size(tileSize, tileSize);
                    panel.Location = new Point(col * tileSize, row * tileSize);
                    panel.BackColor = (row + col) % 2 == 0 ? Color.Beige : Color.SaddleBrown;
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    panel.Click += Panel_Click;

                    this.Controls.Add(panel);
                    squares[row, col] = panel;
                }
            }

            this.ClientSize = new Size(8 * tileSize, 8 * tileSize);
         
            
            this.Height = 8 * 70 + 80;
        }

        private void PlacePieces()
        {
            
            for (int row = 1; row <= 2; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    CreatePiece(row, col, "black");
                }
            }

           
            for (int row = 5; row <= 6; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    CreatePiece(row, col, "white");
                }
            }
        }

        private void CreatePiece(int row, int col, string color)
        {
            Label piece = new Label();
            piece.Text = color == "black" ? "●" : "○";

            if (color == "black")
            {
                piece.ForeColor = Color.Black;
            }
            else
            {
                piece.ForeColor = Color.DarkRed;
            }

            piece.Font = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
            piece.TextAlign = ContentAlignment.MiddleCenter;
            piece.Dock = DockStyle.Fill;
            piece.Click += Piece_Click;

            squares[row, col].Controls.Add(piece);
            pieces[row, col] = piece;
        }

        private void Piece_Click(object sender, EventArgs e)
        {
            Label clickedPiece = sender as Label;
            if (clickedPiece == null)
                return;

            bool isBlackPiece = clickedPiece.Text == "●" || clickedPiece.Text == "♛";

            if (isBlackTurn != isBlackPiece)
                return;

            if (selectedPiece != null)
                selectedPiece.BackColor = Color.Transparent;

            selectedPiece = clickedPiece;

            var pos = FindPiecePosition(selectedPiece);
            selectedRow = pos.row;
            selectedCol = pos.col;

            selectedPiece.BackColor = Color.Yellow;
        }

        
        private void Panel_Click(object sender, EventArgs e)
        {
            if (selectedPiece == null)
                return;

            Panel targetPanel = sender as Panel;
            var targetPos = FindPanelPosition(targetPanel);
            int targetRow = targetPos.row;
            int targetCol = targetPos.col;

            if (!IsValidMove(selectedRow, selectedCol, targetRow, targetCol))
                return;

            string pieceText = selectedPiece.Text;
            bool isQueen = IsQueen(pieceText);

            bool captured = false;

            if (isQueen)
            {
                RemoveCapturedPieces(selectedRow, selectedCol, targetRow, targetCol);
                captured = true; 
            }
            else
            {
                int rowDiff = targetRow - selectedRow;
                int colDiff = targetCol - selectedCol;

                if (Math.Abs(rowDiff) == 2 || Math.Abs(colDiff) == 2)
                {
                    int middleRow = (selectedRow + targetRow) / 2;
                    int middleCol = (selectedCol + targetCol) / 2;

                    if (pieces[middleRow, middleCol] != null)
                    {
                        squares[middleRow, middleCol].Controls.Remove(pieces[middleRow, middleCol]);
                        pieces[middleRow, middleCol] = null;

                        if (isBlackTurn)
                            blackScore++;
                        else
                            whiteScore++;

                        CheckWin();
                        captured = true;
                    }
                }
            }

            
            squares[targetRow, targetCol].Controls.Add(selectedPiece);
            pieces[targetRow, targetCol] = selectedPiece;
            pieces[selectedRow, selectedCol] = null;

            selectedPiece.BackColor = Color.Transparent;

            if (captured && CanCaptureMore(targetRow, targetCol))
            {
                selectedPiece = pieces[targetRow, targetCol];
                selectedRow = targetRow;
                selectedCol = targetCol;
                selectedPiece.BackColor = Color.Yellow;
            }
            else
            {
                selectedPiece = null;
                isBlackTurn = !isBlackTurn;
               
            }

            UpdateAdvancedPieces();
        }


        private void UpdateAdvancedPieces()
        {
            
            for (int col = 0; col < 8; col++)
            {
                if (pieces[7, col] != null && pieces[7, col].Text == "●")
                {
                    pieces[7, col].Text = "♛";
                }
            }

          
            for (int col = 0; col < 8; col++)
            {
                if (pieces[0, col] != null && pieces[0, col].Text == "○")
                {
                    pieces[0, col].Text = "♕";
                }
            }
        }
        private bool IsQueen(string text)
        {
            return text == "♕" || text == "♛";
        }

        private bool IsQueenPathClear(int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowStep = Math.Sign(toRow - fromRow);
            int colStep = Math.Sign(toCol - fromCol);

            int r = fromRow + rowStep;
            int c = fromCol + colStep;

            while (r != toRow || c != toCol)
            {
                if (pieces[r, c] != null)
                    return false; 
                r += rowStep != 0 ? rowStep : 0;
                c += colStep != 0 ? colStep : 0;
            }
            return true;
        }

        private void RemoveCapturedPieces(int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowStep = Math.Sign(toRow - fromRow);
            int colStep = Math.Sign(toCol - fromCol);

            int r = fromRow + rowStep;
            int c = fromCol + colStep;

            while (r != toRow || c != toCol)
            {
                if (pieces[r, c] != null)
                {
                    bool enemyPiece = (isBlackTurn && (pieces[r, c].Text == "○" || pieces[r, c].Text == "♕"))
                                    || (!isBlackTurn && (pieces[r, c].Text == "●" || pieces[r, c].Text == "♛"));

                    if (enemyPiece)
                    {
                        squares[r, c].Controls.Remove(pieces[r, c]);
                        pieces[r, c] = null;

                        if (isBlackTurn) blackScore++;
                        else whiteScore++;

                        CheckWin();
                    }
                    else
                    {
                        
                        break;
                    }
                }

                r += rowStep;
                c += colStep;
            }
        }


        private bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            if (toRow < 0 || toRow > 7 || toCol < 0 || toCol > 7)
                return false;

            if (pieces[toRow, toCol] != null)
            {
                string pieceAtTarget = pieces[toRow, toCol].Text;
                if ((isBlackTurn && (pieceAtTarget == "●" || pieceAtTarget == "♛")) ||
                    (!isBlackTurn && (pieceAtTarget == "○" || pieceAtTarget == "♕")))
                    return false;
            }

            string currentText = selectedPiece.Text;

            bool isWhiteQueen = currentText == "♕";
            bool isBlackQueen = currentText == "♛";

            int rowDiff = toRow - fromRow;
            int colDiff = toCol - fromCol;

            int absRow = Math.Abs(rowDiff);
            int absCol = Math.Abs(colDiff);

           
            if (isWhiteQueen || isBlackQueen)
            {
                if (absRow != 0 && absCol != 0)
                    return false; 


                int rowStep = (absRow == 0) ? 0 : rowDiff / absRow;
                int colStep = (absCol == 0) ? 0 : colDiff / absCol;

                int r = fromRow + rowStep;
                int c = fromCol + colStep;

                while (r != toRow || c != toCol)
                {
                    if (pieces[r, c] != null)
                    {
                        
                        bool isEnemyPiece = (isBlackTurn && (pieces[r, c].Text == "○" || pieces[r, c].Text == "♕")) ||
                                            (!isBlackTurn && (pieces[r, c].Text == "●" || pieces[r, c].Text == "♛"));
                        if (isEnemyPiece)
                        {
                           
                            return true;
                        }
                        return false; 
                    }
                    r += rowStep;
                    c += colStep;
                }

                return true; 
            }

            
            if (pieces[toRow, toCol] != null)
                return false;

          
            if (isBlackTurn && rowDiff < 0) return false;
            if (!isBlackTurn && rowDiff > 0) return false;

            
            if ((absRow == 1 && colDiff == 0) || (rowDiff == 0 && absCol == 1))
                return true;

           
            if ((absRow == 2 && colDiff == 0) || (rowDiff == 0 && absCol == 2))
            {
                int middleRow = (fromRow + toRow) / 2;
                int middleCol = (fromCol + toCol) / 2;

                if (pieces[middleRow, middleCol] != null)
                {
                    string middlePiece = pieces[middleRow, middleCol].Text;
                    if ((isBlackTurn && (middlePiece == "○" || middlePiece == "♕")) ||
                        (!isBlackTurn && (middlePiece == "●" || middlePiece == "♛")))
                        return true;
                }
            }

            return false;
        }

        private (int row, int col) FindPiecePosition(Label piece)
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    if (pieces[row, col] == piece)
                        return (row, col);
            return (-1, -1);
        }

        private (int row, int col) FindPanelPosition(Panel panel)
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    if (squares[row, col] == panel)
                        return (row, col);
            return (-1, -1);
        }

        private void CheckWin()
        {
            int blackCount = 0;
            int whiteCount = 0;

            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    if (pieces[row, col] != null)
                    {
                        if (pieces[row, col].Text == "●" || pieces[row, col].Text == "♛") blackCount++;
                        else if (pieces[row, col].Text == "○" || pieces[row, col].Text == "♕") whiteCount++;
                    }

            if (blackCount == 0)
            {
                MessageBox.Show("Beyaz Oyuncu Kazandı! Skor: " + whiteScore, "Oyun Bitti");
                Application.Exit();
            }
            else if (whiteCount == 0)
            {
                MessageBox.Show("Siyah Oyuncu Kazandı! Skor: " + blackScore, "Oyun Bitti");
                Application.Exit();
            }
        }
        private bool CanCaptureMore(int row, int col)
        {
            string pieceText = pieces[row, col]?.Text;
            if (pieceText == null)
                return false;

            bool isBlackPiece = (pieceText == "●");
            bool isBlackQueen = (pieceText == "♛");
            bool isWhitePiece = (pieceText == "○");
            bool isWhiteQueen = (pieceText == "♕");

           
            int[] dRows = { -1, 1, 0, 0 };
            int[] dCols = { 0, 0, -1, 1 };

          
            if (isBlackPiece || isWhitePiece)
            {
                for (int i = 0; i < 4; i++)
                {
                    int midRow = row + dRows[i];
                    int midCol = col + dCols[i];
                    int newRow = row + dRows[i] * 2;
                    int newCol = col + dCols[i] * 2;

                    if (IsInBounds(midRow, midCol) && IsInBounds(newRow, newCol))
                    {
                        if (pieces[newRow, newCol] == null && pieces[midRow, midCol] != null)
                        {
                            string midText = pieces[midRow, midCol].Text;
                            bool isEnemy = (isBlackPiece && (midText == "○" || midText == "♕")) ||
                                           (isWhitePiece && (midText == "●" || midText == "♛"));
                            if (isEnemy)
                                return true;
                        }
                    }
                }
            }

           
            if (isBlackQueen || isWhiteQueen)
            {
                for (int i = 0; i < 4; i++)
                {
                    int dirRow = dRows[i];
                    int dirCol = dCols[i];

                    int r = row + dirRow;
                    int c = col + dirCol;

                    while (IsInBounds(r, c))
                    {
                        if (pieces[r, c] != null)
                        {
                            string midText = pieces[r, c].Text;
                            bool isEnemy = (isBlackQueen && (midText == "○" || midText == "♕")) ||
                                           (isWhiteQueen && (midText == "●" || midText == "♛"));

                            
                            int behindRow = r + dirRow;
                            int behindCol = c + dirCol;

                            if (isEnemy && IsInBounds(behindRow, behindCol) && pieces[behindRow, behindCol] == null)
                                return true;
                            break; 
                        }
                        r += dirRow;
                        c += dirCol;
                    }
                }
            }

            return false;
        }
        private bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }


    }
}
        
    

