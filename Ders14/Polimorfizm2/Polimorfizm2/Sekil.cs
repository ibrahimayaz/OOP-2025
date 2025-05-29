using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polimorfizm2
{
    public class Sekil
    {
        protected string Renk {  get; set; }

        public virtual string Ciz()
        {
            return "Şekil çiziliyor...";
        }

    }
}
