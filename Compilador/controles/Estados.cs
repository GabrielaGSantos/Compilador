using Compilador.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Estados
    {
        char[] palavra;
        String lexema;
        int posicao, coluna, tamanho, linha, caracter;

        List<Token> tokens = new List<Token>();
        List<Erro> erros = new List<Erro>();

        public Estados(String palavra, int posicao, int tamanho, int linha)
        {
            this.palavra = palavra.ToCharArray();
            this.posicao = 0;
            this.tamanho = tamanho;
            this.linha = linha;
            this.coluna = posicao;
            this.lexema = "";
        }
        public Tuple<List<Token>, List<Erro>> Estado_q0()
        {
            if (tamanho > 0)
            {
                if (palavra[posicao] == ';')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q15();
                }

                else if (palavra[posicao] == 'a')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q50();
                }

                else if (palavra[posicao] == 'f')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q46();
                }

                else if (palavra[posicao] == 'P')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q1();
                }

                else if (palavra[posicao] == 'l')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q37();
                }

                else if (palavra[posicao] == 'i')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q9();
                }

                else if (palavra[posicao] == 'e')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q31();
                }

                else if (palavra[posicao] == 'n')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q26();
                }

                else if (palavra[posicao] == 's')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q41();
                }

                else if (palavra[posicao] == ':')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q22();
                }

                else if (palavra[posicao] == '=')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q20_final();
                }

                else if (palavra[posicao] == '+')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q16_final();
                }

                else if (palavra[posicao] == '-')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q17_final();
                }

                else if (palavra[posicao] == '*')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q18_final();
                }

                else if (palavra[posicao] == '/')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q19_final();
                }

                else if (palavra[posicao] == '{')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q24_final();
                }

                else if (palavra[posicao] == '}')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q25_final();
                }

                else if (palavra[posicao] == '(')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q29_final();
                }

                else if (palavra[posicao] == ')')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q30_final();
                }

                else if (palavra[posicao] == '"')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q55();
                }

                else if (palavra[posicao] == '\t')
                {
                    posicao++;
                    tamanho--;
                    return Estado_q0();
                }
                else
                {
                    caracter = Convert.ToInt32(palavra[posicao]);

                    if ((caracter > 64 && caracter < 91) || (caracter > 96 && caracter < 123))
                    {
                        lexema += palavra[posicao];
                        posicao++;
                        tamanho--;
                        return Estado_q54_final();
                    }
                    else if (caracter > 47 && caracter < 58)
                    {
                        lexema += palavra[posicao];
                        posicao++;
                        tamanho--;
                        return Estado_q53_final(); ;
                    }
                    else
                    {
                        return new Tuple<List<Token>, List<Erro>>(tokens, erros);
                    }
                }
            }
            else
            {
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
        }

        public Tuple<List<Token>, List<Erro>> Estado_q1()
        {
            if (tamanho > 0 && palavra[posicao] == 'r')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q2();
            }
            else
                return Estado_q54_final();
        }

        public Tuple<List<Token>, List<Erro>> Estado_q2()
        {
            if (tamanho > 0 && palavra[posicao] == 'o')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q3();
            }
            else
                return Estado_q54_final();
        }

        private Tuple<List<Token>, List<Erro>> Estado_q3()
        {
            if (tamanho > 0 && palavra[posicao] == 'g')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q4();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q4()
        {
            if (tamanho > 0 && palavra[posicao] == 'r')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q5();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q5()
        {
            if (tamanho > 0 && palavra[posicao] == 'a')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q6();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q6()
        {
            if (tamanho > 0 && palavra[posicao] == 'm')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q7();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q7()
        {
            if (tamanho > 0 && palavra[posicao] == 'a')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q8_final();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q8_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "programa", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                    palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                    palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "programa", linha + 1, posicao + coluna + 1));
                    coluna = posicao;
                    lexema = palavra[posicao].ToString();
                    return Estado_q0();
                }
                else
                {
                    return Estado_q54_final();
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q9()
        {
            if (tamanho > 0 && palavra[posicao] == 'n')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q10();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q10()
        {
            if (tamanho > 0 && palavra[posicao] == 'i')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q11();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q11()
        {
            if (tamanho > 0 && palavra[posicao] == 'c')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q12();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q12()
        {
            if (tamanho > 0 && palavra[posicao] == 'i')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q13();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q13()
        {
            if (tamanho > 0 && palavra[posicao] == 'o')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q14();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q14()
        {
            if (tamanho > 0 && palavra[posicao] == '{')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q40_final();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q15()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "final_linha", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "final_linha", linha + 1, posicao + coluna + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q16_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "soma", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "soma", linha + 1, posicao + coluna + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q17_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "subtracao", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "subtracao", linha + 1, posicao + coluna + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q18_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "multiplicacao", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "multiplicacao", linha + 1, posicao + coluna + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q19_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "divisao", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "divisao", linha + 1, posicao + coluna + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }


        private Tuple<List<Token>, List<Erro>> Estado_q20_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "atribuicao", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }

            else if (tamanho > 0 && palavra[posicao] == '=')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q21_final();
            }
            else
            {
                tokens.Add(new Token(lexema, "atribuicao", linha + 1, posicao + coluna + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q21_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "condicao_igual", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "condicao_igual", linha + 1, posicao + coluna + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q22()
        {
            if (tamanho > 0 && palavra[posicao] == '=')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q23_final();
            }
            else
            {
                erros.Add(new Erro("Erro Lexico! Caracter inesperado", palavra[posicao].ToString(), linha + 1, coluna + posicao + 1, 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q23_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "condicao_diferente", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {                
                tokens.Add(new Token(lexema, "condicao_diferente", linha + 1, coluna + posicao + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
               
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q24_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "abrir_chave", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "abrir_chave", linha + 1, coluna + posicao + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q25_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "fim", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "fim", linha + 1, coluna + posicao + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q26()
        {
            if (tamanho > 0 && palavra[posicao] == 'u')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q27();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q27()
        {
            if (tamanho > 0 && palavra[posicao] == 'm')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q28_final();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q28_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "tipo_variavel", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                    palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                    palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "tipo_variavel", linha + 1, coluna + posicao + 1));
                    coluna = posicao;
                    lexema = null;
                    return Estado_q0();
                }
                else
                {
                    return Estado_q54_final();
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q29_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "abrir", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "abrir", linha + 1, coluna + posicao + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q30_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "fechar", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "fechar", linha + 1, coluna + posicao + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q31()
        {
            if (tamanho > 0 && palavra[posicao] == 'x')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q32();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q32()
        {
            if (tamanho > 0 && palavra[posicao] == 'i')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q33();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q33()
        {
            if (tamanho > 0 && palavra[posicao] == 'b')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q34();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q34()
        {
            if (tamanho > 0 && palavra[posicao] == 'i')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q35();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q35()
        {
            if (tamanho > 0 && palavra[posicao] == 'r')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q36_final();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q36_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "escreva", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                    palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                    palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "escreva", linha + 1, coluna + posicao + 1));
                    coluna = posicao;
                    lexema = null;
                    return Estado_q0();
                }
                else
                {
                    return Estado_q54_final();
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q37()
        {
            if (tamanho > 0 && palavra[posicao] == 'e')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q38();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q38()
        {
            if (tamanho > 0 && palavra[posicao] == 'r')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q39_final();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q39_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "leia", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                   palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                   palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "leia", linha + 1, coluna + posicao + 1));
                    coluna = posicao;
                    lexema = null;
                    return Estado_q0();
                }
                else
                {
                    return Estado_q54_final();
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q40_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "inicio", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "inicio", linha + 1, coluna + posicao + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
               
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q41()
        {
            if (tamanho > 0 && palavra[posicao] == 'e')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q42_final();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q42_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "condicional_entrada", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }

            else if (palavra[posicao] == 'n')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q43();
            }
            else
            {
                if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                   palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                   palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "condicional_entrada", linha + 1, posicao + coluna + 1));
                    coluna = posicao;
                    lexema = null;
                    return Estado_q0();
                }
                else
                {
                    return Estado_q54_final();
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q43()
        {
            if (tamanho > 0 && palavra[posicao] == 'a')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q44();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q44()
        {
            if (tamanho > 0 && palavra[posicao] == 'o')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q45_final();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q45_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "condicional_saida", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                   palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                   palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "condicional_saida", linha + 1, posicao + coluna + 1));
                    coluna = posicao;
                    lexema = null;
                    return Estado_q0();
                }
                else
                {
                    return Estado_q54_final();
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q46()
        {
            if (tamanho > 0 && palavra[posicao] == 'a')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q47();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q47()
        {
            if (tamanho > 0 && palavra[posicao] == 'c')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q48();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q48()
        {
            if (tamanho > 0 && palavra[posicao] == 'a')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q49_final();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q49_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "repetir", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                   palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                   palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "repetir", linha + 1, posicao + coluna + 1));
                    coluna = posicao;
                    lexema = null;
                    return Estado_q0();
                }
                else
                {
                    return Estado_q54_final();
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q50()
        {
            if (tamanho > 0 && palavra[posicao] == 't')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q51();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q51()
        {
            if (tamanho > 0 && palavra[posicao] == 'e')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q52_final();
            }
            else
            {
                return Estado_q54_final();
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q52_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "ateh", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                   palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                   palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "ateh", linha + 1, posicao + coluna + 1));
                    coluna = posicao;
                    lexema = null;
                    return Estado_q0();
                }
                else
                {
                    return Estado_q54_final();
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q53_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "variavel_numerica", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                caracter = Convert.ToInt32(palavra[posicao]);

                if (caracter > 47 && caracter < 58)
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q53_final();
                }

                else if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                    palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                    palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "variavel_numerica", linha + 1, posicao + coluna + 1));
                    coluna = posicao;
                    lexema = null;
                    return Estado_q0();
                }
                else
                {
                    erros.Add(new Erro("Erro Léxico Caracter inesperado", palavra[posicao].ToString(), linha + 1, coluna + posicao + 1, 1));
                    return new Tuple<List<Token>, List<Erro>>(tokens, erros);
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q54_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "variavel", linha + 1, posicao + coluna + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                caracter = Convert.ToInt32(palavra[posicao]);
                if ((caracter > 64 && caracter < 91) || (caracter > 96 && caracter < 123))
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q54_final();
                }
                else if (caracter > 47 && caracter < 58)
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q54_final();
                }

                else if (palavra[posicao] == '_')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q54_final();
                }

                else if (palavra[posicao] == ';' || palavra[posicao] == ':' || palavra[posicao] == '=' || palavra[posicao] == '/' ||
                    palavra[posicao] == '+' || palavra[posicao] == '-' || palavra[posicao] == '{' || palavra[posicao] == '}' || palavra[posicao] == '(' ||
                    palavra[posicao] == '*' || palavra[posicao] == ')')
                {
                    tokens.Add(new Token(lexema, "variavel", linha + 1, coluna + posicao + 1));
                    coluna = posicao;
                    lexema = null;
                    return Estado_q0();
                }
                else
                {
                    erros.Add(new Erro("Erro Léxico! Caracter inesperado", palavra[posicao].ToString(), linha + 1, coluna + posicao + 1, 1));
                    return new Tuple<List<Token>, List<Erro>>(tokens, erros);
                }
            }
        }

        private Tuple<List<Token>, List<Erro>> Estado_q55()
        {
            if (tamanho > 0 && palavra[posicao] != '"')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q55();
            }
            else if (tamanho > 0 &&  palavra[posicao] == '"')
            {
                lexema += palavra[posicao];
                posicao++;
                tamanho--;
                return Estado_q56_final();
            }
            else
            {
                erros.Add(new Erro("Erro Léxico! Esperado \" ", "", linha + 1, coluna + posicao + 1, 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }            
        }

        private Tuple<List<Token>, List<Erro>> Estado_q56_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "texto", linha + 1, coluna + posicao + 1));
                return new Tuple<List<Token>, List<Erro>>(tokens, erros);
            }
            else
            {
                tokens.Add(new Token(lexema, "texto", linha + 1, posicao + coluna + 1));
                coluna = posicao;
                lexema = null;
                return Estado_q0();
            }
        }
     }    
}
