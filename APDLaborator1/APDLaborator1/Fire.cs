﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APDLaborator1
{
    public class Fire
    {
        public Fire()
        {
            Thread numThread = new Thread(new ThreadStart(numbers));
            Thread letThread = new Thread(new ThreadStart(letters));
            letThread.Start();
            numThread.Start();
        }
        public void numbers()
        {
            for (int i = 0; i < 1000; i++)
                Console.WriteLine(i);
        }
        public void letters()
        {
            char a = 'a';
            for (int i = 0; i < 1000; i++)
            {
                int c = a + i % 26;
                Console.WriteLine((char)c);
            }
        }
    }
    }
