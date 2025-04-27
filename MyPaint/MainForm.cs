using PluginInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class MainForm : Form
    {
        Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();

        public MainForm()
        {
            InitializeComponent();
            CurrentColor = Color.Black;
            CurrentTool = DrawTools.Pen;
            CurrentWidth = 1;
            CurrentText = string.Empty;
            ImageIndex = 0;

            FindPlugins();
            CreatePluginsMenu();
        }

        /// <summary>
        /// Текущий цвет.
        /// </summary>
        public static Color CurrentColor { get; set; }

        /// <summary>
        /// Текущий инструмент рисования.
        /// </summary>
        public static DrawTools CurrentTool { get; set; }
        
        /// <summary>
        /// Текущий размер пера.
        /// </summary>
        public static int CurrentWidth { get; set; }

        /// <summary>
        /// Текущий текст, введённый пользователем
        /// </summary>
        public static string CurrentText { get; set; }

        /// <summary>
        /// Текущий номер рисунка
        /// </summary>
        public static int ImageIndex { get; set; }

        private void файлToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var d = ActiveMdiChild as FormDocument;
            сохранитьToolStripMenuItem.Enabled = d != null;
            сохранитькакToolStripMenuItem.Enabled = d != null;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var doc = new FormDocument();
            doc.Text = $"Новый рисунок {++ImageIndex}";
            doc.MdiParent = this;
            doc.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var doc = new FormDocument(openFileDialog.FileName);
                        doc.MdiParent = this;
                        doc.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при открытии файла: " + ex.Message);
                    }
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                d.Save();
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                SaveDocumentAs(d);
            }
        }

        /// <summary>
        /// Вызывает "Сохранить как" для указанного документа
        /// </summary>
        public void SaveDocumentAs(FormDocument d)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "JPEG Image|*.jpg|BMP Image|*.bmp",
                FileName = d.FilePath != null ? Path.GetFileName(d.FilePath) : $"Новый рисунок {ImageIndex}"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                d.SaveAs(dlg.FileName);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void правкаToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var d = ActiveMdiChild as FormDocument;
            отменадействияToolStripMenuItem.Enabled = (d != null) && d.CanUndo();
            повторениеДействияToolStripMenuItem.Enabled = (d != null) && d.CanRedo();
        }

        private void отменадействияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                d.UndoLastAction();
                UpdateUndoRedoButtons();
            }
        }

        private void повторениеДействияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                d.RedoLastAction();
                UpdateUndoRedoButtons();
            }
        }

        public void UpdateUndoRedoButtons()
        {
            var doc = ActiveMdiChild as FormDocument;
            отменадействияToolStripMenuItem.Enabled = doc?.CanUndo() ?? false;
            повторениеДействияToolStripMenuItem.Enabled = doc?.CanRedo() ?? false;
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            UpdateUndoRedoButtons();
        }

        private void каскадомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void слеваНаправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void упорядочитьЗначкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formAbout = new FormAboutBox();
            formAbout.ShowDialog();
        }

        void FindPlugins()
        {
            // папка с плагинами
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;

            // dll-файлы в этой папке
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginInterface.IPlugin");

                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                string pluginName = ((ToolStripMenuItem)sender).Text;
                if (plugins.TryGetValue(pluginName, out IPlugin plugin))
                {
                    d.ApplyPlugin(plugin);
                }
            }
        }

        private void CreatePluginsMenu()
        {
            foreach (var p in plugins)
            {
                var item = фильтрыToolStripMenuItem.DropDownItems.Add(p.Value.Name);
                item.Click += OnPluginClick;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.White;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Blue;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Red;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Black;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                CurrentColor = dlg.Color;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CurrentWidth = 1;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            CurrentWidth = 5;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            CurrentWidth = 10;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            CurrentWidth = 20;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Pen;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Eraser;
        }

        private void прямаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Line;
        }

        private void окружностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Circumference;
        }

        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Circle;
        }

        private void прямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Rectanle;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Text;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.PaintBucket;
        }

        private void звездаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Star;
        }

        public void ShowPosition(int x, int y)
        {
            if (x != -1)
                toolStripLabelPosition.Text = $"X: {x} Y: {y}";
            else
                toolStripLabelPosition.Text = string.Empty;
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                d.ZoomIn();
            }
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                d.ZoomOut();
            }
        }
    }
}
