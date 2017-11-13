using Compilador.modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.controles
{
    class AnaliseSintatica
    {
        List<Token> tokens = new List<Token>();
        List<String> producoes = new List<String>();
        List<String> acoes = new List<String>();
        Erro erros = null;
        Stack<String> pilha = new Stack<String>();

        public AnaliseSintatica(List<Token> lista_tokens)
        {
            this.tokens = lista_tokens;
            acoes.Add("\nEmpilhado: <PROGRAMA>");
            pilha.Push("<PROGRAMA>");
        }

        public Tuple<Erro, List<String>, List<String>> Analisar()
        {
            while (erros == null && pilha.Count != 0 && tokens.Count != 0)
            {
                if (tokens[0].Tipo_token == pilha.Peek())
                {

                    acoes.Add("\nDesempilhado: " + pilha.Peek());
                    pilha.Pop();
                    tokens.RemoveAt(0);
                }

                else if ((pilha.Peek() == "<PROGRAMA>") && (tokens[0].Tipo_token == "programa"))
                    Producao0();

                else if ((pilha.Peek() == "<LISTA_COMANDOS>") && (tokens[0].Tipo_token == "fim"))
                    Producao5();

                else if ((pilha.Peek() == "<LISTA_COMANDOS>") && ((tokens[0].Tipo_token == "escreva") || (tokens[0].Tipo_token == "leia") || (tokens[0].Tipo_token == "condicional_entrada") || (tokens[0].Tipo_token == "repetir") || (tokens[0].Tipo_token == "variavel")))
                    Producao6();

                else if ((pilha.Peek() == "<DECLARACAO_VARIAVEL>") && (tokens[0].Tipo_token == "inicio"))
                    Producao2();

                else if ((pilha.Peek() == "<DECLARACAO_VARIAVEL>") && ((tokens[0].Tipo_token == "tipo_variavel") || (tokens[0].Tipo_token == "final_linha")))
                    Producao1();

                else if ((pilha.Peek() == "<LISTA_VARIAVEIS>") && (tokens[0].Tipo_token == "final_linha"))
                    Producao3();

                else if ((pilha.Peek() == "<LISTA_VARIAVEIS>") && (tokens[0].Tipo_token == "tipo_variavel"))
                    Producao4();

                else if ((pilha.Peek() == "<COMANDO>") && (tokens[0].Tipo_token == "escreva"))
                    Producao7();

                else if ((pilha.Peek() == "<COMANDO>") && (tokens[0].Tipo_token == "leia"))
                    Producao8();

                else if ((pilha.Peek() == "<COMANDO>") && (tokens[0].Tipo_token == "condicional_entrada"))
                    Producao11();

                else if ((pilha.Peek() == "<COMANDO>") && (tokens[0].Tipo_token == "repetir"))
                    Producao10();

                else if ((pilha.Peek() == "<COMANDO>") && (tokens[0].Tipo_token == "variavel"))
                    Producao9();

                else if ((pilha.Peek() == "<TEXTO>") && (tokens[0].Tipo_token == "texto"))
                    Producao15();

                else if ((pilha.Peek() == "<CONTEUDO>") && (tokens[0].Tipo_token == "fechar"))
                    Producao12();

                else if ((pilha.Peek() == "<CONTEUDO>") && (tokens[0].Tipo_token == "variavel"))
                    Producao14();

                else if ((pilha.Peek() == "<CONTEUDO>") && (tokens[0].Tipo_token == "texto"))
                    Producao13();

                else if ((pilha.Peek() == "<OPERACAO>") && (tokens[0].Tipo_token == "abrir"))
                    Producao17();

                else if ((pilha.Peek() == "<OPERACAO>") && (tokens[0].Tipo_token == "variavelNumerica" || tokens[0].Tipo_token == "variavel"))
                    Producao16();

                else if ((pilha.Peek() == "<OP>") && (tokens[0].Tipo_token == "final_linha" || tokens[0].Tipo_token == "fechar"))
                    Producao20();

                else if ((pilha.Peek() == "<OP>") && (tokens[0].Tipo_token == "soma" || tokens[0].Tipo_token == "multiplicacao" || tokens[0].Tipo_token == "subtracao" || tokens[0].Tipo_token == "divisao"))
                    Producao21();

                else if ((pilha.Peek() == "<VARIAVEL>") && (tokens[0].Tipo_token == "variavelNumerica"))
                    Producao19();

                else if ((pilha.Peek() == "<VARIAVEL>") && (tokens[0].Tipo_token == "variavel"))
                    Producao18();

                else if ((pilha.Peek() == "<CONDICAO>") && (tokens[0].Tipo_token == "variavelNumerica" || tokens[0].Tipo_token == "variavel"))
                    Producao28();

                else if ((pilha.Peek() == "<SENAO>") && ((tokens[0].Tipo_token == "fim") || (tokens[0].Tipo_token == "escreva") || (tokens[0].Tipo_token == "leia") || (tokens[0].Tipo_token == "condicional_entrada") || (tokens[0].Tipo_token == "repetir") || (tokens[0].Tipo_token == "variavel")))
                    Producao26();

                else if ((pilha.Peek() == "<SENAO>") && (tokens[0].Tipo_token == "condicional_saida"))
                    Producao27();

                else if ((pilha.Peek() == "<ARITMETICA>") && (tokens[0].Tipo_token == "soma"))
                    Producao22();

                else if ((pilha.Peek() == "<ARITMETICA>") && (tokens[0].Tipo_token == "multiplicacao"))
                    Producao23();

                else if ((pilha.Peek() == "<ARITMETICA>") && (tokens[0].Tipo_token == "subtracao"))
                    Producao24();

                else if ((pilha.Peek() == "<ARITMETICA>") && (tokens[0].Tipo_token == "divisao"))
                    Producao25();

                else if ((pilha.Peek() == "<OPERADOR_LOGICO>") && (tokens[0].Tipo_token == "condicao_igual"))
                    Producao29();

                else if ((pilha.Peek() == "<OPERADOR_LOGICO>") && (tokens[0].Tipo_token == "condicao_diferente"))
                    Producao30();

                else
                    erros = new Erro("Erro Sintático", "Token inesperado: " + tokens[0].Lexema, tokens[0].Linha, (tokens[0].Coluna - tokens[0].Lexema.Length), 1);
            }

            if (pilha.Count == 0 && tokens.Count == 0)
                return new Tuple<Erro, List<String>, List<String>>(erros, producoes, acoes);

            else {
                erros = new Erro("Erro Sintático", "Código incompleto ", 0, 0, 1);
                return new Tuple<Erro, List<String>, List<String>>(erros, producoes, acoes);
            }

        }

        private void Producao0()
        {
            acoes.Add("\nOPERAÇÃO 0");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("fim");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<LISTA_COMANDOS>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("inicio");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<DECLARACAO_VARIAVEL>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("variavel");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("programa");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("0");
            Analisar();

        }

        private void Producao1()
        {
            acoes.Add("\nOPERAÇÃO 1");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("<DECLARACAO_VARIAVEL>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("final_linha");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<LISTA_VARIAVEIS>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("1");
            Analisar();
        }

        private void Producao2()
        {
            acoes.Add("\nOPERAÇÃO 2");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            producoes.Add("2");
            Analisar();
        }

        private void Producao3()
        {
            acoes.Add("\nOPERAÇÃO 3");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            producoes.Add("3");
            Analisar();
        }

        private void Producao4()
        {
            acoes.Add("\nOPERAÇÃO 4");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("variavel");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("tipo_variavel");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("4");
            Analisar();
        }

        private void Producao5()
        {
            acoes.Add("\nOPERAÇÃO 5");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            producoes.Add("5");
            Analisar();
        }

        private void Producao6()
        {
            acoes.Add("\nOPERAÇÃO 6");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("<LISTA_COMANDOS>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<COMANDO>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("6");
            Analisar();
        }

        private void Producao7()
        {
            acoes.Add("\nOPERAÇÃO 7");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("final_linha");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("fechar");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<CONTEUDO>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("abrir");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("escreva");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("7");
            Analisar();
        }

        private void Producao8()
        {
            acoes.Add("\nOPERAÇÃO 8");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("final_linha");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("variavel");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("leia");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("8");
            Analisar(); Analisar();
        }

        private void Producao9()
        {
            acoes.Add("\nOPERAÇÃO 9");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("final_linha");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<OPERACAO>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("atribuicao");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("variavel");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("9");
            Analisar();
        }

        private void Producao10()
        {
            acoes.Add("\nOPERAÇÃO 10");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("final_linha");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("fechar");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<CONDICAO>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("abrir");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("ateh");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("fim");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<LISTA_COMANDOS>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("abrir_chave");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("repetir");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("10");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            Analisar();
        }

        private void Producao11()
        {
            acoes.Add("\nOPERAÇÃO 11");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("<SENAO>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("fim");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<LISTA_COMANDOS>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("abrir_chave");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("fechar");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<CONDICAO>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("abrir");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("condicional_entrada");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("11");
            Analisar();
        }

        private void Producao12()
        {
            acoes.Add("\nOPERAÇÃO 12");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            producoes.Add("12");
            Analisar();
        }

        private void Producao13()
        {
            acoes.Add("\nOPERAÇÃO 13");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("<TEXTO>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("13");
            Analisar();
        }

        private void Producao14()
        {
            acoes.Add("\nOPERAÇÃO 14");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("variavel");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("14");
            Analisar();
        }
        private void Producao15()
        {
            acoes.Add("\nOPERAÇÃO 15");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("texto");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("15");
            Analisar();
        }

        private void Producao16()
        {
            acoes.Add("\nOPERAÇÃO 16");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("<OP>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<VARIAVEL>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("16");
            Analisar();
        }

        private void Producao17()
        {
            acoes.Add("\nOPERAÇÃO 17");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("<OP>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("fechar");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<OP>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<VARIAVEL>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("abrir");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("17");
            Analisar();
        }

        private void Producao18()
        {
            acoes.Add("\nOPERAÇÃO 18");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("variavel");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("18");
            Analisar();
        }

        private void Producao19()
        {
            acoes.Add("\nOPERAÇÃO 19");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("variavelNumerica");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("19");
            Analisar();
        }

        private void Producao20()
        {
            acoes.Add("\nOPERAÇÃO 20");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            producoes.Add("20");
            Analisar();
        }

        private void Producao21()
        {
            acoes.Add("\nOPERAÇÃO 21");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("<OPERACAO>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<ARITMETICA>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("21");
            Analisar();
        }

        private void Producao22()
        {
            acoes.Add("\nOPERAÇÃO 22");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("soma");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("22");
            Analisar();
        }

        private void Producao23()
        {
            acoes.Add("\nOPERAÇÃO 23");
            pilha.Pop();
            acoes.Add("\nOPERAÇÃO 23");
            pilha.Push("subtracao");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("23");
            Analisar();
        }

        private void Producao24()
        {
            acoes.Add("\nOPERAÇÃO 24");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("multiplicacao");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("24");
            Analisar();
        }

        private void Producao25()
        {
            acoes.Add("\nOPERAÇÃO 25");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("divisao");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("25");
            Analisar();
        }

        private void Producao26()
        {
            acoes.Add("\nOPERAÇÃO 26");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            producoes.Add("26");
            Analisar();
        }

        private void Producao27()
        {
            acoes.Add("\nOPERAÇÃO 27");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("fim");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<LISTA_COMANDOS>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("abrir_chave");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("condicional_saida");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("27");
            Analisar();
        }

        private void Producao28()
        {
            acoes.Add("\nOPERAÇÃO 28");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("<VARIAVEL>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<OPERADOR_LOGICO>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            pilha.Push("<VARIAVEL>");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("28");
            Analisar();
        }

        private void Producao29()
        {
            acoes.Add("\nOPERAÇÃO 29");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("condicao_igual");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("29");
            Analisar();
        }

        private void Producao30()
        {
            acoes.Add("\nOPERAÇÃO 30");
            acoes.Add("\nDesempilhado: " + pilha.Peek());
            pilha.Pop();
            pilha.Push("condicao_diferente");
            acoes.Add("\nEmpilhado: " + pilha.Peek());
            producoes.Add("30");
            Analisar();
        }
    }
}
