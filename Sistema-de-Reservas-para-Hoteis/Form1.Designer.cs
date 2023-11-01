namespace Sistema_de_Reservas_para_Hoteis
{
    partial class Form1
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
            Adicionar = new Button();
            Editar = new Button();
            Deletar = new Button();
            dataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Adicionar
            // 
            Adicionar.Location = new Point(545, 408);
            Adicionar.Name = "Adicionar";
            Adicionar.Size = new Size(102, 30);
            Adicionar.TabIndex = 1;
            Adicionar.Text = "Adicionar";
            Adicionar.UseVisualStyleBackColor = true;
            Adicionar.Click += button1_Click;
            // 
            // Editar
            // 
            Editar.Location = new Point(670, 408);
            Editar.Name = "Editar";
            Editar.Size = new Size(98, 30);
            Editar.TabIndex = 2;
            Editar.Text = "Editar";
            Editar.UseVisualStyleBackColor = true;
            // 
            // Deletar
            // 
            Deletar.Location = new Point(786, 408);
            Deletar.Name = "Deletar";
            Deletar.Size = new Size(100, 30);
            Deletar.TabIndex = 3;
            Deletar.Text = "Deletar";
            Deletar.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 12);
            dataGridView.Name = "dataGridView";
            dataGridView.RowTemplate.Height = 25;
            dataGridView.Size = new Size(901, 380);
            dataGridView.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 450);
            Controls.Add(dataGridView);
            Controls.Add(Deletar);
            Controls.Add(Editar);
            Controls.Add(Adicionar);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button Adicionar;
        private Button Editar;
        private Button Deletar;
        private DataGridView dataGridView;
    }
}