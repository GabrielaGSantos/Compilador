using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.modelos
{
    class Erro
    {
        public String tipo;
        public String descricao;
        public int linha;
        public int coluna;
        public int erro_alerta;

        public Erro(String tipo, String descricao, int linha, int coluna, int erro_alerta)
        {
            this.tipo = tipo;
            this.descricao = descricao;
            this.linha = linha;
            this.coluna = coluna;
            this.erro_alerta = erro_alerta;
        }

        public override String ToString()
        {
            return this.tipo + ": " + this.descricao + " (Linha: " + this.linha + ", Coluna: " + this.coluna+")\n";

        }
    }
}
