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
            TextoNome = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            TextoIdade = new TextBox();
            TextoTelefone = new TextBox();
            TextoCpf = new TextBox();
            DataCheckOut = new DateTimePicker();
            TextoPreco = new TextBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            BotaoFalse = new RadioButton();
            BotaoTrue = new RadioButton();
            DataCheckIn = new DateTimePicker();
            BotaoAdicionarCadastro = new Button();
            BotaoCancelarCadastro = new Button();
            CaixaSexo = new ComboBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // LabelNome
            // 
            LabelNome.AutoSize = true;
            LabelNome.Location = new Point(3, 0);
            LabelNome.Name = "LabelNome";
            LabelNome.Size = new Size(40, 15);
            LabelNome.TabIndex = 0;
            LabelNome.Text = "Nome";
            // 
            // LabelSexo
            // 
            LabelSexo.AutoSize = true;
            LabelSexo.Location = new Point(3, 156);
            LabelSexo.Name = "LabelSexo";
            LabelSexo.Size = new Size(32, 15);
            LabelSexo.TabIndex = 1;
            LabelSexo.Text = "Sexo";
            // 
            // LabelTelefone
            // 
            LabelTelefone.AutoSize = true;
            LabelTelefone.Location = new Point(3, 117);
            LabelTelefone.Name = "LabelTelefone";
            LabelTelefone.Size = new Size(51, 15);
            LabelTelefone.TabIndex = 2;
            LabelTelefone.Text = "Telefone";
            // 
            // LabelIdade
            // 
            LabelIdade.AutoSize = true;
            LabelIdade.Location = new Point(3, 78);
            LabelIdade.Name = "LabelIdade";
            LabelIdade.Size = new Size(36, 15);
            LabelIdade.TabIndex = 3;
            LabelIdade.Text = "Idade";
            // 
            // LabelCheckOut
            // 
            LabelCheckOut.AutoSize = true;
            LabelCheckOut.Location = new Point(3, 234);
            LabelCheckOut.Name = "LabelCheckOut";
            LabelCheckOut.Size = new Size(63, 15);
            LabelCheckOut.TabIndex = 4;
            LabelCheckOut.Text = "Check-out";
            // 
            // LabelCheckIn
            // 
            LabelCheckIn.AutoSize = true;
            LabelCheckIn.Location = new Point(3, 195);
            LabelCheckIn.Name = "LabelCheckIn";
            LabelCheckIn.Size = new Size(55, 15);
            LabelCheckIn.TabIndex = 5;
            LabelCheckIn.Text = "Check-in";
            // 
            // LabelPreco
            // 
            LabelPreco.AutoSize = true;
            LabelPreco.Location = new Point(3, 273);
            LabelPreco.Name = "LabelPreco";
            LabelPreco.Size = new Size(37, 15);
            LabelPreco.TabIndex = 6;
            LabelPreco.Text = "Preço";
            // 
            // LabelCPF
            // 
            LabelCPF.AutoSize = true;
            LabelCPF.Location = new Point(3, 39);
            LabelCPF.Name = "LabelCPF";
            LabelCPF.Size = new Size(28, 15);
            LabelCPF.TabIndex = 7;
            LabelCPF.Text = "CPF";
            // 
            // LabelPagamento
            // 
            LabelPagamento.AutoSize = true;
            LabelPagamento.Location = new Point(3, 311);
            LabelPagamento.Name = "LabelPagamento";
            LabelPagamento.Size = new Size(123, 15);
            LabelPagamento.TabIndex = 8;
            LabelPagamento.Text = "Pagamento Efetuado?";
            // 
            // TextoNome
            // 
            TextoNome.Location = new Point(136, 3);
            TextoNome.Name = "TextoNome";
            TextoNome.Size = new Size(251, 23);
            TextoNome.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.Controls.Add(CaixaSexo, 1, 4);
            tableLayoutPanel1.Controls.Add(TextoIdade, 1, 2);
            tableLayoutPanel1.Controls.Add(TextoTelefone, 1, 3);
            tableLayoutPanel1.Controls.Add(LabelPreco, 0, 7);
            tableLayoutPanel1.Controls.Add(TextoCpf, 1, 1);
            tableLayoutPanel1.Controls.Add(LabelNome, 0, 0);
            tableLayoutPanel1.Controls.Add(LabelSexo, 0, 4);
            tableLayoutPanel1.Controls.Add(LabelCPF, 0, 1);
            tableLayoutPanel1.Controls.Add(LabelTelefone, 0, 3);
            tableLayoutPanel1.Controls.Add(LabelIdade, 0, 2);
            tableLayoutPanel1.Controls.Add(LabelCheckOut, 0, 6);
            tableLayoutPanel1.Controls.Add(DataCheckOut, 1, 6);
            tableLayoutPanel1.Controls.Add(LabelCheckIn, 0, 5);
            tableLayoutPanel1.Controls.Add(TextoPreco, 1, 7);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 8);
            tableLayoutPanel1.Controls.Add(DataCheckIn, 1, 5);
            tableLayoutPanel1.Controls.Add(LabelPagamento, 0, 8);
            tableLayoutPanel1.Controls.Add(TextoNome, 1, 0);
            tableLayoutPanel1.Location = new Point(23, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1261053F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1261044F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1261044F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1261044F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1261044F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1261044F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1249933F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.0591917F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.0591917F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(446, 351);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // TextoIdade
            // 
            TextoIdade.Location = new Point(136, 81);
            TextoIdade.Name = "TextoIdade";
            TextoIdade.Size = new Size(251, 23);
            TextoIdade.TabIndex = 13;
            // 
            // TextoTelefone
            // 
            TextoTelefone.Location = new Point(136, 120);
            TextoTelefone.Name = "TextoTelefone";
            TextoTelefone.Size = new Size(251, 23);
            TextoTelefone.TabIndex = 12;
            // 
            // TextoCpf
            // 
            TextoCpf.Location = new Point(136, 42);
            TextoCpf.Name = "TextoCpf";
            TextoCpf.Size = new Size(251, 23);
            TextoCpf.TabIndex = 10;
            // 
            // DataCheckOut
            // 
            DataCheckOut.Location = new Point(136, 237);
            DataCheckOut.Name = "DataCheckOut";
            DataCheckOut.Size = new Size(251, 23);
            DataCheckOut.TabIndex = 14;
            // 
            // TextoPreco
            // 
            TextoPreco.Location = new Point(136, 276);
            TextoPreco.Name = "TextoPreco";
            TextoPreco.Size = new Size(251, 23);
            TextoPreco.TabIndex = 16;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(BotaoFalse, 0, 0);
            tableLayoutPanel3.Controls.Add(BotaoTrue, 0, 0);
            tableLayoutPanel3.Location = new Point(136, 314);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(251, 34);
            tableLayoutPanel3.TabIndex = 17;
            // 
            // BotaoFalse
            // 
            BotaoFalse.AutoSize = true;
            BotaoFalse.Location = new Point(128, 3);
            BotaoFalse.Name = "BotaoFalse";
            BotaoFalse.Size = new Size(47, 19);
            BotaoFalse.TabIndex = 15;
            BotaoFalse.TabStop = true;
            BotaoFalse.Text = "Não";
            BotaoFalse.UseVisualStyleBackColor = true;
            // 
            // BotaoTrue
            // 
            BotaoTrue.AutoSize = true;
            BotaoTrue.Location = new Point(3, 3);
            BotaoTrue.Name = "BotaoTrue";
            BotaoTrue.Size = new Size(45, 19);
            BotaoTrue.TabIndex = 14;
            BotaoTrue.TabStop = true;
            BotaoTrue.Text = "Sim";
            BotaoTrue.UseVisualStyleBackColor = true;
            // 
            // DataCheckIn
            // 
            DataCheckIn.Location = new Point(136, 198);
            DataCheckIn.Name = "DataCheckIn";
            DataCheckIn.Size = new Size(251, 23);
            DataCheckIn.TabIndex = 13;
            // 
            // BotaoAdicionarCadastro
            // 
            BotaoAdicionarCadastro.Location = new Point(668, 398);
            BotaoAdicionarCadastro.Name = "BotaoAdicionarCadastro";
            BotaoAdicionarCadastro.Size = new Size(120, 40);
            BotaoAdicionarCadastro.TabIndex = 11;
            BotaoAdicionarCadastro.Text = "Adicionar";
            BotaoAdicionarCadastro.UseVisualStyleBackColor = true;
            BotaoAdicionarCadastro.Click += BotaoAdicionarCadastro_Click;
            // 
            // BotaoCancelarCadastro
            // 
            BotaoCancelarCadastro.Location = new Point(12, 398);
            BotaoCancelarCadastro.Name = "BotaoCancelarCadastro";
            BotaoCancelarCadastro.Size = new Size(120, 40);
            BotaoCancelarCadastro.TabIndex = 12;
            BotaoCancelarCadastro.Text = "Cancelar";
            BotaoCancelarCadastro.UseVisualStyleBackColor = true;
            // 
            // CaixaSexo
            // 
            CaixaSexo.FormattingEnabled = true;
            CaixaSexo.Location = new Point(136, 159);
            CaixaSexo.Name = "CaixaSexo";
            CaixaSexo.Size = new Size(251, 23);
            CaixaSexo.TabIndex = 13;
            // 
            // CadastroCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BotaoCancelarCadastro);
            Controls.Add(BotaoAdicionarCadastro);
            Controls.Add(tableLayoutPanel1);
            Name = "CadastroCliente";
            Text = "CadastroCliente";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
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
        private TextBox TextoNome;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox TextoTelefone;
        private TextBox TextoCpf;
        private DateTimePicker DataCheckIn;
        private DateTimePicker DataCheckOut;
        private TextBox TextoPreco;
        private TableLayoutPanel tableLayoutPanel3;
        private RadioButton BotaoFalse;
        private RadioButton BotaoTrue;
        private Button BotaoAdicionarCadastro;
        private Button BotaoCancelarCadastro;
        private TextBox TextoIdade;
        private ComboBox CaixaSexo;
    }
}