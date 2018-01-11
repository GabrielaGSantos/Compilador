using Compilador.modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.controles
{
    class Intermediario
    {
        List<Token> tokens = new List<Token>();
        List<String> lista_variaveis = new List<String>();
        List<String> acoes = new List<String>();
        List<String> codigo = new List<String>();

        public Intermediario(List<Token> lista_tokens, List<String> lista_variaveis)
        {
            this.tokens = lista_tokens;
            this.lista_variaveis = lista_variaveis;
        }

        public Tuple<List<String>, List<String>, List<String>> GerarCodigo()
        {
            for (int i = 0; i<tokens.Count; i++)
            {
                if (i+1 > tokens.Count && tokens[i+1].Tipo_token == "atribuicao")
                {
                    String variavel = tokens[i].Lexema;
                    i = i + 2;
                    if(tokens[i + 3].Tipo_token != "final_linha")
                    {
                        List<Token> expressao = new List<Token>();
                        int j = i;
                        while (tokens[j + 1].Tipo_token != "final_linha")
                        {
                            expressao.Add(tokens[j]);
                            j++;
                            i++;
                        }
                        AnalisarExpressao(expressao);
                        i++;
                    }                   
                }
                else
                {
                    if (tokens[i].Tipo_token == "programa")
                        i++;
                    else if (tokens[i].Tipo_token == "tipo_variavel")
                        i = i + 2;
                    else if (tokens[i].Tipo_token == "final_linha" || tokens[i].Tipo_token == "abrir_chave" || tokens[i].Tipo_token == "fim" || tokens[i].Tipo_token == "inicio")
                        codigo.Add(tokens[i].Lexema + "\n");
                    else
                        codigo.Add(tokens[i].Lexema + " ");
                }
            }
            return new Tuple<List<String>, List<String>, List<String>> (acoes, lista_variaveis, codigo);
        }

        public void AnalisarExpressao(List<Token> expressao)
        {
            List<Token> posfixa = new List<Token>();
            Stack<Token> pilha = new Stack<Token>();

            foreach(var posicao in expressao)
            {
                if (posicao.Tipo_token == "variavel" || posicao.Tipo_token == "variavel_numerica")
                    posfixa.Add(posicao);

                else if (posicao.Tipo_token == "abrir")
                    pilha.Push(posicao);

                else if (posicao.Tipo_token == "fechar")
                {
                    while (pilha.Count != 0 || pilha.Peek().Tipo_token != "abrir")
                        posfixa.Add(pilha.Pop());
                }

                else if (posicao.Tipo_token == "soma" || posicao.Tipo_token == "subtracao" || posicao.Tipo_token == "multiplicacao" || posicao.Tipo_token == "divisao")
                {
                    if(posfixa.Count == 0)
                        pilha.Push(posicao);
                    else
                    {                      
                        if(posicao.Tipo_token == "soma" || posicao.Tipo_token == "subtracao")
                        {
                            while(pilha.Peek().Tipo_token == "soma" || pilha.Peek().Tipo_token == "subtracao" || pilha.Peek().Tipo_token == "multiplicacao" || pilha.Peek().Tipo_token == "divisao")
                                posfixa.Add(pilha.Pop());
                            pilha.Push(posicao);
                        }

                        else if (posicao.Tipo_token == "multiplicacao" || posicao.Tipo_token == "divisao")
                        {
                            while (pilha.Peek().Tipo_token == "multiplicacao" || pilha.Peek().Tipo_token == "divisao")
                                posfixa.Add(pilha.Pop());
                            pilha.Push(posicao);
                        }
                    }
                }
            }
        }

    }
}
