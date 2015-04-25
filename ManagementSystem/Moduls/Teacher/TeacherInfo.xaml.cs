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
    /// Interaction logic for TeacherInfo.xaml
    /// </summary>
    public partial class TeacherInfo : UserControl
    {
        private DBase db = SQLConnection.GetDataBase();
        private DataSet Teacher = new DataSet();
        public TeacherInfo()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            try
            {
                string Query = string.Format("SELECT * FROM Teacher First,Academy Second WHERE First.Anum=Second.Anum AND Tnum = '{0}'", LoginInfo.LoginName);
                Teacher = db.openQueryDS(Query, "Teacher");
                GridTeacherInfo.DataContext = Teacher.Tables["Teacher"].DefaultView;
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
