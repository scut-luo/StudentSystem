using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourcesLib.UI
{
    //代表UI中每一个Modul
    public class Modul
    {
        public int ModulNum
        {
            get;
            set;
        }
        public string ModulName
        {
            get;
            set;
        }

        public Modul(int modulNum, string modulName)
        {
            ModulNum = modulNum;
            ModulName = modulName;
        }
    }
}
