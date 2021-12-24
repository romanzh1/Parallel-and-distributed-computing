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
using System.Threading;

namespace server
{
    public partial class Form1 : Form
    {
        List<string> messages = new List<string>();
        string pseudo;

        public Form1()
        {
            InitializeComponent();
        }

        private void createConnection()
        {
            int count = messages.Count;
            NamedPipeServerStream ss;
            ss = new NamedPipeServerStream("server", PipeDirection.InOut, 5, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            ss.WaitForConnection();
            
            messages.Add("Connected");
            Thread t2 = new Thread(createConnection);
            t2.Start();
            StreamReader sr = new StreamReader(ss);
            StreamWriter sw = new StreamWriter(ss);
            pseudo = sr.ReadLine();
            Task<string> t1 = ReadLineAsync(sr, pseudo);
            while (true)
            {
                if (t1.IsCompleted)
                {
                    string s1 = t1.Result;
                    if (String.IsNullOrEmpty(s1))
                    {
                        break;
                    }
                    t1 = ReadLineAsync(sr, pseudo);
                }
                while (count < messages.Count)
                {

                    sw.WriteLine(messages[count]);
                    count++;
                    sw.Flush();
                }
            }
            ss.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(createConnection);
            t1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async Task<string> ReadLineAsync(StreamReader sr, string ps)
        {
            string st = await sr.ReadLineAsync();
            if (!string.IsNullOrEmpty(st))
                messages.Add(ps + ":" + st);
            return st;
        }
    }
}
