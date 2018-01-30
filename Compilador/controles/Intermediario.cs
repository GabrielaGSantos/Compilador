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
        List<Token> codigo_intermediario_final = new List<Token>();

        Token variavel_atual;
        int contador_variaveis = 0;

        public Intermediario(List<Token> lista_tokens, List<String> lista_variaveis)
        {
            this.tokens = lista_tokens;
            this.lista_variaveis = lista_variaveis;
        }

        public Tuple<List<String>, List<String>,  List<Token>> GerarCodigo()
        {
            acoes.Add("\nInício Geração Código intermediário");
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Tipo_token == "atribuicao")
                {
                    if (tokens[i + 2].Tipo_token != "final_linha")
                    {
                        codigo.Add(tokens[i]);
                        if (tokens[i + 4].Tipo_token != "final_linha")
                        {
                            variavel_atual = tokens[i - 1];
                            codigo.RemoveAt(codigo.Count() - 1);
                            codigo.RemoveAt(codigo.Count() - 1);
                            i++;
                            List<Token> expressao = new List<Token>();
                            int j = i;
                            while (tokens[j].Tipo_token != "final_linha")
                            {
                                expressao.Add(tokens[j]);
                                j++;
                                i++;
                            }
                            AnalisarExpressao(expressao);
                        }
                    }
                    else
                        codigo.Add(tokens[i]);
                }
                else
                {
                    if (tokens[i].Tipo_token == "programa")
                    {
                        codigo.Add(new Token("/*" + tokens[i].Lexema + " " + tokens[i + 1].Lexema+"*/", "programa", 0, 0));
                        i++;
                    }
                        
                    else if (tokens[i].Tipo_token == "tipo_variavel")
                        i = i + 2;                   
                    else
                        codigo.Add(tokens[i]);
                }
            }
            AnalisarRepeticoes();
            acoes.Add("Fim Geração Código intermediário");
            return new Tuple<List<String>, List<String>, List<Token>>(acoes, lista_variaveis, codigo_intermediario_final);
        }

        public void AnalisarExpressao(List<Token> expressao)
        {
            Stack<Token> pilha_temporaria = new Stack<Token>();
            List<Token> expressao_final = new List<Token>();

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

            while (pilha_temporaria.Count > 0)
            {
                expressao_final.Add(pilha_temporaria.Pop());
            }
            acoes.Add("Convertida expressão em expressão posfixa");

            String erro = null;
            foreach (var teste in expressao_final)
            {
                erro = erro + teste.Lexema + " ";
            }

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

                    operacao_temporaria.Add(new Token("_x" + (++contador_variaveis), "variavel", 0, 0));
                    operacao_temporaria.Add(new Token("=", "atribuicao", 0, 0));

                    lista_variaveis.Add("_x" + contador_variaveis);

                    operacao_temporaria.Add(var1);
                    operacao_temporaria.Add(operador);
                    operacao_temporaria.Add(var2);
                    operacao_temporaria.Add(new Token(";", "final_linha", 0, 0));

                    pilha_temporaria.Push(new Token("_x" + contador_variaveis, "variavel", 0, 0));
                    acoes.Add("Criada variável _x" + contador_variaveis);
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

        public void AnalisarRepeticoes()
        {
            Stack<int> pilha_temporaria_faca = new Stack<int>();
            Stack<int> pilha_temporaria_se = new Stack<int>();
            Stack<int> pilha_bandeira = new Stack<int>();
            Stack<String> pilha_temporaria = new Stack<String>();
            int contador_se = 0;
            int contador_faca = 0;

            for (int i = 0; i < codigo.Count; i++)
            {
                if (codigo[i].Tipo_token == "repetir")
                {
                    codigo_intermediario_final.Add(new Token("\nfaca_" + (++contador_faca) + ":\n", "repetir", 0, 0));
                    pilha_temporaria_faca.Push(contador_faca);
                    i++;
                    acoes.Add("Convertido token repetir");
                }
                else if (codigo[i].Tipo_token == "ateh")
                {
                    if (codigo_intermediario_final[codigo_intermediario_final.Count - 1].Lexema == "}")
                        codigo_intermediario_final.RemoveAt(codigo_intermediario_final.Count() - 1);
                    String var1 = codigo[i + 2].Lexema;
                    String var2 = codigo[i + 4].Lexema;

                    int bandeira_var1 = 0;
                    int bandeira_var2 = 0;

                    foreach (var variavel in lista_variaveis)
                    {
                        if (variavel == var1)
                            bandeira_var1 = 1;
                        if (variavel == var2)
                            bandeira_var2 = 1;
                    }
                    if (bandeira_var1 == 0)
                        codigo_intermediario_final.Add(new Token("\n\tMOV r2, #" + var1, "", 0, 0));
                    else
                    {
                        codigo_intermediario_final.Add(new Token("\n\tLDR r2, =" + var1, "", 0, 0));
                        codigo_intermediario_final.Add(new Token("\n\tLDR r2, [r2]", "", 0, 0));
                    }
                    if (bandeira_var2 == 0)
                        codigo_intermediario_final.Add(new Token("\n\tMOV r3, #" + var2, "", 0, 0));
                    else
                    {
                        codigo_intermediario_final.Add(new Token("\n\tLDR r3, =" + var2, "", 0, 0));
                        codigo_intermediario_final.Add(new Token("\n\tLDR r3, [r3]", "", 0, 0));
                    }

                    
                    int contador_temp = pilha_temporaria_faca.Pop();
                    if (codigo[i + 3].Tipo_token == "condicao_igual")
                    {
                        codigo_intermediario_final.Add(new Token("\n\tCMP r2, r3\n", "condicao_igual", 0, 0));
                        codigo_intermediario_final.Add(new Token("\tBNE " + "faca_" + contador_temp + "\n", "jump", 0, 0));
                        codigo_intermediario_final.Add(new Token("fim_faca_" + contador_temp + ":\n\n", "jump", 0, 0));
                    }
                    else
                    {
                        codigo_intermediario_final.Add(new Token("\n\tCMP r2, r3\n", "condicao_diferente", 0, 0));
                        codigo_intermediario_final.Add(new Token("\tBEQ " + "faca_" + contador_temp + "\n", "jump", 0, 0));
                        codigo_intermediario_final.Add(new Token("ate_" + contador_temp + ":\n", "ateh", 0, 0));
                    }
                    i = i + 6;
                    acoes.Add("Convertido token ateh");
                }
                else if (codigo[i].Tipo_token == "condicional_entrada")
                {
                    String var1 = codigo[i + 2].Lexema;
                    String var2 = codigo[i + 4].Lexema;

                    int bandeira_var1 = 0;
                    int bandeira_var2 = 0;

                    foreach (var variavel in lista_variaveis)
                    {
                        if (variavel == var1)
                            bandeira_var1 = 1;
                        if (variavel == var2)
                            bandeira_var2 = 1;
                    }
                    if (bandeira_var1 == 0)
                        codigo_intermediario_final.Add(new Token("\n\tMOV r2, #" + var1, "", 0, 0));
                    else
                    {
                        codigo_intermediario_final.Add(new Token("\n\tLDR r2, =" + var1, "", 0, 0));
                        codigo_intermediario_final.Add(new Token("\n\tLDR r2, [r2]", "", 0, 0));
                    }
                    if (bandeira_var2 == 0)
                        codigo_intermediario_final.Add(new Token("\n\tMOV r3, #" + var2, "", 0, 0));
                    else
                    {
                        codigo_intermediario_final.Add(new Token("\n\tLDR r3, =" + var2, "", 0, 0));
                        codigo_intermediario_final.Add(new Token("\n\tLDR r3, [r3]", "", 0, 0));
                    }

                    if (codigo[i + 3].Tipo_token == "condicao_igual")
                    {
                       
                        codigo_intermediario_final.Add(new Token("\n\tCMP r2, r3\n", "condicao_igual", 0, 0));                        
                        codigo_intermediario_final.Add(new Token("\tBNE " + "senao_" + (++contador_se) + "\n", "jump", 0, 0));
                    }
                    else
                    {
                        codigo_intermediario_final.Add(new Token("\n\tCMP r2, r3\n", "condicao_diferente", 0, 0));
                        codigo_intermediario_final.Add(new Token("\tBEQ " + "senao_" + (++contador_se) + "\n", "jump", 0, 0));
                    }
                    codigo_intermediario_final.Add(new Token("\nse_" + contador_se + ":", "condicional_entrada", 0, 0));
                    pilha_temporaria_se.Push(contador_se);
                    i = i + 6;
                    acoes.Add("Convertido token condicao_entrada");
                }
                else if (codigo[i].Tipo_token == "condicional_saida")
                {                                       
                    int contador_temp = pilha_temporaria_se.Pop();
                    pilha_temporaria.Push("fim_senao_" + contador_temp+":");
                    codigo_intermediario_final.Add(new Token("\nsenao_"+contador_temp + ":\n", "condicional_saida", 0, 0));
                    i++;
                    acoes.Add("Convertido token condicao_saida");
                }
                else if (codigo[i].Tipo_token == "fim")
                {
                    if(codigo.Count() > i+1)
                    {
                        if (codigo[i + 1].Tipo_token == "condicional_saida")
                        {
                            codigo_intermediario_final.Add(new Token("fim_se_" + contador_se+":", "fim_condicional_entrada", 0, 0));
                            codigo_intermediario_final.Add(new Token("\n\tB fim_senao_" + contador_se + "\n", "jump", 0, 0));
                        }
                        else if (pilha_temporaria.Count > 0)
                        {
                            String posicao = pilha_temporaria.Pop();
                            codigo_intermediario_final.Add(new Token(posicao + "\n", "final de funcao", 0, 0));
                        }
                    }
                    
                    else
                        codigo_intermediario_final.Add(new Token("\nfim_programa", "fim_programa", 0, 0));
                    acoes.Add("Convertido token fim");
                }
                else
                    codigo_intermediario_final.Add(codigo[i]);
            }
        }
    }
}
