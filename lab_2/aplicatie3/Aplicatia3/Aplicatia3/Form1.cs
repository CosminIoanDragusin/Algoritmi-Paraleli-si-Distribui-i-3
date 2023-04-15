using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicatia3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            new Lift();
        }
        public class Lift 
        {
            /* static ManualResetEvent sosireLiftului = new ManualResetEvent(false);
             static ManualResetEvent liftEtaj1 = new ManualResetEvent(false);
             static ManualResetEvent liftEtaj2 = new ManualResetEvent(false);
             static ManualResetEvent liftEtaj3 = new ManualResetEvent(false);
             static ManualResetEvent liftEtaj4 = new ManualResetEvent(false);
             static ManualResetEvent liftEtaj5 = new ManualResetEvent(false);
             static ManualResetEvent liftEtaj6 = new ManualResetEvent(false);
             static ManualResetEvent liftParter = new ManualResetEvent(false);
            */
            static ManualResetEvent urca = new ManualResetEvent(true);
            static ManualResetEvent coboara = new ManualResetEvent(false);
            public int etajCurent=0;
            

            public Lift()
            {
                Thread tcoboara = new Thread(new ThreadStart(Coboara));
                Thread turca = new Thread(new ThreadStart(Urca));
                // Thread tloc2 = new Thread(new ThreadStart(UrcaEt2));
                // Thread tloc3 = new Thread(new ThreadStart(UrcaEt3));
                tcoboara.Start();
                turca.Start();
              //  tloc2.Start();
               // tloc3.Start();
                Console.ReadLine();
            }
            
            public void Urca()
            {
                while (true)
                {
                    urca.WaitOne();
                    for (int i = 0; i < 6; i++)
                    {
                        //etajCurent= trackBar1.Value.ToString();
                        
                        Thread.Sleep(500);
                    }
                    urca.WaitOne();
                    coboara.Set();
                    urca.Reset();
                }
            }
            public void Coboara()
            {
                while (true)
                {
                    coboara.WaitOne();
                    for (int i = 0; i < 10; i++)
                    {
                       // trackBar1--;
                        
                        Thread.Sleep(500);
                    }
                    urca.Set();
                    coboara.WaitOne();
                    coboara.Reset();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 1;
            checkBox1.Checked=true;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 2;
            checkBox1.Checked = false;
            checkBox2.Checked = true;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 3;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = true;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 4;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = true;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 5;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = true;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 6;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = true;
            checkBox7.Checked = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 0;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = true;
        }
    }
}
