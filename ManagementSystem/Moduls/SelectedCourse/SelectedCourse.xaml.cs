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
    /// SelectedCourse.xaml 的交互逻辑
    /// </summary>
    public partial class SelectedCourse : UserControl
    {
        private SelectedCourseAdapter selectedCourseAdapter = new SelectedCourseAdapter(
            SQLConnection.GetDataBase());
        private AcademyAdapter academyAdapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        private CourseAdapter courseAdapter = new CourseAdapter(
            SQLConnection.GetDataBase());
        private DataSet SelectedCourseList = new DataSet();         //已选课程的信息列表
        public SelectedCourse()
        {
            InitializeComponent();

            LoadData();
        }

        //如果学生已有选课,显示已选课程信息
        private void LoadData()
        {
            if(LoginInfo.LoginType != UserType.Student)
                return;
            try
            {
                //获取学生已选课程
                SelectedCourseList = selectedCourseAdapter.GetSelectedCourseByStudentNum(
                                                LoginInfo.LoginName);

                DataSetHelper scHelper = new DataSetHelper(ref SelectedCourseList);
                DataSet CourseList = courseAdapter.SelectData();
                DataTable CourseTable = CourseList.Tables["Course"];

                //CourseTable.Columns.Add("Aname",Type.GetType("System.String"));     //添加列
                //偷梁换柱
                SelectedCourseList.Tables["SelectedCourse"].TableName = "SC";   //改表名
                CourseList.Tables.Remove("Course"); 
                SelectedCourseList.Tables.Add(CourseTable);     //添加子表
                SelectedCourseList.Relations.Add("SCAndC", SelectedCourseList.Tables["Course"].Columns["Cnum"],
                                                    SelectedCourseList.Tables["SC"].Columns["Cnum"]);
                scHelper.SelectJoinInto("SelectedCourse", SelectedCourseList.Tables["SC"],
                                        "SCAndC.Cnum,SCAndC.Cname,SCAndC.Ccredit,SCAndC.Chours,SCAndC.Anum,Snum,Score",
                                        "1=1", "Cnum");

                LV_SelectedCourse.ItemsSource = SelectedCourseList.Tables["SelectedCourse"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }

        //保存选课
        private void SaveSelectedCourse_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCourseList == null)
                return;
            if(LoginInfo.LoginType != UserType.Student) //不是学生
                return;

            try
            {
                if(SelectedCourseList.HasChanges())
                    selectedCourseAdapter.UpdateData(SelectedCourseList);
                MessageBox.Show("更新选课成功", "提醒", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
                 
        }

        //添加一门选课
        private void AddOneCourse_Click(object sender, RoutedEventArgs e)
        {
            CourseListWindow clw = new CourseListWindow(SelectedCourseList);
            clw.ShowDialog();
        }

        //删除一门选课
        private void DeleteOneCourse_Click(object sender, RoutedEventArgs e)
        {
            object item = LV_SelectedCourse.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择一门选课", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            DataRowView oneRow = item as DataRowView;
            oneRow.Delete();
        }
    }
}
