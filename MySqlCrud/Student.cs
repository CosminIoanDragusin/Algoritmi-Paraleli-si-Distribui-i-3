using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlCrud
{
    class Student
    {
        public string Id { get; set; }

        public string Nume { get; set; }

        public String Media { get; set; }

        public Student(string nume, String media)
        {
            Nume = nume;
            Media = media;
        }
    }
}
