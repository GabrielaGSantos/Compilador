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
                if (palavra[posicao] == 'P')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q1();
                }

                else if (palavra[posicao] == 'i')
                {
                    lexema += palavra[posicao];
                    posicao++;
                    tamanho--;
                    return Estado_q9();
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
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
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
                tokens.Add(new Token(lexema, "Programa", linha+1, coluna+1));
                erros.Add(new Erro("", "", 0, 0, 1));
                return new Tuple<List<Token>, List<Erro>>(tokens,erros);
            }
            else
            {
                return Estado_q54_final();                
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

        private Tuple<List<Token>, List<Erro>> Estado_q40_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "inicio", linha + 1, coluna + 1));
                erros.Add(new Erro("", "", 0, 0, 1));

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
                else
                {
                    erros.Add(new Erro("Erro! Caracter inesperado", palavra[posicao].ToString(), linha + 1, coluna + posicao + 1, 1));
                    return new Tuple<List<Token>, List<Erro>>(tokens, erros);
                }
            }
        }
        private Tuple<List<Token>, List<Erro>> Estado_q54_final()
        {
            if (tamanho == 0)
            {
                tokens.Add(new Token(lexema, "variavel", linha + 1, coluna + 1));
                erros.Add(new Erro("", "", 0, 0, 1));

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
                else
                {
                    erros.Add(new Erro("Erro! Caracter inesperado", palavra[posicao].ToString(), linha+1, coluna+posicao+1, 1));
                    return new Tuple<List<Token>, List<Erro>>(tokens, erros);
                }
            }
        }
    }
}
