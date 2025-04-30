namespace MyPaint
{
    partial class PluginManagerDialogForm
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
            this.dataGridViewPlugins = new System.Windows.Forms.DataGridView();
            this.checkBoxAutoMode = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlugins)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPlugins
            // 
            this.dataGridViewPlugins.AllowUserToAddRows = false;
            this.dataGridViewPlugins.AllowUserToDeleteRows = false;
            this.dataGridViewPlugins.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewPlugins.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewPlugins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlugins.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name1,
            this.Version,
            this.Enabled});
            this.dataGridViewPlugins.Location = new System.Drawing.Point(25, 26);
            this.dataGridViewPlugins.Name = "dataGridViewPlugins";
            this.dataGridViewPlugins.RowHeadersVisible = false;
            this.dataGridViewPlugins.RowHeadersWidth = 51;
            this.dataGridViewPlugins.RowTemplate.Height = 24;
            this.dataGridViewPlugins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPlugins.Size = new System.Drawing.Size(426, 204);
            this.dataGridViewPlugins.TabIndex = 0;
            // 
            // checkBoxAutoMode
            // 
            this.checkBoxAutoMode.AutoSize = true;
            this.checkBoxAutoMode.Location = new System.Drawing.Point(25, 249);
            this.checkBoxAutoMode.Name = "checkBoxAutoMode";
            this.checkBoxAutoMode.Size = new System.Drawing.Size(348, 20);
            this.checkBoxAutoMode.TabIndex = 1;
            this.checkBoxAutoMode.Text = "Автоматический режим (загружать все плагины)";
            this.checkBoxAutoMode.UseVisualStyleBackColor = true;
            this.checkBoxAutoMode.CheckedChanged += new System.EventHandler(this.checkBoxAutoMode_CheckedChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(25, 302);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(127, 29);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ок";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(324, 302);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(127, 29);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // Name1
            // 
            this.Name1.HeaderText = "Название";
            this.Name1.MinimumWidth = 6;
            this.Name1.Name = "Name1";
            this.Name1.ReadOnly = true;
            this.Name1.Width = 102;
            // 
            // Version
            // 
            this.Version.HeaderText = "Версия";
            this.Version.MinimumWidth = 6;
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Width = 83;
            // 
            // Enabled
            // 
            this.Enabled.HeaderText = "Загружено";
            this.Enabled.MinimumWidth = 6;
            this.Enabled.Name = "Enabled";
            this.Enabled.Width = 85;
            // 
            // PluginManagerDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 353);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkBoxAutoMode);
            this.Controls.Add(this.dataGridViewPlugins);
            this.Name = "PluginManagerDialogForm";
            this.Text = "Управление плагинами";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlugins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPlugins;
        private System.Windows.Forms.CheckBox checkBoxAutoMode;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Enabled;
    }
}