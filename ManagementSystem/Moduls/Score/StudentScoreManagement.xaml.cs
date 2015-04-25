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
    /// Interaction logic for StudentScoreManagement.xaml
    /// </summary>
    public partial class StudentScoreManagement : UserControl
    {
        private string TeacherNum;      //教师工号
        //数据接口        
        private SelectedCourseAdapter selectedCourseAdapter = new SelectedCourseAdapter(
            SQLConnection.GetDataBase());
        private TeachCourseAdapter teachCourseAdapter = new TeachCourseAdapter(
            SQLConnection.GetDataBase());
        private CourseAdapter courseAdapter = new CourseAdapter(
            SQLConnection.GetDataBase());
        //数据表
        private DataSet TeachCourseList = new DataSet();
        private DataSet ScoreList = new DataSet();
        public StudentScoreManagement()
        {
            InitializeComponent();

            //初始化窗口
            Init();
        }

        private void Init()
        {
            try
            {
                TeacherNum = LoginInfo.LoginName;
                TeachCourseList = teachCourseAdapter.GetCourseByTeacherNum(TeacherNum); //获取教师的授课信息

                string TCTableName = "TeachCourse_T";
                DataSet CourseList = courseAdapter.SelectData();
                DataTable CourseTable = CourseList.Tables["Course"];

                CourseList.Tables.Remove("Course");     //移除表
                TeachCourseList.Tables.Add(CourseTable);    //添加子表
                TeachCourseList.Tables["TeachCourse"].TableName = TCTableName;      //修改表名

                TeachCourseList.Relations.Add("TCAndC", TeachCourseList.Tables["Course"].Columns["Cnum"],
                                              TeachCourseList.Tables[TCTableName].Columns["Cnum"]);

                DataSetHelper dsHelper = new DataSetHelper(ref TeachCourseList);
                dsHelper.SelectJoinInto("TeachCourse", TeachCourseList.Tables[TCTableName],
                    "Cnum,Tnum,TCAndC.Cname", "1=1", "Cnum");
                cb_TeachCourseFilter.ItemsSource = TeachCourseList.Tables["TeachCourse"].DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }

        private void UpdateOneScore_Click(object sender, RoutedEventArgs e)
        {
            object item = LV_SelectedCourse.SelectedItem;
            //没选择任何项
            if (item == null)
            {
                MessageBox.Show("请选择一个学生", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            LV_SelectedCourse.ItemsSource = ScoreList.Tables["SelectedCourse"].DefaultView;

        }

        //选择不同授课时
        private void TeachCourseSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = cb_TeachCourseFilter.SelectedItem;

            //没选择任何项
            if (item == null)
            {
                LV_SelectedCourse.Items.Clear();
                return;
            }

            int n = cb_TeachCourseFilter.SelectedIndex;
            string CourseNum = (item as DataRowView)["Cnum"].ToString();

            try
            {
                DBase db = SQLConnection.GetDataBase();
                ScoreList = db.openQueryDS(
                    string.Format("SELECT * FROM SelectedCourse FIRST,Student SECOND WHERE FIRST.Snum=SECOND.Snum AND Cnum='{0}'",CourseNum),"SelectedCourse");
                LV_SelectedCourse.ItemsSource = ScoreList.Tables["SelectedCourse"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ScoreList.HasChanges())
                    selectedCourseAdapter.UpdateData(ScoreList);
                MessageBox.Show("更新成绩成功", "提醒", MessageBoxButton.OK,
                    MessageBoxImage.Information);

                TeachCourseSelectionChanged(null, null);
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
