using Compilador.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.controles
{
    class FinalRaspberry
    {
        List<Token> codigo = new List<Token>();
        List<String> acoes = new List<String>();
        List<Token> codigo_final = new List<Token>();
        List<String> lista_mensagens = new List<string>();
        List<String> lista_variaveis = new List<string>();

        int contador_mensagens = 0;
        int contador_divisao = 0;

        public FinalRaspberry(List<Token> codigo_intermediario, List<String> lista_variaveis, List<String> lista_mensagens)
        {
            this.codigo = codigo_intermediario;
            this.lista_variaveis = lista_variaveis;
            this.lista_mensagens = lista_mensagens;
        }

        public Tuple<List<Token>, List<String>> GerarCodigo()
        {
            lista_mensagens.Add(".section .data");
            for (int i = 0; i < codigo.Count; i++)
            {
            }

            return new Tuple<List<Token>, List<String>>(codigo_final, acoes);
        }
    }
}
