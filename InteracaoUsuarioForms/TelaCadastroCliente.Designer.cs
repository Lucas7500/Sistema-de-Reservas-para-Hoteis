namespace InteracaoUsuarioForms
{
    partial class TelaCadastroCliente
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
            BotaoFalse = new RadioButton();
            BotaoTrue = new RadioButton();
            TextoPreco = new TextBox();
            DataCheckOut = new DateTimePicker();
            DataCheckIn = new DateTimePicker();
            CaixaSexo = new ComboBox();
            BotaoAdicionarCadastro = new Button();
            BotaoCancelarCadastro = new Button();
            TextoCPF = new MaskedTextBox();
            TextoTelefone = new MaskedTextBox();
            TextoIdade = new TextBox();
            SinalDoReal = new TextBox();
            SuspendLayout();
            // 
            // LabelNome
            // 
            LabelNome.AutoSize = true;
            LabelNome.Location = new Point(12, 22);
            LabelNome.Name = "LabelNome";
            LabelNome.Size = new Size(40, 15);
            LabelNome.TabIndex = 0;
            LabelNome.Text = "Nome";
            // 
            // LabelSexo
            // 
            LabelSexo.AutoSize = true;
            LabelSexo.Location = new Point(174, 93);
            LabelSexo.Name = "LabelSexo";
            LabelSexo.Size = new Size(32, 15);
            LabelSexo.TabIndex = 1;
            LabelSexo.Text = "Sexo";
            // 
            // LabelTelefone
            // 
            LabelTelefone.AutoSize = true;
            LabelTelefone.Location = new Point(174, 56);
            LabelTelefone.Name = "LabelTelefone";
            LabelTelefone.Size = new Size(51, 15);
            LabelTelefone.TabIndex = 2;
            LabelTelefone.Text = "Telefone";
            // 
            // LabelIdade
            // 
            LabelIdade.AutoSize = true;
            LabelIdade.Location = new Point(12, 93);
            LabelIdade.Name = "LabelIdade";
            LabelIdade.Size = new Size(36, 15);
            LabelIdade.TabIndex = 3;
            LabelIdade.Text = "Idade";
            // 
            // LabelCheckOut
            // 
            LabelCheckOut.AutoSize = true;
            LabelCheckOut.Location = new Point(174, 130);
            LabelCheckOut.Name = "LabelCheckOut";
            LabelCheckOut.Size = new Size(63, 15);
            LabelCheckOut.TabIndex = 4;
            LabelCheckOut.Text = "Check-out";
            // 
            // LabelCheckIn
            // 
            LabelCheckIn.AutoSize = true;
            LabelCheckIn.Location = new Point(12, 130);
            LabelCheckIn.Name = "LabelCheckIn";
            LabelCheckIn.Size = new Size(55, 15);
            LabelCheckIn.TabIndex = 5;
            LabelCheckIn.Text = "Check-in";
            // 
            // LabelPreco
            // 
            LabelPreco.AutoSize = true;
            LabelPreco.Location = new Point(12, 166);
            LabelPreco.Name = "LabelPreco";
            LabelPreco.Size = new Size(37, 15);
            LabelPreco.TabIndex = 6;
            LabelPreco.Text = "Preço";
            // 
            // LabelCPF
            // 
            LabelCPF.AutoSize = true;
            LabelCPF.Location = new Point(12, 56);
            LabelCPF.Name = "LabelCPF";
            LabelCPF.Size = new Size(28, 15);
            LabelCPF.TabIndex = 7;
            LabelCPF.Text = "CPF";
            // 
            // LabelPagamento
            // 
            LabelPagamento.AutoSize = true;
            LabelPagamento.Location = new Point(174, 167);
            LabelPagamento.Name = "LabelPagamento";
            LabelPagamento.Size = new Size(58, 15);
            LabelPagamento.TabIndex = 8;
            LabelPagamento.Text = "Foi pago?";
            // 
            // TextoNome
            // 
            TextoNome.Location = new Point(84, 19);
            TextoNome.Name = "TextoNome";
            TextoNome.Size = new Size(249, 23);
            TextoNome.TabIndex = 1;
            TextoNome.KeyPress += PermitirApenasLetrasNoNome;
            // 
            // BotaoFalse
            // 
            BotaoFalse.AutoSize = true;
            BotaoFalse.Checked = true;
            BotaoFalse.Location = new Point(286, 168);
            BotaoFalse.Name = "BotaoFalse";
            BotaoFalse.Size = new Size(47, 19);
            BotaoFalse.TabIndex = 10;
            BotaoFalse.TabStop = true;
            BotaoFalse.Text = "Não";
            BotaoFalse.UseVisualStyleBackColor = true;
            // 
            // BotaoTrue
            // 
            BotaoTrue.AutoSize = true;
            BotaoTrue.Location = new Point(235, 167);
            BotaoTrue.Name = "BotaoTrue";
            BotaoTrue.Size = new Size(45, 19);
            BotaoTrue.TabIndex = 9;
            BotaoTrue.Text = "Sim";
            BotaoTrue.UseVisualStyleBackColor = true;
            // 
            // TextoPreco
            // 
            TextoPreco.Location = new Point(102, 164);
            TextoPreco.Name = "TextoPreco";
            TextoPreco.Size = new Size(66, 23);
            TextoPreco.TabIndex = 8;
            TextoPreco.KeyPress += PermitirApenasDecimaisNoPrecoDaEstadia;
            // 
            // DataCheckOut
            // 
            DataCheckOut.Format = DateTimePickerFormat.Short;
            DataCheckOut.Location = new Point(243, 124);
            DataCheckOut.Name = "DataCheckOut";
            DataCheckOut.Size = new Size(90, 23);
            DataCheckOut.TabIndex = 7;
            // 
            // DataCheckIn
            // 
            DataCheckIn.Format = DateTimePickerFormat.Short;
            DataCheckIn.Location = new Point(84, 124);
            DataCheckIn.Name = "DataCheckIn";
            DataCheckIn.Size = new Size(84, 23);
            DataCheckIn.TabIndex = 6;
            // 
            // CaixaSexo
            // 
            CaixaSexo.DropDownStyle = ComboBoxStyle.DropDownList;
            CaixaSexo.FormattingEnabled = true;
            CaixaSexo.Location = new Point(233, 90);
            CaixaSexo.Name = "CaixaSexo";
            CaixaSexo.Size = new Size(100, 23);
            CaixaSexo.TabIndex = 5;
            // 
            // BotaoAdicionarCadastro
            // 
            BotaoAdicionarCadastro.Location = new Point(12, 224);
            BotaoAdicionarCadastro.Name = "BotaoAdicionarCadastro";
            BotaoAdicionarCadastro.Size = new Size(90, 30);
            BotaoAdicionarCadastro.TabIndex = 11;
            BotaoAdicionarCadastro.Text = "Adicionar";
            BotaoAdicionarCadastro.UseVisualStyleBackColor = true;
            BotaoAdicionarCadastro.Click += AoClicarAdicionarCadastro;
            // 
            // BotaoCancelarCadastro
            // 
            BotaoCancelarCadastro.Location = new Point(267, 224);
            BotaoCancelarCadastro.Name = "BotaoCancelarCadastro";
            BotaoCancelarCadastro.Size = new Size(90, 30);
            BotaoCancelarCadastro.TabIndex = 12;
            BotaoCancelarCadastro.Text = "Cancelar";
            BotaoCancelarCadastro.UseVisualStyleBackColor = true;
            BotaoCancelarCadastro.Click += AoClicarCancelarCadastro;
            // 
            // TextoCPF
            // 
            TextoCPF.Culture = new System.Globalization.CultureInfo("en-US");
            TextoCPF.ImeMode = ImeMode.NoControl;
            TextoCPF.Location = new Point(84, 53);
            TextoCPF.Mask = "000.000.000-00";
            TextoCPF.Name = "TextoCPF";
            TextoCPF.Size = new Size(84, 23);
            TextoCPF.TabIndex = 2;
            // 
            // TextoTelefone
            // 
            TextoTelefone.Location = new Point(233, 53);
            TextoTelefone.Mask = "(00) 00000-0000";
            TextoTelefone.Name = "TextoTelefone";
            TextoTelefone.Size = new Size(100, 23);
            TextoTelefone.TabIndex = 3;
            // 
            // TextoIdade
            // 
            TextoIdade.Location = new Point(84, 90);
            TextoIdade.MaxLength = 3;
            TextoIdade.Name = "TextoIdade";
            TextoIdade.Size = new Size(84, 23);
            TextoIdade.TabIndex = 4;
            TextoIdade.KeyPress += PermitirApenasNumerosNaIdade;
            // 
            // SinalDoReal
            // 
            SinalDoReal.BackColor = Color.Gainsboro;
            SinalDoReal.Location = new Point(84, 164);
            SinalDoReal.Name = "SinalDoReal";
            SinalDoReal.PlaceholderText = "R$";
            SinalDoReal.ReadOnly = true;
            SinalDoReal.Size = new Size(20, 23);
            SinalDoReal.TabIndex = 17;
            SinalDoReal.Text = "R$";
            // 
            // TelaCadastroCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(369, 266);
            ControlBox = false;
            Controls.Add(SinalDoReal);
            Controls.Add(TextoIdade);
            Controls.Add(BotaoFalse);
            Controls.Add(CaixaSexo);
            Controls.Add(BotaoTrue);
            Controls.Add(TextoCPF);
            Controls.Add(DataCheckOut);
            Controls.Add(LabelPagamento);
            Controls.Add(LabelCheckOut);
            Controls.Add(TextoPreco);
            Controls.Add(LabelPreco);
            Controls.Add(TextoTelefone);
            Controls.Add(LabelCPF);
            Controls.Add(TextoNome);
            Controls.Add(LabelCheckIn);
            Controls.Add(LabelSexo);
            Controls.Add(DataCheckIn);
            Controls.Add(LabelNome);
            Controls.Add(LabelIdade);
            Controls.Add(LabelTelefone);
            Controls.Add(BotaoCancelarCadastro);
            Controls.Add(BotaoAdicionarCadastro);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TelaCadastroCliente";
            Text = "Cadastro";
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
        private TextBox TextoNome;
        private DateTimePicker DataCheckIn;
        private DateTimePicker DataCheckOut;
        private RadioButton BotaoFalse;
        private RadioButton BotaoTrue;
        private Button BotaoAdicionarCadastro;
        private Button BotaoCancelarCadastro;
        private TextBox TextoPreco;
        private ComboBox CaixaSexo;
        private MaskedTextBox TextoTelefone;
        private MaskedTextBox TextoCPF;
        private TextBox TextoIdade;
        private TextBox SinalDoReal;
    }
}