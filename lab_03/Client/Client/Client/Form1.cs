using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
namespace Client
{
    public partial class Form1 : Form
    {
        TcpClient client = null;
        NetworkStream stream = default(NetworkStream);
        StreamReader streamReader = null;
        StreamWriter streamWriter = null;
        String receivedText;
        bool connected = false;
        String me, s1 = null;
        ArrayList users = new ArrayList();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client = new TcpClient("localhost", 5000);
            stream = client.GetStream();
            connected = true;
            streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(txtBoxConnect.Text + "#");
            streamWriter.Flush();

            me = txtBoxConnect.Text;

            Thread msgThread = new Thread(receiveMesaj);
            msgThread.Start();
        }

        public void receiveMesaj()
        {
            while (true)
            {
                stream = client.GetStream();
                streamReader = new StreamReader(stream);
                receivedText = streamReader.ReadLine();
                if(receivedText == "User existent")
                {
                    MessageBox.Show("Already connected");
                }
                else
                {
                    if(receivedText.Substring(0,6) == "Joined" || receivedText.Substring(0, 6) == "Public")
                    {
                        dispatcher();
                    }
                }
            }
        }

        private void dispatcher()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(dispatcher));
            else
            {
                if (receivedText.Substring(0, 6) == "Joined")
                {
                    chatBox.Text = "You joined the chat!";
                    //userBox.Enabled = false;
                    btnConnect.Text = "Disconnect";
                    listBox1.Items.Clear();
                    s1 = receivedText.Substring(6);
                    while (s1.Contains("."))
                    {
                        listBox1.Items.Add(s1.Substring(0, s1.IndexOf(".")));
                        s1 = s1.Substring(s1.IndexOf(".") + 1);
                    }

                }
                else
                {
                    if (receivedText.Substring(0, 6) == "Public")
                    {
                        chatBox.Text = chatBox.Text + Environment.NewLine + receivedText.Substring(6);
                    }
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            stream = client.GetStream();
            streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(textBox1.Text);
            streamWriter.Flush();
        }       

    }
}
