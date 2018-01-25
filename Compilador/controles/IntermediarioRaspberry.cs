using Compilador.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.controles
{
    class IntermediarioRaspberry
    {
        List<Token> codigo = new List<Token>();
        List<String> acoes = new List<String>();
        List<Token> codigo_raspberry = new List<Token>();
        List<String> lista_mensagens = new List<string>();
        
        int contador_mensagens = 0;
        int contador_divisao = 0;

        public IntermediarioRaspberry(List<Token> codigo_intermediario, List<String> acoes)
        {
            this.codigo = codigo_intermediario;
            this.acoes = acoes;
        }

        public Tuple<List<Token>, List<String>, List<String>> GerarCodigo()
        {
            lista_mensagens.Add(".section .data");
            for (int i = 0; i < codigo.Count; i++)
            {
                if (codigo[i].Tipo_token == "escreva")
                {
                    codigo_raspberry.Add(codigo[i]);
                    if(codigo[i+2].Tipo_token == "texto")
                    {
                        i = i + 2;
                        lista_mensagens.Add("\tmsg" + (++contador_mensagens) + ": .asciz " + codigo[i].Lexema);
                        lista_mensagens.Add("\tmsglen" + (contador_mensagens) + "= .-msg" + contador_mensagens);
                        codigo_raspberry.Add(new Token("msg" + (contador_mensagens), "texto", 0, 0));
                        acoes.Add("Salva mensagem: " + codigo[i].Lexema);
                        i++;
                    }
                }
                else if(codigo[i].Tipo_token == "atribuicao")
                {
                    if(codigo[i+2].Tipo_token == "divisao")
                    {
                        String variavel_atual = codigo[i - 1].Lexema;
                        codigo_raspberry.RemoveAt(codigo_raspberry.Count - 1);
                        String dividento = codigo[i + 1].Lexema;
                        String divisor = codigo[i + 3].Lexema;

                        codigo_raspberry.Add(new Token("\n\tLDR r2, =" + dividento, "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tLDR r2, [r2]", "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tLDR r3, =" + divisor, "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tLDR r3, [r3]", "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tMOV r4, #0", "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\ndivisao_" + (++contador_divisao) + ":", "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tSUB r2, r2, r3", "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tADD r4, r4, #1", "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tCMP r2, r3", "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tBGT divisao_"+contador_divisao, "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tLDR r1, ="+variavel_atual, "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\n\tSTR r4, [r1]", "assembly_divisao", 0, 0));
                        codigo_raspberry.Add(new Token("\nfim_divisao_"+contador_divisao+"\n\n", "assembly_divisao", 0, 0));
                        i = i + 4;
                        acoes.Add("Convertido token divisao para algoritmo de divisao");
                    }
                    else
                        codigo_raspberry.Add(codigo[i]);
                }
                else
                    codigo_raspberry.Add(codigo[i]);
            }
            lista_mensagens.Add(".text");
            return new Tuple<List<Token>, List<String>, List<String>>(codigo_raspberry, acoes, lista_mensagens);
        }
    }
}
