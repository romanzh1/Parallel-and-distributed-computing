using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using Server;

namespace Parallel_Graphical_Chat
{
    public partial class Form1 : Form
    {
        const int port = 8888; // порт для прослушивания подключений
        const string address = "127.0.0.1";
        
        TcpListener server = null;

        public Form1()
        {
            InitializeComponent();
        }

        public void createClient()
        {
            while (true)
            {
                ConnectClient cc = new ConnectClient(server);
                Thread threadRead = new Thread(cc.StartRead);
                threadRead.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server = new TcpListener(IPAddress.Parse(address), port);
            server.Start();

            Thread thread = new Thread(createClient);
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (server != null)
                server.Stop();
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
                server.Stop();
        }
    }
}
