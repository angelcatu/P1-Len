namespace Proyecto1LFP
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualTécnicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualDeUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtBoxAnalalizador = new System.Windows.Forms.TextBox();
            this.txtBoxLog = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAnalizar = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inicioToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(547, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // inicioToolStripMenuItem
            // 
            this.inicioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.analizarToolStripMenuItem,
            this.ejecutarToolStripMenuItem});
            this.inicioToolStripMenuItem.Name = "inicioToolStripMenuItem";
            this.inicioToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.inicioToolStripMenuItem.Text = "Inicio";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualTécnicoToolStripMenuItem,
            this.manualDeUsuarioToolStripMenuItem,
            this.acercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como...";
            // 
            // analizarToolStripMenuItem
            // 
            this.analizarToolStripMenuItem.Name = "analizarToolStripMenuItem";
            this.analizarToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.analizarToolStripMenuItem.Text = "Analizar";
            this.analizarToolStripMenuItem.Click += new System.EventHandler(this.analizarToolStripMenuItem_Click);
            // 
            // ejecutarToolStripMenuItem
            // 
            this.ejecutarToolStripMenuItem.Name = "ejecutarToolStripMenuItem";
            this.ejecutarToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ejecutarToolStripMenuItem.Text = "Ejecutar";
            // 
            // manualTécnicoToolStripMenuItem
            // 
            this.manualTécnicoToolStripMenuItem.Name = "manualTécnicoToolStripMenuItem";
            this.manualTécnicoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.manualTécnicoToolStripMenuItem.Text = "Manual técnico";
            // 
            // manualDeUsuarioToolStripMenuItem
            // 
            this.manualDeUsuarioToolStripMenuItem.Name = "manualDeUsuarioToolStripMenuItem";
            this.manualDeUsuarioToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.manualDeUsuarioToolStripMenuItem.Text = "Manual de usuario";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            // 
            // txtBoxAnalalizador
            // 
            this.txtBoxAnalalizador.Location = new System.Drawing.Point(13, 28);
            this.txtBoxAnalalizador.Multiline = true;
            this.txtBoxAnalalizador.Name = "txtBoxAnalalizador";
            this.txtBoxAnalalizador.Size = new System.Drawing.Size(492, 246);
            this.txtBoxAnalalizador.TabIndex = 1;
            // 
            // txtBoxLog
            // 
            this.txtBoxLog.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtBoxLog.ForeColor = System.Drawing.SystemColors.Info;
            this.txtBoxLog.Location = new System.Drawing.Point(13, 291);
            this.txtBoxLog.Multiline = true;
            this.txtBoxLog.Name = "txtBoxLog";
            this.txtBoxLog.Size = new System.Drawing.Size(522, 56);
            this.txtBoxLog.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(511, 101);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(23, 67);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "C";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAnalizar
            // 
            this.btnAnalizar.Location = new System.Drawing.Point(511, 28);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(23, 67);
            this.btnAnalizar.TabIndex = 4;
            this.btnAnalizar.Text = "A";
            this.btnAnalizar.UseVisualStyleBackColor = true;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(511, 174);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(23, 67);
            this.btnShow.TabIndex = 5;
            this.btnShow.Text = "S";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 359);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnAnalizar);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtBoxLog);
            this.Controls.Add(this.txtBoxAnalalizador);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inicioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejecutarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualTécnicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualDeUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.TextBox txtBoxAnalalizador;
        private System.Windows.Forms.TextBox txtBoxLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAnalizar;
        private System.Windows.Forms.Button btnShow;
    }
}

