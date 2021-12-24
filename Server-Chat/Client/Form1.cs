using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;

namespace Client
{
    public partial class Form1 : Form
    {
        NamedPipeClientStream pc;
        Task<string> task1;
        StreamReader sr;
        StreamWriter sw;
        public Form1()
        {
            InitializeComponent();
        }

        //List<string> messages = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            pc = new NamedPipeClientStream(".", "server", PipeDirection.InOut, PipeOptions.Asynchronous);
            pc.Connect();
            sr = new StreamReader(pc);
            sw = new StreamWriter(pc);
            task1 = sr.ReadLineAsync();
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sw.WriteLineAsync(textBox1.Text);
            sw.Flush();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //messages.Add(sr.ReadLine());
            if (task1 != null && task1.IsCompleted)
            {
                string s1 = task1.Result;
                textBox2.Text += s1;
                textBox2.Text += Environment.NewLine;
                task1 = sr.ReadLineAsync();
            }
        }
    }
}
