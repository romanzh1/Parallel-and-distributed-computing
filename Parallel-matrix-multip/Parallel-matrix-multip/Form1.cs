using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Parallel_matrix_multip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Str1_Click(object sender, EventArgs e)
        {

        }

        private void Okno1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Str2_Click(object sender, EventArgs e)
        {

        }

        int N, M, Q, W;
        double [,] a, b;

        private void Button1_Click(object sender, EventArgs e)
        {
            Okno7.Visible = false;
            Str14.Visible = false;
            Okno3.Clear();
            Okno4.Clear();
            N = Int32.Parse(Okno1.Text);
            M = N;
            Q = Int32.Parse(Okno5.Text);
            W = Q;
            a = new double[N, M];
            b = new double[Q, W];
            Random c = new Random();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++) {
                    a[i, j] = c.NextDouble();
                    b[i, j] = c.NextDouble();
                }
            }

            if (N < 5)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++) Okno3.Text += (Math.Round(a[i, j], 2) + "  ");
                    Okno3.Text += Environment.NewLine;
                }
                for (int i = 0; i < Q; i++)
                {
                    for (int j = 0; j < W; j++) Okno4.Text += (Math.Round(b[i, j], 2) + "  ");
                    Okno4.Text += Environment.NewLine;
                }
            }
        }

        private void Str9_Click(object sender, EventArgs e)
        {

        }

        private void Okno8_TextChanged(object sender, EventArgs e)
        {

        }

        private void Okno7_TextChanged(object sender, EventArgs e)
        {

        }

        private void Str8_Click(object sender, EventArgs e)
        {

        }

        private void Okno2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Str13_Click(object sender, EventArgs e)
        {

        }

        private void Spisok_SelectedIndexChanged(object sender, EventArgs e)
        {
            Str8.Visible = false;
            Okno7.Visible = false;
            Str10.Visible = false;
            Str11.Visible = false;
            Str12.Visible = false;
            Str13.Visible = false;
            Str14.Visible = false;

        }

        private void Str14_Click(object sender, EventArgs e)
        {

        }

        double[,] big;
        private void Button2_Click(object sender, EventArgs e)
        {
            if (N < 5) Str14.Visible = true;
            if (N < 5) Okno7.Visible = true;
            Okno7.Clear();
            big = new double[a.GetLength(0), b.GetLength(1)];
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        big[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            textBox2.Text = string.Format("time: {0}", sw.ElapsedMilliseconds);
            sw.Stop();

            if (M < 5)
            {
                for (int i = 0; i < big.GetLength(0); i++)
                {
                    for (int j = 0; j < big.GetLength(1); j++)
                    {
                        Okno7.Text += Math.Round(big[i, j], 2) + "  ";
                    }
                    Okno7.Text += Environment.NewLine;
                }
            }

            textBox1.Text = sumDiag().ToString();
        }

        private double sumDiag()
        {
            double sum = 0;
            for (int j = 0; j < big.GetLength(1); j++)
            {
                sum += big[j, j];
            }
            return sum;
        }

        private void Okno3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
