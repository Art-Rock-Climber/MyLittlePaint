using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyPaint
{
    public partial class FormDocument : Form
    {
        private int x, y;

        /// <summary>
        /// Битовая карта
        /// </summary>
        private Bitmap bmp;

        private Bitmap bmpTemp;

        private bool isModified;

        private float size = 1;

        private Size baseSize;

        /// <summary>
        /// Путь к файлу, в котором сохранён документ
        /// </summary>
        public string FilePath { get; private set; }

        public FormDocument()
        {
            InitializeComponent();
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            bmpTemp = bmp;
            BackColor = Color.White;
            isModified = false;
            AutoScrollMinSize = new Size(ClientSize.Width, ClientSize.Height);
            baseSize = new Size(ClientSize.Width, ClientSize.Height);
            ClearBMP(bmp);
            ClearBMP(bmpTemp);
        }

        public FormDocument(Bitmap bmp, string filePath = null)
        {
            InitializeComponent();
            this.bmp = bmp;
            bmpTemp = bmp;

            FilePath = filePath;
            if (!string.IsNullOrEmpty(filePath))
            {
                Text = Path.GetFileName(filePath);
            }
            else
            {
                Text = "Новый рисунок";
            }

            isModified = false;
        }

        public FormDocument(string filePath)
        {
            InitializeComponent();
            BackColor = Color.White;

            using (Image img = Image.FromFile(filePath))
            {
                bmp = new Bitmap(img); // Копируем изображение, чтобы освободить файл
            }

            bmpTemp = (Bitmap)bmp.Clone();

            FilePath = filePath;
            Text = Path.GetFileName(filePath);
            isModified = false;
            AutoScrollMinSize = new Size(ClientSize.Width, ClientSize.Height);
            baseSize = new Size(ClientSize.Width, baseSize.Height);

            //ClearBMP(bmp);
            //ClearBMP(bmpTemp);
        }


        private void FormDocument_MouseDown(object sender, MouseEventArgs e)
        {
            x = (int)((e.X - AutoScrollPosition.X) / size);
            y = (int)((e.Y - AutoScrollPosition.Y) / size);

            if (e.Button == MouseButtons.Left)
            {
                var brush = new SolidBrush(MainForm.CurrentColor);

                switch (MainForm.CurrentTool)
                {
                    case DrawTools.PaintBucket:
                        ScanlineFloodFill(x, y, MainForm.CurrentColor);
                        bmpTemp = (Bitmap)bmp.Clone();
                        Invalidate();
                        break;
                    case DrawTools.Text:
                        var inputForm = new TextInputForm();
                        if (inputForm.ShowDialog(this) == DialogResult.OK)
                        {
                            MainForm.CurrentText = inputForm.TextValue;
                            using (var g = Graphics.FromImage(bmp))
                            {
                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                                g.DrawString(MainForm.CurrentText,
                                           new Font(DefaultFont.FontFamily, MainForm.CurrentWidth * 5 / size),
                                           brush, x, y);
                                bmpTemp = (Bitmap)bmp.Clone();
                                Invalidate();
                            }
                        }
                        break;

                    case DrawTools.Star:
                        var starSettingsForm = new StarSettingsForm();
                        if (starSettingsForm.ShowDialog() == DialogResult.OK)
                        {
                            using (var g = Graphics.FromImage(bmp))
                            {
                                var pen = new Pen(MainForm.CurrentColor, MainForm.CurrentWidth / size);
                                int points = starSettingsForm.Points;
                                float ratio = starSettingsForm.InnerOuterRatio;
                                int outerRadius = (int)(50 / size);

                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                DrawStar(g, pen, brush, x, y, outerRadius, points, ratio);
                                bmpTemp = (Bitmap)bmp.Clone();
                                Invalidate();
                            }
                        }
                        break;
                }
            }
        }

        private void FormDocument_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var pen = new Pen(MainForm.CurrentColor, MainForm.CurrentWidth / size);
                var brush = new SolidBrush(MainForm.CurrentColor);

                float scaledX = (e.X - AutoScrollPosition.X) / size;
                float scaledY = (e.Y - AutoScrollPosition.Y) / size;

                switch (MainForm.CurrentTool)
                {
                    case DrawTools.Pen:
                        using (var g = Graphics.FromImage(bmp))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            g.DrawLine(pen, x, y, scaledX, scaledY);
                            x = (int)scaledX;
                            y = (int)scaledY;
                            bmpTemp = bmp;
                            Invalidate();
                        }
                        break;
                    case DrawTools.Line:
                        bmpTemp = (Bitmap)bmp.Clone();
                        using (var g = Graphics.FromImage(bmpTemp))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            g.DrawLine(pen, x, y, scaledX, scaledY);
                            Invalidate();
                        }
                        break;
                    case DrawTools.Circumference:
                        bmpTemp = (Bitmap)bmp.Clone();
                        using (var g = Graphics.FromImage(bmpTemp))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            g.DrawEllipse(pen, new Rectangle(x, y, (int)(scaledX - x), (int)(scaledY - y)));
                            Invalidate();
                        }
                        break;
                    case DrawTools.Circle:
                        bmpTemp = (Bitmap)bmp.Clone();
                        using (var g = Graphics.FromImage(bmpTemp))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            g.FillEllipse(brush, new Rectangle(x, y, (int)(scaledX - x), (int)(scaledY - y)));

                            Invalidate();
                        }
                        break;
                    case DrawTools.Rectanle:
                        bmpTemp = (Bitmap)bmp.Clone();
                        using (var g = Graphics.FromImage(bmpTemp))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            g.DrawRectangle(pen, new Rectangle(x, y, (int)(scaledX - x), (int)(scaledY - y)));
                            Invalidate();
                        }
                        break;
                    case DrawTools.Eraser:
                        using (var eraserPen = new Pen(BackColor, MainForm.CurrentWidth))
                        using (var g = Graphics.FromImage(bmp))
                        {
                            g.DrawLine(eraserPen, x, y, scaledX, scaledY);
                            x = (int)scaledX;
                            y = (int)scaledY;
                            bmpTemp = bmp;
                            Invalidate();
                        }
                        break;
                }

                var parent = MdiParent as MainForm;
                parent?.ShowPosition((int)scaledX, (int)scaledY);
            }
        }

        private void ScanlineFloodFill(int x, int y, Color newColor)
        {
            Color targetColor = bmp.GetPixel(x, y);
            if (AreColorsClose(targetColor, newColor)) return;

            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(new Point(x, y));

            while (pixels.Count > 0)
            {
                Point p = pixels.Pop();
                int currentX = p.X;
                int currentY = p.Y;

                // Найдем левую границу заливки
                while (currentX >= 0 && AreColorsClose(bmp.GetPixel(currentX, currentY), targetColor))
                    currentX--;
                currentX++; // Вернемся на один пиксель вправо

                bool spanAbove = false;
                bool spanBelow = false;

                // Заливаем вправо до конца области
                while (currentX < bmp.Width && AreColorsClose(bmp.GetPixel(currentX, currentY), targetColor))
                {
                    bmp.SetPixel(currentX, currentY, newColor);

                    if (currentY > 0) // Проверка строки выше
                    {
                        if (!spanAbove && AreColorsClose(bmp.GetPixel(currentX, currentY - 1), targetColor))
                        {
                            pixels.Push(new Point(currentX, currentY - 1));
                            spanAbove = true;
                        }
                        else if (spanAbove && !AreColorsClose(bmp.GetPixel(currentX, currentY - 1), targetColor))
                        {
                            spanAbove = false;
                        }
                    }

                    if (currentY < bmp.Height - 1) // Проверка строки ниже
                    {
                        if (!spanBelow && AreColorsClose(bmp.GetPixel(currentX, currentY + 1), targetColor))
                        {
                            pixels.Push(new Point(currentX, currentY + 1));
                            spanBelow = true;
                        }
                        else if (spanBelow && !AreColorsClose(bmp.GetPixel(currentX, currentY + 1), targetColor))
                        {
                            spanBelow = false;
                        }
                    }

                    currentX++; // Переходим к следующему пикселю вправо
                }
            }
        }

        private bool AreColorsClose(Color c1, Color c2, int tolerance = 2)
        {
            return Math.Abs(c1.A - c2.A) <= tolerance &&  // Проверка альфа-канала
                Math.Abs(c1.R - c2.R) <= tolerance &&
                Math.Abs(c1.G - c2.G) <= tolerance &&
                Math.Abs(c1.B - c2.B) <= tolerance;

        }

        private void DrawStar(Graphics g, Pen pen, Brush brush, int centerX, int centerY, int outerRadius, int points, float innerOuterRatio)
        {
            if (points < 3) return; // Минимум 3 точки для корректной звезды

            PointF[] starPoints = new PointF[points * 2];
            double angleStep = Math.PI / points;
            double angle = -Math.PI / 2; // Начинаем сверху

            for (int i = 0; i < points * 2; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : outerRadius * innerOuterRatio;
                starPoints[i] = new PointF(
                    centerX + (float)(radius * Math.Cos(angle)),
                    centerY + (float)(radius * Math.Sin(angle))
                );
                angle += angleStep;
            }

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillPolygon(brush, starPoints);
            g.DrawPolygon(pen, starPoints);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.ScaleTransform(size, size);
    
            int scrollX = AutoScrollPosition.X;
            int scrollY = AutoScrollPosition.Y;
            e.Graphics.DrawImage(bmpTemp, scrollX / size, scrollY / size);

        }

        private void FormDocument_MouseLeave(object sender, EventArgs e)
        {
            var parent = MdiParent as MainForm;
            parent?.ShowPosition(-1, -1);
        }

        private void FormDocument_MouseUp(object sender, MouseEventArgs e)
        {
            //switch (MainForm.CurrentTool)
            //{
            //    case Tools.Circumference:
            //        bmp = (Bitmap)bmpTemp.Clone();
            //        Invalidate();
            //        break;
            //    case Tools.Circle:
            //        bmp = (Bitmap)bmpTemp.Clone();
            //        Invalidate();
            //        break;
            //    case Tools.Rectanle:
            //        bmp = (Bitmap)bmpTemp.Clone();
            //        Invalidate();
            //        break;
            //}
            bmp = (Bitmap)bmpTemp.Clone();
            Invalidate();
        }

        private void FormDocument_MouseEnter(object sender, EventArgs e)
        {
            switch (MainForm.CurrentTool)
            {
                case DrawTools.Pen:
                    Cursor = Cursors.Cross;
                    break;
                case DrawTools.Line:
                    Cursor = Cursors.Cross;
                    break;
                case DrawTools.Circumference:
                    Cursor = Cursors.UpArrow;
                    break;
                case DrawTools.Circle:
                    Cursor = Cursors.UpArrow;
                    break;
                case DrawTools.Rectanle:
                    Cursor = Cursors.UpArrow;
                    break;
                case DrawTools.Eraser:
                    Cursor = Cursors.Cross;
                    break;
                case DrawTools.Text:
                    Cursor = Cursors.IBeam;
                    break;
                case DrawTools.PaintBucket:
                    Cursor = Cursors.Hand;
                    break;
            }
        }

        /// <summary>
        /// Сохраняет изображение в текущий файл
        /// </summary>
        public void Save()
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                bmp.Save(FilePath);
                isModified = true;
            }
            else
            {
                var parent = MdiParent as MainForm;
                parent?.SaveDocumentAs(this);
                isModified = true;
            }
        }

        private void FormDocument_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isModified)
            {
                DialogResult result = MessageBox.Show(
                    $"Сохранить изменения в \"{this.Text}\"?",
                    "Изменения не сохранены",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    Save();
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        public void SaveAs(string path)
        {
            try
            {
                ImageFormat format = ImageFormat.Jpeg;
                string extension = Path.GetExtension(path).ToLower();

                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }

                using (Bitmap bmpCopy = new Bitmap(bmp))
                {
                    //var graphics = Graphics.FromImage(bmpCopy);
                    //graphics.Clear(Color.White);
                    //graphics.DrawImage(bmp, 0, 0);
                    bmpCopy.Save(path, format);
                }

                FilePath = path;
                Text = Path.GetFileName(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ApplyZoom()
        {
            AutoScrollMinSize = new Size((int)(baseSize.Width * size), (int)(baseSize.Height * size));
            Invalidate();
        }

        public void ZoomIn()
        {
            if (size >= 1.5) return;
            size += 0.1f;
            ApplyZoom();
        }

        public void ZoomOut()
        {
            if (size < 0.6) return;
            size -= 0.1f;
            ApplyZoom();
        }

        private void ClearBMP(Bitmap b)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.White);
            }
        }
    }
}
