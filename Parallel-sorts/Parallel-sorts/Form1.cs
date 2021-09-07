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

namespace Parallel_sorts
{
    public partial class Form1 : Form
    {
        Sorts so = new Sorts();
        int[] arr1, arr2, arr3;
        ListBox listBox1 = new ListBox();
        Form Game;


        public Form1()
        {
            InitializeComponent();
            genListBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game.Visible = false;
            int len = Int32.Parse(textBox1.Text);
            arr1 = so.genArr(len);
            arr2 = new int[len];
            arr3 = new int[len];
            for (int i = 0; i < len; i++)
            {
                arr3[i] = arr2[i] = arr1[i];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Game.Visible = false;

            var sw = Stopwatch.StartNew();
            so.QuickSort(arr3, 0, arr3.Length - 1);
            label3.Text = string.Format("time: {0}", sw.ElapsedMilliseconds);

            sw = Stopwatch.StartNew();
            so.sortInsert(arr2);
            label2.Text = string.Format("time: {0}", sw.ElapsedMilliseconds);

            sw = Stopwatch.StartNew();
            so.sortChoice(arr1);
            label1.Text = string.Format("time: {0}", sw.ElapsedMilliseconds);  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Game.Visible = false;
            listBox1.Items.Clear();
            for (int i = 0; i < arr1.Length; i++)
            {
                listBox1.Items.Add(arr1[i]);
            }
            Game.Visible = true;
       
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Game.Visible = false;
            listBox1.Items.Clear();
            for (int i = 0; i < arr2.Length; i++)
            {
                listBox1.Items.Add(arr2[i]);
            }
            Game.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Game.Visible = false;
            listBox1.Items.Clear();
            for (int i = 0; i < arr3.Length; i++)
            {
                listBox1.Items.Add(arr3[i]);
            }
            Game.Visible = true;
        }

        private void genListBox()
        {
            // Set the size and location of the ListBox.
            listBox1.Size = new System.Drawing.Size(400, 290);
            listBox1.Location = new System.Drawing.Point(10, 10);
            // Add the ListBox to the form.
            //listBox1.MultiColumn = true;
            // Set the selection mode to multiple and extended.
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            createForm();
        }

        public void createForm()
        {
            Game = new Form();
            Game.Size = new Size(450, 400);
            Game.StartPosition = FormStartPosition.CenterScreen;
            Game.Font = new Font("Comic Sans MS", 15);

            Game.Controls.Add(listBox1);
            //Game.ControlBox = false;
            //Game.FormBorderStyle = FormBorderStyle.None;
        }
    }
}
