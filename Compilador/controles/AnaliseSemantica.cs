using Compilador.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.controles
{
    class AnaliseSemantica
    {
        List<Token> tokens = new List<Token>();
        List<String> lista_variaveis = new List<String>();
        List<String> acoes = new List<String>();
        Erro erros = null;

        public AnaliseSemantica(List<Token> lista_tokens)
        {
            this.tokens = lista_tokens;
        }

        public Tuple<Erro, List<String>> Analisar()
        {
            bool variavel = false;
            foreach (var token in tokens)
            {
                if (token.Tipo_token == "tipo_variavel")
                    variavel = true;

                if (token.Tipo_token == "final_linha")
                    variavel = false;

                if (variavel == true)
                {
                    if(token.Tipo_token == "variavel")
                        AdicionarVariavel(token);
                }                   
            }            
            return new Tuple<Erro, List<String>>(erros, acoes);
        }

        public void AdicionarVariavel(Token token)
        {
            bool bandeira_erro = false;

            if(lista_variaveis != null)
            {
                foreach(var variavel in lista_variaveis)
                {
                    if (token.Lexema == variavel)
                    {
                        bandeira_erro = true;
                        erros = new Erro("Erro Semantico", "Variavel já utilizada: " + variavel, token.Linha, token.Coluna, 1);
                    }                    
                }

                if(bandeira_erro == false)
                {
                    lista_variaveis.Add(token.Lexema);
                    acoes.Add("Variavel: '" + token.Lexema + "' declarada");
                }
            }
            else
            {
                lista_variaveis.Add(token.Lexema);
                acoes.Add("Variavel: '" + token.Lexema + "' declarada");
            }
        }
    }
}
