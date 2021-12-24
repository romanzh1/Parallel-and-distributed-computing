using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fractal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int N = Math.Min(pictureBox1.Height, pictureBox1.Height);
            myFractal mf = new myFractal(N, N, -2, -1.5, 3.0, 3.0, 1250);
            Bitmap flag = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = flag;
            DateTime time = DateTime.Now;
            mf.Recalc();
            var timer = DateTime.Now - time;

            textBox1.Text = timer.TotalMilliseconds.ToString() + " ms";
            time = DateTime.Now;
            mf.RecalcParal();
            timer = DateTime.Now - time;

            textBox2.Text = timer.TotalMilliseconds.ToString() + " ms";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    flag.SetPixel(i, j, mf.GetColor(mf.DepthsParal[i, j]));
                }
            }
            Graphics gr = pictureBox1.CreateGraphics();
            gr.DrawImage(flag, 0, 0);
            pictureBox1.Refresh();
        }
    }
}
