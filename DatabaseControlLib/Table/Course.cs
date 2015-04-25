using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseControlLib
{
    public class Course
    {
        private string m_Cnum;
        private string m_Cname;
        private double m_Ccredit;
        private int m_Chours;
        private int m_Anum;

        //课程号
        public string Cnum
        {
            get { return m_Cnum; }
            set
            {
                m_Cnum = value;
            }
        }

        //课程名
        public string Cname
        {
            get { return m_Cname; }
            set
            {
                m_Cname = value;
            }
        }

        //学分
        public double Ccredit
        {
            get { return m_Ccredit; }
            set
            {
                m_Ccredit = value;
            }
        }

        //学时
        public int Chours
        {
            get { return m_Chours; }
            set
            {
                m_Chours = value;
            }
        }

        //学院号
        public int Anum
        {
            get { return m_Anum; }
            set
            {
                m_Anum = value;
            }
        }

        //构造函数
        public Course()
        {

        }
        public Course(string cnum, string cname, double ccredit, int chours, int anum)
        {
            m_Cnum = cnum;
            m_Cname = cname;
            m_Ccredit = ccredit;
            m_Chours = chours;
            m_Anum = anum;
        }
    }
}
