namespace MyPaint
{
    partial class StarSettingsForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownPoints = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRatio = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRatio)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(72, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ок";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(200, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Количество лучей";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(39, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "Отношение внутреннего и внешнего радиуса";
            // 
            // numericUpDownPoints
            // 
            this.numericUpDownPoints.Location = new System.Drawing.Point(237, 27);
            this.numericUpDownPoints.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownPoints.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownPoints.Name = "numericUpDownPoints";
            this.numericUpDownPoints.Size = new System.Drawing.Size(63, 22);
            this.numericUpDownPoints.TabIndex = 4;
            this.numericUpDownPoints.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numericUpDownRatio
            // 
            this.numericUpDownRatio.DecimalPlaces = 2;
            this.numericUpDownRatio.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownRatio.Location = new System.Drawing.Point(237, 70);
            this.numericUpDownRatio.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            this.numericUpDownRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownRatio.Name = "numericUpDownRatio";
            this.numericUpDownRatio.Size = new System.Drawing.Size(63, 22);
            this.numericUpDownRatio.TabIndex = 5;
            this.numericUpDownRatio.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // StarSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 157);
            this.Controls.Add(this.numericUpDownRatio);
            this.Controls.Add(this.numericUpDownPoints);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "StarSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка звезды";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRatio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPoints;
        private System.Windows.Forms.NumericUpDown numericUpDownRatio;
    }
}