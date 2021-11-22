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
using System.Threading;

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
        struct arrMatr
        {
            public double[,] one;
            public double[,] two;
            public double[,] rez;

            public int start;
            public int countRow;
        }

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

        private double[,] multiMatrix(double[,] one, double[,] two, double[,] rez)
        {
            arrMatr [] am = new arrMatr[8];
            Thread[] flow = new Thread[am.Length];
            int countRow = (int)Math.Floor((decimal)one.GetLength(0) / am.Length);
            if (countRow == 0)
            {
                arrMatr arr;
                arr.one = one;
                arr.two = two;
                arr.rez = rez;
                arr.start = 0;
                arr.countRow = one.GetLength(0);
                multThread(arr);
            }
            else
            {
                for (int i = 0; i < am.Length; i++)
                {
                    am[i].one = one;
                    am[i].two = two;
                    am[i].rez = rez;
                    am[i].start = (int)Math.Floor((decimal)one.GetLength(0) * (i / am.Length));
                    am[i].countRow = (int)Math.Floor((decimal)one.GetLength(0) * ((i + 1) / am.Length)) - am[i].start;

                }
                //37: 0, 4, 9, 13, 18, 23, 27, 32, 37
                for (int i = 0; i < am.Length; i++)
                {
                    flow[i] = new Thread(multThread);
                    flow[i].Start(am[i]);
                }
                for (int i = 0; i < am.Length; i++)
                {
                    flow[i].Join();
                }
            }

            return rez;
        }

        private void multThread(object obj)
        {
            arrMatr am = (arrMatr)obj;
            multiRow(am.one, am.two, am.rez, am.start, am.countRow);
        }

        private void multiRow(double[,] one, double[,] two, double[,] rez, int start, int countRow)
        {
            for (int i = start; i < start + countRow; i++)
            {
                for (int j = 0; j < two.GetLength(1); j++)
                {
                    rez[i, j] = 0;
                    for (int k = 0; k < two.GetLength(0); k++)
                    {
                        rez[i, j] += one[i, k] * two[k, j];
                    }
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


        private void Button2_Click(object sender, EventArgs e)
        {
            if (N < 5) Str14.Visible = true;
            if (N < 5) Okno7.Visible = true;
            Okno7.Clear();
            double[,] big = new double[a.GetLength(0), b.GetLength(1)];
            var sw = Stopwatch.StartNew();
            big = multiMatrix(a, b, big);
            sw.Stop();
            textBox2.Text = string.Format("time: {0}", sw.ElapsedMilliseconds);

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

            textBox1.Text = sumDiag(big).ToString();
        }

        private double sumDiag(double[,] big)
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
