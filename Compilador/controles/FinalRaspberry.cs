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
        List<String> codigo_final = new List<String>();
        List<String> lista_mensagens = new List<string>();
        List<String> lista_variaveis = new List<string>();
        
        public FinalRaspberry(List<Token> codigo_intermediario, List<String> lista_variaveis, List<String> lista_mensagens, List<String> acoes)
        {
            this.codigo = codigo_intermediario;
            this.lista_variaveis = lista_variaveis;
            this.lista_mensagens = lista_mensagens;
            this.acoes = acoes;

        }

        public Tuple<List<String>, List<String>> GerarCodigo()
        {
            acoes.Add("\nInício Geração Código Final");
            codigo_final.Add(codigo[0].Lexema+"\n\n");
            codigo.RemoveAt(0);
                     
            foreach (var variavel in lista_variaveis)
                codigo_final.Add("\n.comm " + variavel + ", 32");
            codigo_final.Add("\n\n");

            foreach (var msg in lista_mensagens)
                codigo_final.Add("\n"+msg);

            for (int i = 0; i < codigo.Count; i++)
            {
                if (codigo[i].Tipo_token == "inicio")
                {
                    codigo_final.Add("\n\n.global main \n.func main\nmain:");
                    acoes.Add("Convertido token inicio");
                }
                else if(codigo[i].Lexema == ";")
                    acoes.Add("Convertido token final_linha");
                else if (codigo[i].Tipo_token == "escreva")
                {
                    if (codigo[i + 1].Tipo_token == "texto")
                    {
                        codigo_final.Add("\n\n\tLDR r1, =msg" + codigo[i + 1].Linha);
                        codigo_final.Add("\n\tLDR r2, =msglen" + codigo[i + 1].Linha);
                        codigo_final.Add("\n\tMOV r0, #1");
                        codigo_final.Add("\n\tMOV r7, #4");
                        codigo_final.Add("\n\tSWI 0\n");
                        i = i + 2;
                        acoes.Add("Convertido token imprimir texto");
                    }
                    else
                    {
                        codigo_final.Add("\n\n\tLDR r0, addr_pattern");
                        codigo_final.Add("\n\tLDR r1, =" + codigo[i + 2].Lexema);
                        codigo_final.Add("\n\n\tLDR r1, [r1]");
                        codigo_final.Add("\n\tBL printf");
                        codigo_final.Add("\n\n\tLDR lr, addr_lr_bu");
                        codigo_final.Add("\n\n\tLDR lr, [lr]");
                        codigo_final.Add("\n\tBX lr\n");
                        i = i + 4;
                        acoes.Add("Convertido token imprimir variavel");
                    }
                }
                else if (codigo[i].Tipo_token == "leia")
                {
                    codigo_final.Add("\n\n\tLDR r1, addr_lr_bu");
                    codigo_final.Add("\n\tLDR R0, addr_pattern\n");
                    codigo_final.Add("\n\tLDR r1, =" + codigo[i + 1].Lexema);
                    codigo_final.Add("\n\tBL scanf\n");
                    acoes.Add("Convertido token leitura");
                    i = i + 2;
                }
                else if (codigo[i].Lexema == "=")
                {

                    String var1 = codigo[i + 1].Lexema;
                    String var2 = codigo[i + 3].Lexema;
                    int bandeira_var1 = 0;
                    int bandeira_var2 = 0;

                    foreach (var variavel in lista_variaveis)
                    {
                        if (var1 == variavel)
                        {
                            var1 = "=" + var1;
                            bandeira_var1 = 1;
                        }                            
                        if (var2 == variavel)
                        {
                            bandeira_var2 = 1;
                            var2 = "=" + var2;
                        }
                    }
                    
                    if (codigo[i + 2].Lexema == "+")
                    {
                        String variavel_atual = codigo[i - 1].Lexema;
                        codigo_final.RemoveAt(codigo_final.Count - 1);

                        if (bandeira_var1 == 1)
                        {
                            codigo_final.Add("\n\n\tLDR r2, " + var1);
                            codigo_final.Add("\n\tLDR r2, [r2]");
                        }
                        else
                            codigo_final.Add("\n\n\tMOV r2, #" + var1);

                        if(bandeira_var2 == 1)
                        {
                            codigo_final.Add("\n\tLDR r3, " + var2);
                            codigo_final.Add("\n\tLDR r3, [r3]");
                        }
                        else
                            codigo_final.Add("\n\n\tMOV r3, #" + var2);

                        codigo_final.Add("\n\tADD r4, r2, r3");
                        codigo_final.Add("\n\tLDR r1, =" + variavel_atual);
                        codigo_final.Add("\n\tSTR r4, [r1]\n");
                        i = i + 3;
                        acoes.Add("Convertido token adiçao");
                    }
                    else if (codigo[i + 2].Lexema == "-")
                    {
                        String variavel_atual = codigo[i - 1].Lexema;
                        codigo_final.RemoveAt(codigo_final.Count - 1);

                        if (bandeira_var1 == 1)
                        {
                            codigo_final.Add("\n\n\tLDR r2, " + var1);
                            codigo_final.Add("\n\tLDR r2, [r2]");
                        }
                        else
                            codigo_final.Add("\n\n\tMOV r2, #" + var1);

                        if (bandeira_var2 == 1)
                        {
                            codigo_final.Add("\n\tLDR r3, " + var2);
                            codigo_final.Add("\n\tLDR r3, [r3]");
                        }
                        else
                            codigo_final.Add("\n\n\tMOV r3, #" + var2);

                        codigo_final.Add("\n\tSUB r4, r2, r3");
                        codigo_final.Add("\n\tLDR r1, =" + variavel_atual);
                        codigo_final.Add("\n\tSTR r4, [r1]\n");
                        i = i + 3;
                        acoes.Add("Convertido token subtracao");
                    }
                    else if (codigo[i + 2].Lexema == "*")
                    {
                        String variavel_atual = codigo[i - 1].Lexema;
                        codigo_final.RemoveAt(codigo_final.Count - 1);

                        if (bandeira_var1 == 1)
                        {
                            codigo_final.Add("\n\n\tLDR r2, " + var1);
                            codigo_final.Add("\n\tLDR r2, [r2]");
                        }
                        else
                            codigo_final.Add("\n\n\tMOV r2, #" + var1);

                        if (bandeira_var2 == 1)
                        {
                            codigo_final.Add("\n\tLDR r3, " + var2);
                            codigo_final.Add("\n\tLDR r3, [r3]");
                        }
                        else
                            codigo_final.Add("\n\n\tMOV r3, #" + var2);

                        codigo_final.Add("\n\tMUL r4, r2, r3");
                        codigo_final.Add("\n\tLDR r1, =" + variavel_atual);
                        codigo_final.Add("\n\tSTR r4, [r1]\n");
                        i = i + 3;
                        acoes.Add("Convertido token subtracao");
                    }
                    else
                    {
                        String variavel_atual = codigo[i - 1].Lexema;
                        codigo_final.RemoveAt(codigo_final.Count - 1);

                        if (bandeira_var1 == 1)
                        {
                            codigo_final.Add("\n\n\tLDR r2, " + var1);
                            codigo_final.Add("\n\tLDR r2, [r2]");
                        }
                        else
                            codigo_final.Add("\n\n\tMOV r2, #" + var1);
                        
                        codigo_final.Add("\n\tLDR r1, =" + variavel_atual);
                        codigo_final.Add("\n\tSTR r2, [r1]\n");
                        i = i + 2;
                        acoes.Add("Convertido token atribuicao");
                    }
                }
                else if(codigo[i].Tipo_token == "fim_programa")
                {
                    codigo_final.Add("\nfinal:");
                    codigo_final.Add("\n\tMOV r7, #1");
                    codigo_final.Add("\n\tSWI 0\n");
                    codigo_final.Add("\naddr_pattern: .word padraoLeitura \naddr_lr_bu: .word lr_bu \n.global scanf \n.global printf ");
                    acoes.Add("Fim do Programa");
                }
                else
                    codigo_final.Add(codigo[i].Lexema);
            }
            acoes.Add("Fim Geração Código Final");
            return new Tuple<List<String>, List<String>>(codigo_final, acoes);
        }
    }
}
