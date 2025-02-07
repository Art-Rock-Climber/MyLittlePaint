namespace MyPaint
{
    partial class TextInputForm
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
            this.Ок = new System.Windows.Forms.Button();
            this.Отмена = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Ок
            // 
            this.Ок.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ок.Location = new System.Drawing.Point(56, 47);
            this.Ок.Name = "Ок";
            this.Ок.Size = new System.Drawing.Size(75, 23);
            this.Ок.TabIndex = 2;
            this.Ок.Text = "Ок";
            this.Ок.UseVisualStyleBackColor = true;
            this.Ок.Click += new System.EventHandler(this.button1_Click);
            // 
            // Отмена
            // 
            this.Отмена.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Отмена.Location = new System.Drawing.Point(163, 47);
            this.Отмена.Name = "Отмена";
            this.Отмена.Size = new System.Drawing.Size(75, 23);
            this.Отмена.TabIndex = 1;
            this.Отмена.Text = "Отмена";
            this.Отмена.UseVisualStyleBackColor = true;
            this.Отмена.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(33, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(229, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TextInputForm
            // 
            this.AcceptButton = this.Ок;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Отмена;
            this.ClientSize = new System.Drawing.Size(294, 82);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Отмена);
            this.Controls.Add(this.Ок);
            this.Name = "TextInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Введите текст";
            this.Load += new System.EventHandler(this.TextInputForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ок;
        private System.Windows.Forms.Button Отмена;
        private System.Windows.Forms.TextBox textBox1;
    }
}