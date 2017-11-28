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
        List<String> variaveis_inicializadas = new List<String>();
        List<String> acoes = new List<String>();
        List<String[]> valores_atribuicoes = new List<String[]>();
        List<Erro> erros = new List<Erro>();

        public AnaliseSemantica(List<Token> lista_tokens)
        {
            this.tokens = lista_tokens;
        }

        public Tuple<List<Erro>, List<String>> Analisar()
        {
            int i = 0;
            foreach (var token in tokens)
            {
                if (token.Tipo_token == "variavel")
                {
                    if (tokens[i - 1].Tipo_token == "tipo_variavel")
                        DeclararVariavel(token);
                    else if (tokens[i + 1].Tipo_token == "atribuicao")
                    {
                        VerificarVariavel(token);
                        acoes.Add("Variavel '" + token.Lexema + "' inicializada");
                        variaveis_inicializadas.Add(token.Lexema);
                        if(tokens[i + 2].Tipo_token == "variavel_numerica" && tokens[i + 3].Tipo_token == "final_linha")
                        {
                            String[] variavel = new String[2];
                            variavel[0] = token.Lexema;
                            variavel[1] = tokens[i + 2].Lexema;
                            valores_atribuicoes.Add(variavel);
                        }
                    }
                    else if (tokens[i - 1].Tipo_token == "leia")
                    {
                        VerificarVariavel(token);
                        acoes.Add("Variavel '" + token.Lexema + "' inicializada");
                        variaveis_inicializadas.Add(token.Lexema);
                    }
                    else
                    {
                        if (tokens[i - 1].Tipo_token != "programa")
                        {
                            VerificarVariavel(token);
                            bool inicializada = false;
                            foreach (var variavel in variaveis_inicializadas)
                            {
                                if (token.Lexema == variavel)
                                    inicializada = true;
                            }
                            if (inicializada)
                                acoes.Add("Variavel '" + token.Lexema + "' utilizada");
                            else
                                erros.Add(new Erro("Erro Semantico:", "Variavel '" + token.Lexema + "' não inicializada: ", token.Linha, token.Coluna, 1));
                        }
                    }
                }
                if (token.Tipo_token == "divisao")
                {
                    if(tokens[i+1].Tipo_token == "variavel" && tokens[i+2].Tipo_token == "final_linha")
                    {
                        foreach(var valor in valores_atribuicoes)
                        {
                            if(valor[0] == tokens[i+1].Lexema)
                                if(valor[1] == "0")
                                    erros.Add(new Erro("Erro Semantico:", "Divisão por Zero", token.Linha, token.Coluna, 1));
                        }
                    }
                    else if (tokens[i+1].Tipo_token == "variavel_numerica" && tokens[i+2].Tipo_token == "final_linha")
                    {
                        if(tokens[i + 1].Lexema == "0")
                            erros.Add(new Erro("Erro Semantico:", "Divisão por Zero", token.Linha, token.Coluna, 1));
                    }
                }
                i++;
            }

            return new Tuple<List<Erro>, List<String>>(erros, acoes);
        }

        public void DeclararVariavel(Token token)
        {
            bool bandeira_erro = false;

            if (lista_variaveis != null)
            {
                foreach (var variavel in lista_variaveis)
                {
                    if (token.Lexema == variavel)
                    {
                        bandeira_erro = true;
                        erros.Add(new Erro("Erro Semantico:", "Variavel '" + variavel + "' já declarada: ", token.Linha, token.Coluna, 1));
                    }
                }
                if (bandeira_erro == false)
                {
                    lista_variaveis.Add(token.Lexema);
                    acoes.Add("Variavel '" + token.Lexema + "' declarada");
                }
            }
            else
            {
                lista_variaveis.Add(token.Lexema);
                acoes.Add("Variavel '" + token.Lexema + "' declarada");
            }
        }

        public void VerificarVariavel(Token token)
        {
            bool declarada = false;
            foreach (var variavel in lista_variaveis)
            {
                if (variavel == token.Lexema)
                    declarada = true;
            }
            if (!declarada)
                erros.Add(new Erro("Erro Semantico:", "Variavel '" + token.Lexema + "' não declarada: ", token.Linha, token.Coluna, 1));
        }             
    }
}
