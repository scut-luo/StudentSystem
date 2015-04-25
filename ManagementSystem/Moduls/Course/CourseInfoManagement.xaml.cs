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
    /// CourseInfoManagement.xaml 的交互逻辑
    /// </summary>
    public partial class CourseInfoManagement : UserControl
    {       
        private DataSet courseList = new DataSet();         //课程信息列表
        private AcademyAdapter academyAdapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        private CourseAdapter courseAdapter = new CourseAdapter(
            SQLConnection.GetDataBase());

        public CourseInfoManagement()
        {
            InitializeComponent();

            //加载数据
            LoadData();
        }

        private void LoadData()
        {
            //获取所有课程信息
            try
            {
                DataSet academyList = new DataSet();        //学院信息列表

                academyList = academyAdapter.SelectData();
                courseList = courseAdapter.SelectData();

                DataSetHelper dsHelper = new DataSetHelper(ref courseList);
                DataTable table = academyList.Tables["Academy"];

                academyList.Tables.Remove("Academy");
                courseList.Tables.Add(table);       //添加子表
                courseList.Relations.Add("CAndA", courseList.Tables["Academy"].Columns["Anum"],
                                          courseList.Tables["Course"].Columns["Anum"]);
                dsHelper.SelectJoinInto("CourseAndAcademy", courseList.Tables["Course"],
                                         "Cnum,Cname,Ccredit,Chours,Anum,CAndA.Aname", "1=1", "Cnum");
                LV_Course.ItemsSource = courseList.Tables["CourseAndAcademy"].DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }

        //添加课程信息
        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            CourseInfoUpdate ciu = new CourseInfoUpdate();
            ciu.ShowDialog();
            LoadData();
        }

        //更改课程信息
        private void EditCourse_Click(object sender, RoutedEventArgs e)
        {
            object item = LV_Course.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择一门课程", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            DataRowView oneRow = item as DataRowView;
            CourseInfoUpdate ciu =
                new CourseInfoUpdate(false,courseAdapter.GetCourseByCourseNum(oneRow["Cnum"].ToString()));
            ciu.ShowDialog();
            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int n = LV_Course.SelectedIndex;
            if (n == -1)
            {
                MessageBox.Show("请选择要删除的课程", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            try
            {
                courseList.Tables["Course"].Rows[n].Delete();
                courseAdapter.UpdateData(courseList);
                MessageBox.Show("删除课程成功", "提醒", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                LoadData();
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }            
        }
    }
}
