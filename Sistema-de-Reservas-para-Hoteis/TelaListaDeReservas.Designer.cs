namespace Sistema_de_Reservas_para_Hoteis
{
    partial class TelaListaDeReservas
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            BotaoAdicionar = new Button();
            BotaoEditar = new Button();
            BotaoDeletar = new Button();
            TelaDaLista = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nomeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            cpfDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            telefoneDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idadeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            sexoDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            checkInDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            checkOutDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            precoEstadiaDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            pagamentoEfetuadoDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            reservaBindingSource3 = new BindingSource(components);
            reservaBindingSource = new BindingSource(components);
            reservaBindingSource1 = new BindingSource(components);
            reservaBindingSource2 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)TelaDaLista).BeginInit();
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource2).BeginInit();
            SuspendLayout();
            // 
            // BotaoAdicionar
            // 
            BotaoAdicionar.Location = new Point(439, 404);
            BotaoAdicionar.Name = "BotaoAdicionar";
            BotaoAdicionar.Size = new Size(110, 35);
            BotaoAdicionar.TabIndex = 1;
            BotaoAdicionar.Text = "Adicionar";
            BotaoAdicionar.UseVisualStyleBackColor = true;
            BotaoAdicionar.Click += AoClicarAdicionarAbrirTelaDeCadastro;
            // 
            // BotaoEditar
            // 
            BotaoEditar.Location = new Point(564, 404);
            BotaoEditar.Name = "BotaoEditar";
            BotaoEditar.Size = new Size(110, 35);
            BotaoEditar.TabIndex = 2;
            BotaoEditar.Text = "Editar";
            BotaoEditar.UseVisualStyleBackColor = true;
            BotaoEditar.Click += AoClicarEditarElementoSelecionado;
            // 
            // BotaoDeletar
            // 
            BotaoDeletar.Location = new Point(689, 404);
            BotaoDeletar.Name = "BotaoDeletar";
            BotaoDeletar.Size = new Size(110, 35);
            BotaoDeletar.TabIndex = 3;
            BotaoDeletar.Text = "Deletar";
            BotaoDeletar.UseVisualStyleBackColor = true;
            // 
            // TelaDaLista
            // 
            TelaDaLista.AllowUserToAddRows = false;
            TelaDaLista.AllowUserToResizeColumns = false;
            TelaDaLista.AllowUserToResizeRows = false;
            TelaDaLista.AutoGenerateColumns = false;
            TelaDaLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TelaDaLista.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, nomeDataGridViewTextBoxColumn, cpfDataGridViewTextBoxColumn, telefoneDataGridViewTextBoxColumn, idadeDataGridViewTextBoxColumn, sexoDataGridViewTextBoxColumn, checkInDataGridViewTextBoxColumn, checkOutDataGridViewTextBoxColumn, precoEstadiaDataGridViewTextBoxColumn, pagamentoEfetuadoDataGridViewCheckBoxColumn });
            TelaDaLista.DataSource = reservaBindingSource3;
            TelaDaLista.Location = new Point(12, 12);
            TelaDaLista.Name = "TelaDaLista";
            TelaDaLista.ReadOnly = true;
            TelaDaLista.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            TelaDaLista.RowTemplate.Height = 25;
            TelaDaLista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TelaDaLista.Size = new Size(787, 380);
            TelaDaLista.TabIndex = 4;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.FillWeight = 50.9854622F;
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            idDataGridViewTextBoxColumn.Width = 30;
            // 
            // nomeDataGridViewTextBoxColumn
            // 
            nomeDataGridViewTextBoxColumn.DataPropertyName = "Nome";
            nomeDataGridViewTextBoxColumn.FillWeight = 105.936462F;
            nomeDataGridViewTextBoxColumn.HeaderText = "Nome";
            nomeDataGridViewTextBoxColumn.Name = "nomeDataGridViewTextBoxColumn";
            nomeDataGridViewTextBoxColumn.ReadOnly = true;
            nomeDataGridViewTextBoxColumn.Width = 110;
            // 
            // cpfDataGridViewTextBoxColumn
            // 
            cpfDataGridViewTextBoxColumn.DataPropertyName = "Cpf";
            cpfDataGridViewTextBoxColumn.FillWeight = 105.936462F;
            cpfDataGridViewTextBoxColumn.HeaderText = "CPF";
            cpfDataGridViewTextBoxColumn.Name = "cpfDataGridViewTextBoxColumn";
            cpfDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // telefoneDataGridViewTextBoxColumn
            // 
            telefoneDataGridViewTextBoxColumn.DataPropertyName = "Telefone";
            telefoneDataGridViewTextBoxColumn.FillWeight = 105.936462F;
            telefoneDataGridViewTextBoxColumn.HeaderText = "Telefone";
            telefoneDataGridViewTextBoxColumn.Name = "telefoneDataGridViewTextBoxColumn";
            telefoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idadeDataGridViewTextBoxColumn
            // 
            idadeDataGridViewTextBoxColumn.DataPropertyName = "Idade";
            idadeDataGridViewTextBoxColumn.FillWeight = 101.522842F;
            idadeDataGridViewTextBoxColumn.HeaderText = "Idade";
            idadeDataGridViewTextBoxColumn.Name = "idadeDataGridViewTextBoxColumn";
            idadeDataGridViewTextBoxColumn.ReadOnly = true;
            idadeDataGridViewTextBoxColumn.Width = 50;
            // 
            // sexoDataGridViewTextBoxColumn
            // 
            sexoDataGridViewTextBoxColumn.DataPropertyName = "Sexo";
            sexoDataGridViewTextBoxColumn.FillWeight = 105.936462F;
            sexoDataGridViewTextBoxColumn.HeaderText = "Sexo";
            sexoDataGridViewTextBoxColumn.Name = "sexoDataGridViewTextBoxColumn";
            sexoDataGridViewTextBoxColumn.ReadOnly = true;
            sexoDataGridViewTextBoxColumn.Width = 70;
            // 
            // checkInDataGridViewTextBoxColumn
            // 
            checkInDataGridViewTextBoxColumn.DataPropertyName = "CheckIn";
            checkInDataGridViewTextBoxColumn.FillWeight = 105.936462F;
            checkInDataGridViewTextBoxColumn.HeaderText = "Check-In";
            checkInDataGridViewTextBoxColumn.Name = "checkInDataGridViewTextBoxColumn";
            checkInDataGridViewTextBoxColumn.ReadOnly = true;
            checkInDataGridViewTextBoxColumn.Width = 70;
            // 
            // checkOutDataGridViewTextBoxColumn
            // 
            checkOutDataGridViewTextBoxColumn.DataPropertyName = "CheckOut";
            checkOutDataGridViewTextBoxColumn.FillWeight = 105.936462F;
            checkOutDataGridViewTextBoxColumn.HeaderText = "Check-Out";
            checkOutDataGridViewTextBoxColumn.Name = "checkOutDataGridViewTextBoxColumn";
            checkOutDataGridViewTextBoxColumn.ReadOnly = true;
            checkOutDataGridViewTextBoxColumn.Width = 70;
            // 
            // precoEstadiaDataGridViewTextBoxColumn
            // 
            precoEstadiaDataGridViewTextBoxColumn.DataPropertyName = "PrecoEstadia";
            precoEstadiaDataGridViewTextBoxColumn.FillWeight = 105.936462F;
            precoEstadiaDataGridViewTextBoxColumn.HeaderText = "Preço da Estadia";
            precoEstadiaDataGridViewTextBoxColumn.Name = "precoEstadiaDataGridViewTextBoxColumn";
            precoEstadiaDataGridViewTextBoxColumn.ReadOnly = true;
            precoEstadiaDataGridViewTextBoxColumn.Width = 60;
            // 
            // pagamentoEfetuadoDataGridViewCheckBoxColumn
            // 
            pagamentoEfetuadoDataGridViewCheckBoxColumn.DataPropertyName = "PagamentoEfetuado";
            pagamentoEfetuadoDataGridViewCheckBoxColumn.FillWeight = 105.936462F;
            pagamentoEfetuadoDataGridViewCheckBoxColumn.HeaderText = "Pagamento Efetuado?";
            pagamentoEfetuadoDataGridViewCheckBoxColumn.Name = "pagamentoEfetuadoDataGridViewCheckBoxColumn";
            pagamentoEfetuadoDataGridViewCheckBoxColumn.ReadOnly = true;
            pagamentoEfetuadoDataGridViewCheckBoxColumn.Width = 84;
            // 
            // reservaBindingSource3
            // 
            reservaBindingSource3.DataSource = typeof(Reserva);
            // 
            // reservaBindingSource
            // 
            reservaBindingSource.DataSource = typeof(Reserva);
            // 
            // reservaBindingSource1
            // 
            reservaBindingSource1.DataSource = typeof(Reserva);
            // 
            // reservaBindingSource2
            // 
            reservaBindingSource2.DataSource = typeof(Reserva);
            // 
            // TelaListaDeReservas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(815, 451);
            Controls.Add(TelaDaLista);
            Controls.Add(BotaoDeletar);
            Controls.Add(BotaoEditar);
            Controls.Add(BotaoAdicionar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TelaListaDeReservas";
            Text = "Reserva de Hotel";
            ((System.ComponentModel.ISupportInitialize)TelaDaLista).EndInit();
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource3).EndInit();
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button BotaoAdicionar;
        private Button BotaoEditar;
        private Button BotaoDeletar;
        private BindingSource reservaBindingSource;
        private BindingSource reservaBindingSource3;
        private BindingSource reservaBindingSource1;
        private BindingSource reservaBindingSource2;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nomeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn cpfDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn telefoneDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idadeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sexoDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn checkInDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn checkOutDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn precoEstadiaDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn pagamentoEfetuadoDataGridViewCheckBoxColumn;
        private static DataGridView TelaDaLista;
    }
}