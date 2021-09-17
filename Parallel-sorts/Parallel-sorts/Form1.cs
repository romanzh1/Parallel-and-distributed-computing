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

namespace Parallel_sorts
{
    public partial class Form1 : Form
    {
        Sorts so1;
        Sorts so2;
        Sorts so3;
        ListBox listBox1 = new ListBox();
        Form listArrs;

        public Form1()
        {
            InitializeComponent();
            genListBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            listArrs.Visible = false;
            int len = Int32.Parse(textBox1.Text);
            so1 = new Sorts();
            so2 = new Sorts();
            so3 = new Sorts();
            so1.N = len;
            so2.N = len;
            so3.N = len;
            so1.arr = so1.genArr();
            so2.arr = new int[len];
            so3.arr = new int[len];
            for (int i = 0; i < len; i++)
            {
                so3.arr[i] = so2.arr[i] = so1.arr[i];
            }
        }

        private void Progress(int x){
            if (InvokeRequired)
            {
                retProg newPr = Progress;
                Invoke(newPr, x);
                return;
            }
            progressBar3.Value = x;
        }

        private void Progress2(int x)
        {
            if (InvokeRequired)
            {
                retProg newPr = Progress2;
                Invoke(newPr, x);
                return;
            }
            progressBar2.Value = x;
        }

        private void Progress3(int x)
        {
            if (InvokeRequired)
            {
                retProg newPr = Progress3;
                Invoke(newPr, x);
                return;
            }
            progressBar1.Value = x;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listArrs.Visible = false;

            so1.pr1 = Progress;
            so2.pr2 = Progress2;
            so3.pr3 = Progress3;

            timer1.Start();
            Thread flow1 = new Thread(so1.sortQuick);
            flow1.Start();
            Thread flow2 = new Thread(so2.sortInsert);
            flow2.Start();
            Thread flow3 = new Thread(so3.sortChoice);
            flow3.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < so1.N; i++)
            {
                listBox1.Items.Add(so1.arr[i]);
            }
            listArrs.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < so2.N; i++)
            {
                listBox1.Items.Add(so2.arr[i]);
            }
            listArrs.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < so3.N; i++)
            {
                listBox1.Items.Add(so3.arr[i]);
            }
            listArrs.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (so1.fin) label3.Text = string.Format("time: {0} \n permutations: {1}", so1.sw.ElapsedMilliseconds, so1.p);
            if (so2.fin) label2.Text = string.Format("time: {0} \n permutations: {1}", so2.sw.ElapsedMilliseconds, so2.p);
            if (so3.fin) { label1.Text = string.Format("time: {0} \n permutations: {1}", so3.sw.ElapsedMilliseconds, so3.p); timer1.Stop(); }

        }

        private void genListBox()
        {
            // Set the size and location of the ListBox.
            listBox1.Size = new System.Drawing.Size(400, 290);
            listBox1.Location = new System.Drawing.Point(10, 10);
            // Set the selection mode to multiple and extended.
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            createForm();
        }

        public void createForm()
        {
            listArrs = new Form();
            listArrs.Size = new Size(450, 400);
            listArrs.StartPosition = FormStartPosition.CenterParent;
            listArrs.Font = new Font("Comic Sans MS", 15);
            listArrs.Controls.Add(listBox1);
            listArrs.ControlBox = false;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
