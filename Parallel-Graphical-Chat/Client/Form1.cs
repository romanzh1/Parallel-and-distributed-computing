using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        const int port = 8888; // порт для прослушивания подключений
        const string address = "127.0.0.1";

        BinaryFormatter formatter;

        Graphics gr;
        bool startPaint;
        Random random = new Random();
        Paint pa;

        bool draw = true;

        TcpClient client;
        Socket socket;
        bool connect = false;

        Graphics g;
        Brush br = Brushes.Black;
        int a, red, green, blue;

        private void Send(string message)
        {
            try
            {
                socket.Send(Encoding.GetEncoding(1251).GetBytes(message));
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }

        private void Read()
        {
            while (true)
            {
                if (!connect)
                {
                    break;
                }
                try
                {
                    byte[] read_data = new byte[1024];
                    socket.Receive(read_data);
                    string msg = Encoding.GetEncoding(1251).GetString(read_data.Where(x => x != 0).ToArray());
                    if (draw)
                    {
                        socket.Send(Encoding.GetEncoding(1251).GetBytes("-GETCOLOR"));
                        byte[] read_data2 = new byte[1024];
                        socket.Receive(read_data2);
                        string msg2 = Encoding.GetEncoding(1251).GetString(read_data2.Where(x => x != 0).ToArray());
                        if (msg2[0] == '-')
                        {
                            int t = Convert.ToInt32(msg2.Substring(1, msg2.Length - 1));
                            a = 255;
                            red = (100 + t * 89) % 255;
                            green = (0 + t * 69) % 255;
                            blue = (50 + t * 45) % 255;
                            br = new SolidBrush(Color.FromArgb(255, red, green, blue));
                            draw = false;
                        }
                        draw = false;
                    }
                    if (msg.Length != 0)
                        if (msg[0] == '.')
                        {
                            string[] data = msg.Split('.');
                            Brush DrawBrush = new SolidBrush(Color.FromArgb(Convert.ToInt32(data[3]), Convert.ToInt32(data[4]), Convert.ToInt32(data[5]), Convert.ToInt32(data[6])));
                            int[] Pos = new int[2];
                            Pos[0] = Convert.ToInt32(data[1]);
                            Pos[1] = Convert.ToInt32(data[2]);
                            gr.FillRectangle(DrawBrush, Pos[0], Pos[1], 5, 5);
                        }
                }
                catch
                {
                    break;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gr = pictureBox1.CreateGraphics();
            startPaint = false;
            pa = new Paint();
            pa.color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            // создаем объект BinaryFormatter
            formatter = new BinaryFormatter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient();
                client.Connect(address, port);
                connect = true;
                socket = client.Client;
                string name = "user" + random.Next(1, 1000);
                Send(name + " подключился к доске");
                Thread reading = new Thread(Read);
                reading.Start();
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            startPaint = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            /*if (startPaint)
            {
                gr.DrawLine(new Pen((pa.color), 3), new Point(e.X, e.Y), new Point(e.X + 3, e.Y + 3));
                pa.coord = new Point(e.X, e.Y);

                //sendPaint();
            }*/
            if (e.Button == MouseButtons.Left)
            {
                Send(string.Format($".{e.X}.{e.Y}.{a}.{red}.{green}.{blue}"));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //button2.Text = mymy;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Закрываем потоки

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //cs.client.Close();
            //cs.stream.Close();
        }
    }
}
