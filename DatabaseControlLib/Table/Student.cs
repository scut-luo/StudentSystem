using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseControlLib
{
    public class Student
    {
        public int id
        {
            get;
            set;
        }
        public string Snum
        {
            get;
            set;
        }
        public string Sname
        {
            get;
            set;
        }
        public int Ssex
        {
            get;
            set;
        }
        public byte[] Sphoto
        {
            get;
            set;
        }
        public int Anum
        {
            get;
            set;
        }

        public Student()
        {

        }
        public Student(int sid,string snum,string sname,
            int ssex, byte[] sphoto, int anum)
        {
            id = sid;
            Snum = snum;
            Sname = sname;
            Ssex = ssex;
            Sphoto = sphoto;
            Anum = anum;
        }
    }
}
