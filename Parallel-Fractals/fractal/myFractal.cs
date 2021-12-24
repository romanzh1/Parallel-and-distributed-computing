using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace fractal
{
    class myFractal
    {
        int W;
        int H;
        double X;
        double Y;
        double Wx;
        double Hy;
        int N;

        public int[,] Depths;
        public int[,] DepthsParal;

        public myFractal(int w, int h, double x, double y, double wx, double hy, int n)
        {
            Depths = new int[w, h];
            DepthsParal = new int[w, h];
            W = w;
            H = h;
            X = x;
            Y = y;
            Wx = wx;
            Hy = hy;
            N = n;
        }
        private int getDepth(double x, double y, int maxtry)
        {
            int res = 1;
            double x1 = x, y1 = y;
            while (x1*x1 + y1*y1 < 4.0 && res < maxtry)
            {
                res++;
                double x2 = x1 * x1 - y1 * y1 + x;
                double y2 = 2 * x1 * y1 + y;
                x1 = x2;
                y1 = y2;
            }
            return res < maxtry ? res : -1;
        }

        private void CalcLine(Object o)
        {
            int i = (int)o;
            double dx = Wx / W;
            for (int j = 0; j < H; j++)
            {
                double x = X + i * dx;
                double y = Y + j * dx;
                DepthsParal[i, j] = getDepth(x, y, N);
            }
        }

        public void Recalc()
        {
            double dx = Wx / W;
            for (int i = 0; i < W; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    double x = X + i * dx;
                    double y = Y + j * dx;
                    Depths[i, j] = getDepth(x, y, N);
                }
            }
        }

        public void RecalcParal()
        {
            Task[] tasks = new Task[N];
            double dx = Wx / W;
            for (int i = 0; i < W; i++)
            {
                Action<object> a1 = this.CalcLine;
                Task t1 = new Task(a1, i);
                tasks[i] = t1;
                t1.Start();
            }
            for (int i = 0; i < W; i++)
            {
                tasks[i].Wait();
            }
        }

        public Color GetColor(int depth)
        {
            if (depth == -1) return Color.Black;
            else if (depth < 4) return Color.Blue;
            else if (depth < 8) return Color.Green;
            else if (depth < 12) return Color.Purple;
            else if (depth < 20) return Color.Yellow;
            else if (depth < 30) return Color.Orange;
            return Color.Red;
        }
    }
}
