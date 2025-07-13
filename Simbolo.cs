using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    internal class Simbolo
    {
        public string Lexema {  get; set; }
        public string Token { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }
    }
}
