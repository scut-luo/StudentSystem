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
using System.Windows.Shapes;

using DatabaseControlLib;
using System.Data;

namespace ManagementSystem
{
    /// <summary>
    /// Interaction logic for TeachCourseUpdate.xaml
    /// </summary>
    public partial class TeachCourseUpdate : Window
    {
        private string TeacherNum;      //教师工号
        private DataSet Teacher = new DataSet();        //教师信息
        private DataSet TeachCourseList = new DataSet();    //授课表
        //数据接口
        private CourseAdapter courseAdapter = new CourseAdapter(
            SQLConnection.GetDataBase());
        private TeacherAdapter teacherAdapter = new TeacherAdapter(
            SQLConnection.GetDataBase());
        private TeachCourseAdapter teachCourseAdapter = new TeachCourseAdapter(
            SQLConnection.GetDataBase());
        public TeachCourseUpdate(string teacherNum)
        {
            InitializeComponent();

            TeacherNum = teacherNum;

            //初始化窗口
            Init();
        }

        private void Init()
        {
            //初始化窗口
            try
            {    
                //获取教师信息
                Teacher = teacherAdapter.GetTeacherByTeacherNum(TeacherNum);
                //数据绑定
                Grid_Teacher.DataContext = Teacher.Tables[0].DefaultView;

                //获取所有课程信息
                DataSet CourseList = courseAdapter.SelectData();

                //获取选择的教师的授课信息
                TeachCourseList = teachCourseAdapter.GetCourseByTeacherNum(TeacherNum);

                string TeachCoureName = "TeachCourse_T";
                TeachCourseList.Tables["TeachCourse"].TableName = TeachCoureName;  //修改原表名
                
                DataTable CourseTable = CourseList.Tables["Course"];
                CourseList.Tables.Remove("Course");
                TeachCourseList.Tables.Add(CourseTable);        //添加子表

                TeachCourseList.Relations.Add("TCAndC",TeachCourseList.Tables["Course"].Columns["Cnum"],
                                              TeachCourseList.Tables[TeachCoureName].Columns["Cnum"]);
                
                DataSetHelper dsHelper = new DataSetHelper(ref TeachCourseList);
                dsHelper.SelectJoinInto("TeachCourse",TeachCourseList.Tables[TeachCoureName],
                                        "Cnum,Tnum,TCplace,TCAndC.Cname","1=1","Cnum");

                //这里很重要
                TeachCourseList.Tables["TeachCourse"].AcceptChanges();
                //数据绑定
                LV_TeachCourse.ItemsSource = TeachCourseList.Tables["TeachCourse"].DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
                this.Close();
            }

        }

        private void AddOneTeachCourse_Click(object sender, RoutedEventArgs e)
        {
            if(tb_Cname.Text == "" ||
               tb_TCplace.Text == "")
            {
                MessageBox.Show("请输入完整授课信息", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            //顺序不能错
            TeachCourseList.Tables["TeachCourse"].Rows.Add(
                new object[] { tb_Cname.Tag,
                               tb_Tnum.Text,
                               tb_TCplace.Text,
                               tb_Cname.Text});
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                teachCourseAdapter.UpdateData(TeachCourseList);

                MessageBox.Show("更新教师授课信息成功", "提醒", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接或输入数据是否正确", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
                this.Close();
            }
        }

        private void CourseList_Click(object sender, RoutedEventArgs e)
        {
            TCCourseListWindow clw = new TCCourseListWindow();
            clw.Owner = this;
            clw.ShowDialog();
        }
    }
}
