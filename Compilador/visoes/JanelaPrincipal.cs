using Compilador.controles;
using Compilador.modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class Compilador : Form
    {
        String programa;
        List<Token> lista_de_tokens = null;
        public Compilador()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Exibe a janela ao usuário para que seja selecionado o arquivo de código fonte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lerArquivo(object sender, EventArgs e)
        {            
            abrir_arquivo.Filter = "Arquivo texto|*.txt";
            abrir_arquivo.Title = "Selecione um arquivo txt";

            if(abrir_arquivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                caixa_console.Clear();
                System.IO.StreamReader sr = new System.IO.StreamReader(abrir_arquivo.FileName);
                programa = sr.ReadToEnd(); 
                sr.Close();
                caixa_console.AppendText(programa);
            }
            else
            {
                MessageBox.Show("Arquivo não existe");
            }
        }

        private void caixa_console_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button_compilar_Click(object sender, EventArgs e)
        {
            caixa_console.Clear();
            bool bandeira_erro= false;
            AnaliseLexica analisadorLexico = new AnaliseLexica(programa);
            Tuple<List<Token>, List<Erro>> token_erro = analisadorLexico.Analisar();
            this.lista_de_tokens = new List<Token>(token_erro.Item1);

            if (listar_tokens.Checked)
            {
                int maior = 0;

                foreach(var token in token_erro.Item1)
                {
                    if (token.Lexema.Length > maior)
                        maior = token.Lexema.Length;
                }
                caixa_console.AppendText(String.Format("| {0, -20} | {1, -" + maior + "} | {2, -7} | {3, -7} | \n", "Token", "Lexema", "Linha", "Coluna"));
                caixa_console.AppendText(String.Format("| {0, -20} | {1, -" + maior + "} | {2, -7} | {3, -7} | \n", "---------------", "----------", "-------", "-------"));
                foreach (var token in token_erro.Item1)
                {
                    caixa_console.AppendText(String.Format("| {0, -20} | {1, -" + maior + "} | {2, -7} | {3, -7} | \n", token.Tipo_token, token.Lexema, token.Linha, token.Coluna));
                }
                caixa_console.AppendText("\n\n____________________________________________________\n");
                caixa_console.AppendText("Análise Léxica Concluída!\n");
            }
            else
            {
                caixa_console.AppendText("Análise Léxica Concluída!\n");
                caixa_console.AppendText("____________________________________________________\n");
            }
            foreach (var erro in token_erro.Item2)
            {
                if(erro.tipo != "")
                    caixa_console.AppendText(erro.ToString());
                if (erro.erro_alerta == 1)
                    bandeira_erro = true;
            }
            
            if (!bandeira_erro)
                AnalisarSintatica(token_erro.Item1);
        }

        private void Compilador_Load(object sender, EventArgs e)
        {

        }

        private void AnalisarSintatica(List<Token> lista_tokens)
        {
            bool bandeira_erro = false;
            AnaliseSintatica analise = new AnaliseSintatica(lista_tokens);
            Tuple<Erro, List<String>, List<String>> lista_erro = analise.Analisar();

            if (lista_erro.Item1 == null)
            {
                caixa_console.AppendText("Análise Sintática Concluída!\n");
                if (Producoes.Checked)
                {
                    caixa_console.AppendText("Sequencia de Produções: ");
                    foreach (var producao in lista_erro.Item2)
                    {
                        caixa_console.AppendText(" " + producao);
                    }
                }                           
            }            
            else
            {
                caixa_console.AppendText(lista_erro.Item1.ToString());
                bandeira_erro = true;
            }
            caixa_console.AppendText("\n____________________________________________________\n");

            if (Log.Checked)
            {
                foreach (var acoes in lista_erro.Item3)
                {
                    caixa_console.AppendText(acoes);
                }
            }

            if (!bandeira_erro)
                AnalisarSemantica();
        }

        private void AnalisarSemantica()
        {
            AnaliseSemantica analise = new AnaliseSemantica(lista_de_tokens);
            Tuple<List<Erro>, List<String>, List<String>> tokens_erros = analise.Analisar();

            if (tokens_erros.Item1.Count == 0)
            {
                caixa_console.AppendText("Análise Semântica Concluída!\n");                
            }

            else
            {
                foreach(var erro in tokens_erros.Item1)
                {
                    caixa_console.AppendText(erro.ToString());
                }
            }

            caixa_console.AppendText("\n____________________________________________________\n\n");

            if (logSemantico.Checked && tokens_erros.Item2 != null)
            {
                caixa_console.AppendText("\nSequencia de Produções: ");
                foreach (var producao in tokens_erros.Item2)
                {
                    caixa_console.AppendText("\n" + producao);
                }
            }

            if (tokens_erros.Item1.Count == 0)
            {
                CodigoIntermediario(tokens_erros.Item3);
            }
        }

        private void CodigoIntermediario(List<String> lista_variaveis)
        {
            Intermediario codigo_intermediario = new Intermediario(lista_de_tokens, lista_variaveis);
            Tuple<List<String>, List<String>, List<Token>> intermediario = codigo_intermediario.GerarCodigo();
                       
            CodigoIntermediarioRaspberry(intermediario.Item2, intermediario.Item3, intermediario.Item1);
        }

        private void CodigoIntermediarioRaspberry(List<String> lista_variaveis, List<Token> codigo_intermediario, List<String> acoes)
        {
            IntermediarioRaspberry codigo_intermediario_raspberry = new IntermediarioRaspberry(codigo_intermediario, acoes);
            Tuple<List<Token>, List<String>, List<String>> raspberry = codigo_intermediario_raspberry.GerarCodigo();

            foreach(var token in raspberry.Item1)
            {
                if (token.Tipo_token == "final_linha" || token.Tipo_token == "inicio" || token.Tipo_token == "fim" || token.Tipo_token == "abrir_chave")
                    caixa_console.AppendText(token.Lexema + "\n");
                else
                    caixa_console.AppendText(token.Lexema + " ");
            }
            CodigoFinal(raspberry.Item1, lista_variaveis, raspberry.Item3);
        }

        private void CodigoFinal(List<Token> codigo_intermediario, List<String> lista_variavel, List<String> lista_mensagens)
        {
            FinalRaspberry codigo_intermediario_raspberry = new FinalRaspberry(codigo_intermediario, lista_variavel, lista_mensagens);
            Tuple<List<Token>, List<String>> raspberry = codigo_intermediario_raspberry.GerarCodigo();

            foreach (var token in raspberry.Item1)
            {
                if (token.Tipo_token == "final_linha" || token.Tipo_token == "inicio" || token.Tipo_token == "fim" || token.Tipo_token == "abrir_chave")
                    caixa_console.AppendText(token.Lexema + "\n");
                else
                    caixa_console.AppendText(token.Lexema + " ");
            }
        }

            private void Log_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void logSemantico_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
