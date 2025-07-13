namespace Compilador
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
            this.codigoText = new System.Windows.Forms.RichTextBox();
            this.erroresText = new System.Windows.Forms.TextBox();
            this.compilarBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // codigoText
            // 
            this.codigoText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codigoText.BackColor = System.Drawing.Color.PowderBlue;
            this.codigoText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codigoText.Location = new System.Drawing.Point(12, 48);
            this.codigoText.MinimumSize = new System.Drawing.Size(500, 300);
            this.codigoText.Name = "codigoText";
            this.codigoText.Size = new System.Drawing.Size(788, 312);
            this.codigoText.TabIndex = 0;
            this.codigoText.Text = "";
            // 
            // erroresText
            // 
            this.erroresText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.erroresText.BackColor = System.Drawing.Color.PowderBlue;
            this.erroresText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.erroresText.Location = new System.Drawing.Point(12, 371);
            this.erroresText.Margin = new System.Windows.Forms.Padding(8);
            this.erroresText.MinimumSize = new System.Drawing.Size(500, 100);
            this.erroresText.Multiline = true;
            this.erroresText.Name = "erroresText";
            this.erroresText.Size = new System.Drawing.Size(788, 168);
            this.erroresText.TabIndex = 1;
            // 
            // compilarBtn
            // 
            this.compilarBtn.BackColor = System.Drawing.Color.PowderBlue;
            this.compilarBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.compilarBtn.Location = new System.Drawing.Point(13, 13);
            this.compilarBtn.Name = "compilarBtn";
            this.compilarBtn.Size = new System.Drawing.Size(98, 29);
            this.compilarBtn.TabIndex = 2;
            this.compilarBtn.Text = "Compilar";
            this.compilarBtn.UseVisualStyleBackColor = false;
            this.compilarBtn.Click += new System.EventHandler(this.compilarBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(812, 551);
            this.Controls.Add(this.compilarBtn);
            this.Controls.Add(this.erroresText);
            this.Controls.Add(this.codigoText);
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "Form1";
            this.Text = "Compilador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox codigoText;
        private System.Windows.Forms.TextBox erroresText;
        private System.Windows.Forms.Button compilarBtn;
    }
}

