namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
         

namespace SimpleCheckers
    {
        class Program
        {
            const int SIZE = 8;
            static string[,] board = new string[SIZE, SIZE];

            static void Main(string[] args)
            {
                InitializeBoard();

                bool playerOneTurn = true;

                while (true)
                {
                    Console.Clear();
                    PrintBoard();

                    Console.WriteLine($"Player {(playerOneTurn ? "1 (X)" : "2 (O)")} turn.");
                    Console.Write("Enter move (e.g., 2 1 3 0): ");
                    var input = Console.ReadLine();
                    var tokens = input.Split();

                    if (tokens.Length != 4 ||
                        !int.TryParse(tokens[0], out int fromRow) ||
                        !int.TryParse(tokens[1], out int fromCol) ||
                        !int.TryParse(tokens[2], out int toRow) ||
                        !int.TryParse(tokens[3], out int toCol))
                    {
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        continue;
                    }

                    if (IsValidMove(fromRow, fromCol, toRow, toCol, playerOneTurn))
                    {
                        MakeMove(fromRow, fromCol, toRow, toCol);
                        playerOneTurn = !playerOneTurn;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move!");
                        Console.ReadKey();
                    }
                }
            }

            static void InitializeBoard()
            {
                for (int row = 0; row < SIZE; row++)
                {
                    for (int col = 0; col < SIZE; col++)
                    {
                        if ((row + col) % 2 == 1 && row < 3)
                            board[row, col] = "O"; // Player 2
                        else if ((row + col) % 2 == 1 && row > 4)
                            board[row, col] = "X"; // Player 1
                        else
                            board[row, col] = "."; // Empty
                    }
                }
            }

            static void PrintBoard()
            {
                Console.WriteLine("  0 1 2 3 4 5 6 7");
                for (int row = 0; row < SIZE; row++)
                {
                    Console.Write(row + " ");
                    for (int col = 0; col < SIZE; col++)
                    {
                        Console.Write(board[row, col] + " ");
                    }
                    Console.WriteLine();
                }
            }

            static bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol, bool isPlayerOne)
            {
                string playerSymbol = isPlayerOne ? "X" : "O";

                if (fromRow < 0 || fromRow >= SIZE || fromCol < 0 || fromCol >= SIZE ||
                    toRow < 0 || toRow >= SIZE || toCol < 0 || toCol >= SIZE)
                    return false;

                if (board[fromRow, fromCol] != playerSymbol || board[toRow, toCol] != ".")
                    return false;

                int dir = isPlayerOne ? -1 : 1;
                int rowDiff = toRow - fromRow;
                int colDiff = Math.Abs(toCol - fromCol);

                // Basic move
                if (rowDiff == dir && colDiff == 1)
                    return true;

                // Capture move
                if (rowDiff == 2 * dir && colDiff == 2)
                {
                    int midRow = (fromRow + toRow) / 2;
                    int midCol = (fromCol + toCol) / 2;
                    string opponent = isPlayerOne ? "O" : "X";
                    if (board[midRow, midCol] == opponent)
                        return true;
                }

                return false;
            }

            static void MakeMove(int fromRow, int fromCol, int toRow, int toCol)
            {
                board[toRow, toCol] = board[fromRow, fromCol];
                board[fromRow, fromCol] = ".";

                // Handle capture
                if (Math.Abs(toRow - fromRow) == 2)
                {
                    int midRow = (fromRow + toRow) / 2;
                    int midCol = (fromCol + toCol) / 2;
                    board[midRow, midCol] = ".";
                }
            }
        }
    }

}
    


