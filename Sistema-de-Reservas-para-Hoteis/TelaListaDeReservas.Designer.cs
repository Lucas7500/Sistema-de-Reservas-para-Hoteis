﻿using Dominio;

namespace Interacao
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            BotaoAdicionar = new Button();
            BotaoEditar = new Button();
            BotaoDeletar = new Button();
            TelaDaLista = new DataGridView();
            reservaBindingSource = new BindingSource(components);
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
            ((System.ComponentModel.ISupportInitialize)TelaDaLista).BeginInit();
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource).BeginInit();
            SuspendLayout();
            // 
            // BotaoAdicionar
            // 
            BotaoAdicionar.Location = new Point(490, 404);
            BotaoAdicionar.Name = "BotaoAdicionar";
            BotaoAdicionar.Size = new Size(110, 35);
            BotaoAdicionar.TabIndex = 1;
            BotaoAdicionar.Text = "Adicionar";
            BotaoAdicionar.UseVisualStyleBackColor = true;
            BotaoAdicionar.Click += AoClicarAbrirTelaDeCadastro;
            // 
            // BotaoEditar
            // 
            BotaoEditar.Location = new Point(615, 404);
            BotaoEditar.Name = "BotaoEditar";
            BotaoEditar.Size = new Size(110, 35);
            BotaoEditar.TabIndex = 2;
            BotaoEditar.Text = "Editar";
            BotaoEditar.UseVisualStyleBackColor = true;
            BotaoEditar.Click += AoClicarEditarElementoSelecionado;
            // 
            // BotaoDeletar
            // 
            BotaoDeletar.Location = new Point(741, 405);
            BotaoDeletar.Name = "BotaoDeletar";
            BotaoDeletar.Size = new Size(110, 35);
            BotaoDeletar.TabIndex = 3;
            BotaoDeletar.Text = "Deletar";
            BotaoDeletar.UseVisualStyleBackColor = true;
            BotaoDeletar.Click += AoClicarDeletarElementoSelecionado;
            // 
            // TelaDaLista
            // 
            TelaDaLista.AllowUserToAddRows = false;
            TelaDaLista.AllowUserToResizeColumns = false;
            TelaDaLista.AllowUserToResizeRows = false;
            TelaDaLista.AutoGenerateColumns = false;
            TelaDaLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            TelaDaLista.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            TelaDaLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TelaDaLista.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, nomeDataGridViewTextBoxColumn, cpfDataGridViewTextBoxColumn, telefoneDataGridViewTextBoxColumn, idadeDataGridViewTextBoxColumn, sexoDataGridViewTextBoxColumn, checkInDataGridViewTextBoxColumn, checkOutDataGridViewTextBoxColumn, precoEstadiaDataGridViewTextBoxColumn, pagamentoEfetuadoDataGridViewCheckBoxColumn });
            TelaDaLista.DataSource = reservaBindingSource;
            TelaDaLista.Location = new Point(8, 18);
            TelaDaLista.Name = "TelaDaLista";
            TelaDaLista.ReadOnly = true;
            TelaDaLista.RowTemplate.Height = 25;
            TelaDaLista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TelaDaLista.Size = new Size(843, 381);
            TelaDaLista.TabIndex = 4;
            // 
            // reservaBindingSource
            // 
            reservaBindingSource.DataSource = typeof(Reserva);
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.FillWeight = 49.0693741F;
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.MinimumWidth = 40;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nomeDataGridViewTextBoxColumn
            // 
            nomeDataGridViewTextBoxColumn.DataPropertyName = "Nome";
            nomeDataGridViewTextBoxColumn.FillWeight = 558.3756F;
            nomeDataGridViewTextBoxColumn.HeaderText = "Nome";
            nomeDataGridViewTextBoxColumn.MinimumWidth = 110;
            nomeDataGridViewTextBoxColumn.Name = "nomeDataGridViewTextBoxColumn";
            nomeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cpfDataGridViewTextBoxColumn
            // 
            cpfDataGridViewTextBoxColumn.DataPropertyName = "Cpf";
            cpfDataGridViewTextBoxColumn.FillWeight = 49.0693741F;
            cpfDataGridViewTextBoxColumn.HeaderText = "CPF";
            cpfDataGridViewTextBoxColumn.MinimumWidth = 85;
            cpfDataGridViewTextBoxColumn.Name = "cpfDataGridViewTextBoxColumn";
            cpfDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // telefoneDataGridViewTextBoxColumn
            // 
            telefoneDataGridViewTextBoxColumn.DataPropertyName = "Telefone";
            telefoneDataGridViewTextBoxColumn.FillWeight = 49.0693741F;
            telefoneDataGridViewTextBoxColumn.HeaderText = "Telefone";
            telefoneDataGridViewTextBoxColumn.MinimumWidth = 85;
            telefoneDataGridViewTextBoxColumn.Name = "telefoneDataGridViewTextBoxColumn";
            telefoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idadeDataGridViewTextBoxColumn
            // 
            idadeDataGridViewTextBoxColumn.DataPropertyName = "Idade";
            idadeDataGridViewTextBoxColumn.FillWeight = 49.0693741F;
            idadeDataGridViewTextBoxColumn.HeaderText = "Idade";
            idadeDataGridViewTextBoxColumn.MinimumWidth = 40;
            idadeDataGridViewTextBoxColumn.Name = "idadeDataGridViewTextBoxColumn";
            idadeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sexoDataGridViewTextBoxColumn
            // 
            sexoDataGridViewTextBoxColumn.DataPropertyName = "Sexo";
            sexoDataGridViewTextBoxColumn.FillWeight = 49.0693741F;
            sexoDataGridViewTextBoxColumn.HeaderText = "Sexo";
            sexoDataGridViewTextBoxColumn.MinimumWidth = 62;
            sexoDataGridViewTextBoxColumn.Name = "sexoDataGridViewTextBoxColumn";
            sexoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // checkInDataGridViewTextBoxColumn
            // 
            checkInDataGridViewTextBoxColumn.DataPropertyName = "CheckIn";
            checkInDataGridViewTextBoxColumn.FillWeight = 49.0693741F;
            checkInDataGridViewTextBoxColumn.HeaderText = "Check-in";
            checkInDataGridViewTextBoxColumn.MinimumWidth = 66;
            checkInDataGridViewTextBoxColumn.Name = "checkInDataGridViewTextBoxColumn";
            checkInDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // checkOutDataGridViewTextBoxColumn
            // 
            checkOutDataGridViewTextBoxColumn.DataPropertyName = "CheckOut";
            checkOutDataGridViewTextBoxColumn.FillWeight = 49.0693741F;
            checkOutDataGridViewTextBoxColumn.HeaderText = "Check-out";
            checkOutDataGridViewTextBoxColumn.MinimumWidth = 66;
            checkOutDataGridViewTextBoxColumn.Name = "checkOutDataGridViewTextBoxColumn";
            checkOutDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // precoEstadiaDataGridViewTextBoxColumn
            // 
            precoEstadiaDataGridViewTextBoxColumn.DataPropertyName = "PrecoEstadia";
            precoEstadiaDataGridViewTextBoxColumn.FillWeight = 49.0693741F;
            precoEstadiaDataGridViewTextBoxColumn.HeaderText = "Preço da Estadia";
            precoEstadiaDataGridViewTextBoxColumn.MinimumWidth = 76;
            precoEstadiaDataGridViewTextBoxColumn.Name = "precoEstadiaDataGridViewTextBoxColumn";
            precoEstadiaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pagamentoEfetuadoDataGridViewCheckBoxColumn
            // 
            pagamentoEfetuadoDataGridViewCheckBoxColumn.DataPropertyName = "PagamentoEfetuado";
            pagamentoEfetuadoDataGridViewCheckBoxColumn.FillWeight = 49.0693741F;
            pagamentoEfetuadoDataGridViewCheckBoxColumn.HeaderText = "Pagamento Efetuado";
            pagamentoEfetuadoDataGridViewCheckBoxColumn.MinimumWidth = 74;
            pagamentoEfetuadoDataGridViewCheckBoxColumn.Name = "pagamentoEfetuadoDataGridViewCheckBoxColumn";
            pagamentoEfetuadoDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // TelaListaDeReservas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(857, 451);
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
            ((System.ComponentModel.ISupportInitialize)reservaBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button BotaoAdicionar;
        private Button BotaoEditar;
        private Button BotaoDeletar;
        private BindingSource reservaBindingSource;
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