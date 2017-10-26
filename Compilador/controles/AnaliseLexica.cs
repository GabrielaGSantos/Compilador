using Compilador.modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    class AnaliseLexica
    {
        String programa;
        RichTextBox console;
        String[] subPrograma;
        ArrayList resposta = new ArrayList();

        List<Token> tokens = new List<Token>();
        List<Erro> erros = new List<Erro>();

        Estados estados;

        public AnaliseLexica(String programa, RichTextBox console)
        {
            this.programa = programa;
            this.console = console;
        }

        public Tuple<List<Token>, List<Erro>> Analisar()
        {
            int num_linha = 0;
            subPrograma = programa.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (var linha in subPrograma)
            {
                AnalisarLinha(linha, num_linha++);
            }

            return new Tuple<List<Token>, List<Erro>>(tokens, erros);
        }

        public void AnalisarLinha(String linha, int numLinha)
        {
            int num_coluna = 0, qntd_coluna;
            bool final_linha = false;
            Tuple<List<Token>, List<Erro>> token_erro = null;

            String[] palavras = linha.Split(new[] { ' ' }, 2);

            if (palavras[0].Contains('"'))
            {
                if(palavras.Length == 2)
                {
                    palavras[0] += " " + palavras[1];
                    palavras[1] = "";
                    palavras = palavras[0].Split(new[] { '"' }, 3);
                }
                else
                {
                    palavras = palavras[0].Split(new[] { '"' }, 3);
                }
                
                if (palavras.Length == 3)
                {
                    palavras[0] = palavras[0] + '"' + palavras[1] + '"';
                    palavras[1] = palavras[2];
                }
                else
                {
                    palavras[0] = palavras[0] + '"' + palavras[1];
                    palavras = new string[] { palavras[0]};
                }
            }

            qntd_coluna = palavras[0].Length;
            
            while (final_linha == false)
            {   
               
                estados = new Estados(palavras[0], num_coluna, qntd_coluna, numLinha);
                token_erro = estados.Estado_q0();
                                
                if(token_erro != null)
                {
                    foreach (var token in token_erro.Item1)
                    {
                        tokens.Add(token);
                    }

                    foreach (var erro in token_erro.Item2)
                    {
                        erros.Add(erro);
                    }
                }

                if (palavras.Length > 1)
                {
                    linha = palavras[1];
                    palavras = linha.Split(new char[] { ' ' }, 2);
                    if (palavras[0].Contains('"'))
                    {
                        if (palavras.Length == 2)
                        {
                            palavras[0] += " " + palavras[1];
                            palavras[1] = "";
                            palavras = palavras[0].Split(new[] { '"' }, 3);
                        }
                        else
                        {
                            palavras = palavras[0].Split(new[] { '"' }, 3);
                        }
                        if (palavras.Length == 3)
                        {
                            palavras[0] = palavras[0] + '"' + palavras[1] + '"';
                            palavras[1] = palavras[2];
                        }
                        else
                        {
                            palavras[0] = palavras[0] + '"' + palavras[1];
                            palavras = new string[] { palavras[0] };
                        }                            
                    }
                    num_coluna = qntd_coluna + 1;
                    qntd_coluna = palavras[0].Length;
                }
                else
                    final_linha = true;                
            };            
        }

    }
}
