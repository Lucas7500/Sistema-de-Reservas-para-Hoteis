namespace Sistema_de_Reservas_para_Hoteis
{
    partial class CadastroCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LabelNome = new Label();
            LabelSexo = new Label();
            LabelTelefone = new Label();
            LabelIdade = new Label();
            LabelCheckOut = new Label();
            LabelCheckIn = new Label();
            LabelPreco = new Label();
            LabelCPF = new Label();
            LabelPagamento = new Label();
            SuspendLayout();
            // 
            // LabelNome
            // 
            LabelNome.AutoSize = true;
            LabelNome.Location = new Point(35, 28);
            LabelNome.Name = "LabelNome";
            LabelNome.Size = new Size(40, 15);
            LabelNome.TabIndex = 0;
            LabelNome.Text = "Nome";
            // 
            // LabelSexo
            // 
            LabelSexo.AutoSize = true;
            LabelSexo.Location = new Point(35, 141);
            LabelSexo.Name = "LabelSexo";
            LabelSexo.Size = new Size(32, 15);
            LabelSexo.TabIndex = 1;
            LabelSexo.Text = "Sexo";
            // 
            // LabelTelefone
            // 
            LabelTelefone.AutoSize = true;
            LabelTelefone.Location = new Point(35, 110);
            LabelTelefone.Name = "LabelTelefone";
            LabelTelefone.Size = new Size(51, 15);
            LabelTelefone.TabIndex = 2;
            LabelTelefone.Text = "Telefone";
            // 
            // LabelIdade
            // 
            LabelIdade.AutoSize = true;
            LabelIdade.Location = new Point(35, 79);
            LabelIdade.Name = "LabelIdade";
            LabelIdade.Size = new Size(36, 15);
            LabelIdade.TabIndex = 3;
            LabelIdade.Text = "Idade";
            // 
            // LabelCheckOut
            // 
            LabelCheckOut.AutoSize = true;
            LabelCheckOut.Location = new Point(35, 209);
            LabelCheckOut.Name = "LabelCheckOut";
            LabelCheckOut.Size = new Size(63, 15);
            LabelCheckOut.TabIndex = 4;
            LabelCheckOut.Text = "Check-out";
            // 
            // LabelCheckIn
            // 
            LabelCheckIn.AutoSize = true;
            LabelCheckIn.Location = new Point(35, 175);
            LabelCheckIn.Name = "LabelCheckIn";
            LabelCheckIn.Size = new Size(55, 15);
            LabelCheckIn.TabIndex = 5;
            LabelCheckIn.Text = "Check-in";
            // 
            // LabelPreco
            // 
            LabelPreco.AutoSize = true;
            LabelPreco.Location = new Point(35, 244);
            LabelPreco.Name = "LabelPreco";
            LabelPreco.Size = new Size(37, 15);
            LabelPreco.TabIndex = 6;
            LabelPreco.Text = "Preço";
            // 
            // LabelCPF
            // 
            LabelCPF.AutoSize = true;
            LabelCPF.Location = new Point(35, 54);
            LabelCPF.Name = "LabelCPF";
            LabelCPF.Size = new Size(28, 15);
            LabelCPF.TabIndex = 7;
            LabelCPF.Text = "CPF";
            // 
            // LabelPagamento
            // 
            LabelPagamento.AutoSize = true;
            LabelPagamento.Location = new Point(35, 276);
            LabelPagamento.Name = "LabelPagamento";
            LabelPagamento.Size = new Size(123, 15);
            LabelPagamento.TabIndex = 8;
            LabelPagamento.Text = "Pagamento Efetuado?";
            // 
            // CadastroCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LabelPagamento);
            Controls.Add(LabelCPF);
            Controls.Add(LabelPreco);
            Controls.Add(LabelCheckIn);
            Controls.Add(LabelCheckOut);
            Controls.Add(LabelIdade);
            Controls.Add(LabelTelefone);
            Controls.Add(LabelSexo);
            Controls.Add(LabelNome);
            Name = "CadastroCliente";
            Text = "CadastroCliente";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelNome;
        private Label LabelSexo;
        private Label LabelTelefone;
        private Label LabelIdade;
        private Label LabelCheckOut;
        private Label LabelCheckIn;
        private Label LabelPreco;
        private Label LabelCPF;
        private Label LabelPagamento;
    }
}