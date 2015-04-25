using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DatabaseControlLib;
using System.Data;

namespace ManagementSystem
{
    /// <summary>
    /// Interaction logic for StudentScoreInfo.xaml
    /// </summary>
    public partial class StudentScoreInfo : UserControl
    {
        private string StudentNum;
        private DataSet ScoreList = new DataSet();
        public StudentScoreInfo()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            StudentNum = LoginInfo.LoginName;   //获取学生学号
            try
            {
                string Query = string.Format("SELECT Cname,Score FROM SelectedCourse FIRST,Course SECOND " +
                                              "WHERE FIRST.Cnum=SECOND.Cnum AND Snum='{0}'", StudentNum);
                DBase db = SQLConnection.GetDataBase();
                ScoreList = db.openQueryDS(Query, "SelectedCourse");
                LV_SelectedCourse.ItemsSource = ScoreList.Tables["SelectedCourse"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }
    }
}
