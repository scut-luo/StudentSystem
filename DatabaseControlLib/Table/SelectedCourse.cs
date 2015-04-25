using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseControlLib
{
    public class SelectedCourse
    {
        private string m_Snum;
        private string m_Cnum;
        private double m_Score;

        //学号
        public string Snum
        {
            get { return m_Snum; }
            set
            {
                m_Snum = value;
            }
        }
        //课程号
        public string Cnum
        {
            get { return m_Cnum; }
            set
            {
                m_Cnum = value;
            }
        }
        //成绩
        public double Score
        {
            get { return m_Score; }
            set
            {
                m_Score = value;
            }
        }

        //构造函数
        public SelectedCourse()
        {

        }
        public SelectedCourse(string snum, string cnum, double score)
        {
            m_Snum = snum;
            m_Cnum = cnum;
            m_Score = score;
        }
    }
}
