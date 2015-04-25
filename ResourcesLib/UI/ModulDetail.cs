using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourcesLib.UI
{
    public class ModulDetail
    {
        public int DetailNum    //子模块的编号
        {
            get;
            set;
        }
        public int ModulNum     //模块的编号
        {
            get;
            set;
        }
        public string ShowImage //模块对应的图片
        {
            get;
            set;
        }
        public string DetailName //子模块名字
        {
            get;
            set;
        }
        public string UriPath   //子模块对应的界面(xaml)
        {
            get;
            set;
        }
        public ModulDetail(int detailNum,int modulNum,string showImage,
            string detailName, string uriPath)
        {
            DetailNum = detailNum;
            ModulNum = modulNum;
            ShowImage = showImage;
            DetailName = detailName;
            UriPath = uriPath;
        }
    }
}
