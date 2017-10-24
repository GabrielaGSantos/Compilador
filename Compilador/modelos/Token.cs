using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.modelos
{
    class Token
    {
        public String tipo;
        public String lexema;
        public int linha;
        public int coluna;

        public Token(String tipo, String lexema, int linha, int coluna)
        {
            this.tipo = tipo;
            this.lexema = lexema;
            this.linha = linha;
            this.coluna = coluna;
        }

        public override String ToString()
        {
            return this.tipo + "\t |"+this.lexema +"\t |"+this.linha +"\t\t |" + this.coluna+"\n";

        }
    }
}
