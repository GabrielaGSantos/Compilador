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
            AnaliseLexica analisadorLexico = new AnaliseLexica(programa, caixa_console);
            Tuple<List<Token>, List<Erro>> token_erro = analisadorLexico.Analisar();

            if (listar_tokens.Checked)
            {
                caixa_console.AppendText(String.Format("| {0, -10} | {1, -20} | {2, -7} | {3, -7} | \n", "Token", "Lexama", "Linha", "Coluna"));
                caixa_console.AppendText(String.Format("| {0, -10} | {1, -20} | {2, -7} | {3, -7} | \n", "----------", "--------------------", "-------", "-------"));
                foreach (var token in token_erro.Item1)
                {
                    caixa_console.AppendText(token.ToString());
                }
                caixa_console.AppendText("\n\n____________________________________________________\n");
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
            }
        }

        private void Compilador_Load(object sender, EventArgs e)
        {

        }
        
    }
}
