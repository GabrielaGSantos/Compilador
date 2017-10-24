﻿namespace Compilador
{
    partial class Compilador
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_arquivo = new System.Windows.Forms.Button();
            this.listar_tokens = new System.Windows.Forms.CheckBox();
            this.caixa_console = new System.Windows.Forms.RichTextBox();
            this.button_compilar = new System.Windows.Forms.Button();
            this.abrir_arquivo = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // button_arquivo
            // 
            this.button_arquivo.Location = new System.Drawing.Point(45, 28);
            this.button_arquivo.Name = "button_arquivo";
            this.button_arquivo.Size = new System.Drawing.Size(128, 37);
            this.button_arquivo.TabIndex = 0;
            this.button_arquivo.Text = "Escolher Arquivo";
            this.button_arquivo.UseVisualStyleBackColor = true;
            this.button_arquivo.Click += new System.EventHandler(this.lerArquivo);
            // 
            // listar_tokens
            // 
            this.listar_tokens.AutoSize = true;
            this.listar_tokens.Location = new System.Drawing.Point(513, 39);
            this.listar_tokens.Name = "listar_tokens";
            this.listar_tokens.Size = new System.Drawing.Size(90, 17);
            this.listar_tokens.TabIndex = 1;
            this.listar_tokens.Text = "Listar Tokens";
            this.listar_tokens.UseVisualStyleBackColor = true;
            // 
            // caixa_console
            // 
            this.caixa_console.BackColor = System.Drawing.Color.Black;
            this.caixa_console.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.caixa_console.ForeColor = System.Drawing.Color.White;
            this.caixa_console.Location = new System.Drawing.Point(45, 107);
            this.caixa_console.Name = "caixa_console";
            this.caixa_console.ReadOnly = true;
            this.caixa_console.Size = new System.Drawing.Size(573, 331);
            this.caixa_console.TabIndex = 2;
            this.caixa_console.Text = "";
            this.caixa_console.TextChanged += new System.EventHandler(this.caixa_console_TextChanged);
            // 
            // button_compilar
            // 
            this.button_compilar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_compilar.Location = new System.Drawing.Point(228, 28);
            this.button_compilar.Name = "button_compilar";
            this.button_compilar.Size = new System.Drawing.Size(128, 37);
            this.button_compilar.TabIndex = 3;
            this.button_compilar.Text = "Compilar";
            this.button_compilar.UseVisualStyleBackColor = true;
            this.button_compilar.Click += new System.EventHandler(this.button_compilar_Click);
            // 
            // Compilador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 462);
            this.Controls.Add(this.button_compilar);
            this.Controls.Add(this.caixa_console);
            this.Controls.Add(this.listar_tokens);
            this.Controls.Add(this.button_arquivo);
            this.Name = "Compilador";
            this.Text = "Compilador";
            this.Load += new System.EventHandler(this.Compilador_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_arquivo;
        private System.Windows.Forms.CheckBox listar_tokens;
        private System.Windows.Forms.RichTextBox caixa_console;
        private System.Windows.Forms.Button button_compilar;
        private System.Windows.Forms.OpenFileDialog abrir_arquivo;
    }
}

