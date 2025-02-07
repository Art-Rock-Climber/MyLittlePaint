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
    public partial class StarSettingsForm : Form
    {
        public int Points { get; private set; }
        public float InnerOuterRatio { get; private set; }

        public StarSettingsForm()
        {
            InitializeComponent();
            numericUpDownPoints.Value = 5; // По умолчанию 5 лучей
            numericUpDownRatio.Value = 0.5M; // Отношение радиусов 0.5
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Points = (int)numericUpDownPoints.Value;
            InnerOuterRatio = (float)numericUpDownRatio.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
