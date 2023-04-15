using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Aplicatia2
{
    class Program
    {
        static void Main(string[] args)
        {
            new Pc();
        }
    }

    public class Pc
    {
        static ManualResetEvent stocGol = new ManualResetEvent(true);
        static ManualResetEvent stocPlin = new ManualResetEvent(false);
        int contor = 0;
        public Pc()
        {
            Thread tp = new Thread(new ThreadStart(Produce));
            Thread tc = new Thread(new ThreadStart(Consuma));
            tp.Start();
            tc.Start();
            Console.ReadLine();
        }

        public void Produce()
        {
            while (true)
            {
                stocGol.WaitOne();
                for (int i = 0; i < 10; i++)
                {
                    contor++;
                    Console.WriteLine(contor);
                    Thread.Sleep(500);
                }
                stocGol.WaitOne();
                stocPlin.Set();
                stocGol.Reset();
            }
        }

        public void Consuma()
        {
            while (true)
            {
                stocPlin.WaitOne();
                for (int i = 0; i < 10; i++)
                {
                    contor--;
                    Console.WriteLine(contor);
                    Thread.Sleep(500);
                }
                stocGol.Set();
                stocPlin.WaitOne();
                stocPlin.Reset();
            }
        }
    } 
}
