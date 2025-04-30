using PluginManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class PluginManagerDialogForm : Form
    {
        private AppConfig config;
        private BindingList<PluginConfig> pluginsBindingList;

        public PluginManagerDialogForm(AppConfig config)
        {
            this.config = config;
            this.pluginsBindingList = new BindingList<PluginConfig>(config.Plugins);

            InitializeComponent();
            checkBoxAutoMode.Checked = config.AutoMode;

            ConfigureDataGridView();

            dataGridViewPlugins.DataSource = pluginsBindingList;
            dataGridViewPlugins.Enabled = !config.AutoMode;

        }

        private void ConfigureDataGridView()
        {
            dataGridViewPlugins.AutoGenerateColumns = false;
            dataGridViewPlugins.Columns.Clear();

            DataGridViewCheckBoxColumn enabledColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Вкл.",
                DataPropertyName = "Enabled",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridViewPlugins.Columns.Add(enabledColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Название плагина",
                DataPropertyName = "NameRus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridViewPlugins.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn authorColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Автор",
                DataPropertyName = "AuthorRus",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridViewPlugins.Columns.Add(authorColumn);

            DataGridViewTextBoxColumn versionColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Версия",
                DataPropertyName = "Version",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridViewPlugins.Columns.Add(versionColumn);
        }

        private void checkBoxAutoMode_CheckedChanged(object sender, EventArgs e)
        {
            config.AutoMode = checkBoxAutoMode.Checked;
            dataGridViewPlugins.Enabled = !config.AutoMode;

            foreach (var plugin in pluginsBindingList)
            {
                plugin.Enabled = config.AutoMode;
            }
            dataGridViewPlugins.Refresh();
        }
    }
}
