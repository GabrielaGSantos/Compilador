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
        List<Token> codigo = new List<Token>();
        List<Token> expressao_final = new List<Token>();
        Token variavel_atual;
        int contador_variaveis = 0;

        public Intermediario(List<Token> lista_tokens, List<String> lista_variaveis)
        {
            this.tokens = lista_tokens;
            this.lista_variaveis = lista_variaveis;
        }

        public Tuple<List<String>, List<String>, List<Token>> GerarCodigo()
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Tipo_token == "atribuicao")
                {
                    variavel_atual = tokens[i - 1];
                    i = i + 1;
                    if (tokens[i + 3].Tipo_token != "final_linha")
                    {
                        List<Token> expressao = new List<Token>();
                        int j = i;
                        while (tokens[j].Tipo_token != "final_linha")
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
                        codigo.Add(tokens[i]);
                    else
                        codigo.Add(tokens[i]);
                }
            }
            return new Tuple<List<String>, List<String>, List<Token>>(acoes, lista_variaveis, codigo);
        }

        public void AnalisarExpressao(List<Token> expressao)
        {
            Stack<Token> pilha_temporaria = new Stack<Token>();

            foreach (var posicao in expressao)
            {
                if (posicao.Tipo_token == "variavel" || posicao.Tipo_token == "variavel_numerica")
                    expressao_final.Add(posicao);

                else if (posicao.Tipo_token == "abrir")
                    pilha_temporaria.Push(posicao);

                else if (posicao.Tipo_token == "fechar")
                {
                    while (pilha_temporaria.Peek().Tipo_token != "abrir")
                        expressao_final.Add(pilha_temporaria.Pop());
                    pilha_temporaria.Pop();
                }

                else if (posicao.Tipo_token == "soma" || posicao.Tipo_token == "subtracao" || posicao.Tipo_token == "multiplicacao" || posicao.Tipo_token == "divisao")
                {
                    if (pilha_temporaria.Count == 0)
                        pilha_temporaria.Push(posicao);

                    else if (posicao.Tipo_token == "soma" || posicao.Tipo_token == "subtracao")
                    {
                        while (pilha_temporaria.Count != 0 && (pilha_temporaria.Peek().Tipo_token == "soma" || pilha_temporaria.Peek().Tipo_token == "subtracao" || pilha_temporaria.Peek().Tipo_token == "multiplicacao" || pilha_temporaria.Peek().Tipo_token == "divisao"))
                            expressao_final.Add(pilha_temporaria.Pop());
                        pilha_temporaria.Push(posicao);
                    }

                    else if (posicao.Tipo_token == "multiplicacao" || posicao.Tipo_token == "divisao")
                    {
                        while (pilha_temporaria.Count != 0 && (pilha_temporaria.Peek().Tipo_token == "multiplicacao" || pilha_temporaria.Peek().Tipo_token == "divisao"))
                            expressao_final.Add(pilha_temporaria.Pop());
                        pilha_temporaria.Push(posicao);
                    }
                }
                else
                {
                    expressao_final.Add(posicao);
                }
            }

            String final = null;
            foreach (var teste in expressao_final)
            {
                final = final + teste.Lexema.ToString() + " ";
            }

            while (pilha_temporaria.Count > 0)
            {
                final += pilha_temporaria.Pop().Lexema.ToString() + " ";
            }
            acoes.Add("Convertida expressão: " + expressao.ToString() + "em expressão posfixa: " + final);

            ConverterExpressao(expressao_final);
        }

        public void ConverterExpressao(List<Token> expressao_posfixa)
        {
            Stack<Token> pilha_temporaria = new Stack<Token>();
            List<Token> operacao_temporaria = new List<Token>();

            while (expressao_posfixa.Count() > 1)
            {
                Token token_atual = expressao_posfixa[0];

                if (token_atual.Tipo_token == "soma" || token_atual.Tipo_token == "subtracao" || token_atual.Tipo_token == "multiplicacao" || token_atual.Tipo_token == "divisao")
                {
                    Token var2 = pilha_temporaria.Pop();
                    Token var1 = pilha_temporaria.Pop();
                    Token operador = token_atual;
                    expressao_posfixa.RemoveAt(0);

                    operacao_temporaria.Add(new Token("_x" + contador_variaveis++, "variavel", 0, 0));
                    operacao_temporaria.Add(new Token("=", "atribuicao", 0, 0));

                    lista_variaveis.Add("_x" + contador_variaveis);

                    operacao_temporaria.Add(var1);
                    operacao_temporaria.Add(operador);
                    operacao_temporaria.Add(var2);
                    operacao_temporaria.Add(new Token(";", "final_linha", 0, 0));

                    pilha_temporaria.Push(new Token("_x" + contador_variaveis, "variavel", 0, 0));
                }

                else
                {
                    pilha_temporaria.Push(token_atual);
                    expressao_posfixa.RemoveAt(0);
                }
            }

            Token var_2 = pilha_temporaria.Pop();
            Token operacao = expressao_posfixa[0];
            Token var_1 = pilha_temporaria.Pop();

            operacao_temporaria.Add(variavel_atual);
            operacao_temporaria.Add(new Token("=", "atribuicao", 0, 0));
            operacao_temporaria.Add(var_1);
            operacao_temporaria.Add(operacao);
            operacao_temporaria.Add(var_2);
            operacao_temporaria.Add(new Token(";", "final_linha", 0, 0));


            foreach (var token in operacao_temporaria)
            {
                codigo.Add(token);
            }
        }            
    }
}
