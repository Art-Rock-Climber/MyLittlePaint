using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CurrentColor = Color.Black;
            CurrentTool = DrawTools.Pen;
            CurrentWidth = 1;
            CurrentText = string.Empty;
            ImageIndex = 0;
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

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formAbout = new FormAboutBox();
            formAbout.ShowDialog();
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var doc = new FormDocument();
            doc.Text = $"Новый рисунок {++ImageIndex}";
            doc.MdiParent = this;
            doc.Show();
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

        public void ShowPosition(int x, int y)
        {
            if (x != -1)
                statusLabelPosition.Text = $"X: {x} Y: {y}";
            else
                statusLabelPosition.Text = string.Empty;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Pen;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            CurrentTool = DrawTools.Eraser;
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

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                d.Save();
            }
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

        private void файлToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var d = ActiveMdiChild as FormDocument;
            сохранитьToolStripMenuItem.Enabled = d != null;
            сохранитьКакToolStripMenuItem.Enabled = d != null;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                d.ZoomIn();
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormDocument d)
            {
                d.ZoomOut();
            }
        }
    }
}
