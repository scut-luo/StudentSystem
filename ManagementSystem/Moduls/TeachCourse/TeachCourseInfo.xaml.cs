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

using System.Data;
using DatabaseControlLib;

namespace ManagementSystem
{
    /// <summary>
    /// Interaction logic for TeachCourseInfo.xaml
    /// </summary>
    public partial class TeachCourseInfo : UserControl
    {        
        private DataSet TeachCourseList = new DataSet();
        private string TeacherNum;
        public TeachCourseInfo()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            try
            {
                TeacherNum = LoginInfo.LoginName;
                string Query = string.Format("SELECT * FROM Course FIRST,TeachCourse SECOND WHERE FIRST.Cnum=SECOND.Cnum AND Tnum='{0}'", TeacherNum);
                DBase db = SQLConnection.GetDataBase();
                TeachCourseList = db.openQueryDS(Query, "TeachCourse");

                LV_TeachCourse.ItemsSource = TeachCourseList.Tables["TeachCourse"].DefaultView;
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
