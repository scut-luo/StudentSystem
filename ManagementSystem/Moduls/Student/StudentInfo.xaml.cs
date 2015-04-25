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
    /// StudentInfo.xaml 的交互逻辑
    /// </summary>
    public partial class StudentInfo : UserControl
    {
        private DataSet Student = new DataSet();
        private DBase db = SQLConnection.GetDataBase();
        public StudentInfo()
        {
            InitializeComponent();

            InitWindow();
        }

        private void InitWindow()
        {
            try
            {
                string Query = string.Format("SELECT * FROM Student First,Academy Second WHERE First.Anum=Second.Anum AND Snum='{0}'", LoginInfo.LoginName);
                Student = db.openQueryDS(Query, "Student");
                GridStudentInfo.DataContext = Student.Tables[0].DefaultView;
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
