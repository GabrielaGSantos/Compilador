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

            if (listar_tokens.Checked)
            {
                int maior = 0;

                foreach(var token in token_erro.Item1)
                {
                    if (token.Lexema.Length > maior)
                        maior = token.Lexema.Length;
                }
                caixa_console.AppendText(String.Format("| {0, -20} | {1, -" + maior + "} | {2, -7} | {3, -7} | \n", "Token", "Lexema", "Linha", "Coluna"));
                caixa_console.AppendText(String.Format("| {0, -20} | {1, -" + maior + "} | {2, -7} | {3, -7} | \n", "---------------", "--------------", "-------", "-------"));
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
            
            if(!bandeira_erro)
                AnalisarSintatica(token_erro.Item1);
        }

        private void Compilador_Load(object sender, EventArgs e)
        {

        }

        private void AnalisarSintatica(List<Token> lista_tokens)
        {
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
            }

            caixa_console.AppendText("\n____________________________________________________\n\n");

            if (Log.Checked)
            {
                foreach (var acoes in lista_erro.Item3)
                {
                    caixa_console.AppendText(acoes);
                }
            }
            
        }
        
    }
}
