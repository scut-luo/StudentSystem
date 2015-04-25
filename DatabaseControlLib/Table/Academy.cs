using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseControlLib
{
    public class Academy
    {
        private int m_Anum;     //学院号
        private string m_Aname; //学院名称

        //学院号
        public int Anum
        {
            get { return m_Anum; }
            set
            {
                m_Anum = value;
            }
        }

        //学院名称
        public string Aname
        {
            get { return m_Aname; }
            set
            {
                m_Aname = value;
            }
        }

        //构造函数
        public Academy()
        {

        }
        public Academy(int anum, string aname)
        {
            m_Anum = anum;
            m_Aname = aname;
        }
    }
}
