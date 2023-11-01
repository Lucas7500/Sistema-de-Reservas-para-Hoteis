namespace Sistema_de_Reservas_para_Hoteis
{
    partial class JanelaPrincipal
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
            BotaoAdicionar = new Button();
            BotaoEditar = new Button();
            BotaoDeletar = new Button();
            TelaDaLista = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)TelaDaLista).BeginInit();
            SuspendLayout();
            // 
            // BotaoAdicionar
            // 
            BotaoAdicionar.Location = new Point(508, 408);
            BotaoAdicionar.Name = "BotaoAdicionar";
            BotaoAdicionar.Size = new Size(100, 30);
            BotaoAdicionar.TabIndex = 1;
            BotaoAdicionar.Text = "Adicionar";
            BotaoAdicionar.UseVisualStyleBackColor = true;
            BotaoAdicionar.Click += BotaoAdicionar_Click;
            // 
            // BotaoEditar
            // 
            BotaoEditar.Location = new Point(634, 408);
            BotaoEditar.Name = "BotaoEditar";
            BotaoEditar.Size = new Size(100, 30);
            BotaoEditar.TabIndex = 2;
            BotaoEditar.Text = "Editar";
            BotaoEditar.UseVisualStyleBackColor = true;
            // 
            // BotaoDeletar
            // 
            BotaoDeletar.Location = new Point(752, 408);
            BotaoDeletar.Name = "BotaoDeletar";
            BotaoDeletar.Size = new Size(100, 30);
            BotaoDeletar.TabIndex = 3;
            BotaoDeletar.Text = "Deletar";
            BotaoDeletar.UseVisualStyleBackColor = true;
            // 
            // TelaDaLista
            // 
            TelaDaLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            TelaDaLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TelaDaLista.Location = new Point(12, 12);
            TelaDaLista.Name = "TelaDaLista";
            TelaDaLista.RowTemplate.Height = 25;
            TelaDaLista.Size = new Size(840, 380);
            TelaDaLista.TabIndex = 4;
            // 
            // JanelaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(863, 450);
            Controls.Add(TelaDaLista);
            Controls.Add(BotaoDeletar);
            Controls.Add(BotaoEditar);
            Controls.Add(BotaoAdicionar);
            Name = "JanelaPrincipal";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)TelaDaLista).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button BotaoAdicionar;
        private Button BotaoEditar;
        private Button BotaoDeletar;
        private DataGridView TelaDaLista;
    }
}