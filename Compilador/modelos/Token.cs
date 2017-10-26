using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.modelos
{
    class Token
    {
        private String lexema;
        private String tipo_token;
        private int linha;
        private int coluna;

        public string Lexema { get => lexema; set => lexema = value; }
        public string Tipo_token { get => tipo_token; set => tipo_token = value; }
        public int Linha { get => linha; set => linha = value; }
        public int Coluna { get => coluna; set => coluna = value; }

        public Token(String lexema, String token, int linha, int coluna)
        {
            this.Lexema = lexema;
            this.Tipo_token = token;
            this.Linha = linha;
            this.Coluna = coluna;
        }        
    }
}
