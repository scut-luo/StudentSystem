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
using System.Collections.ObjectModel;

namespace ManagementSystem
{
    /// <summary>
    /// TeachCourseManagement.xaml 的交互逻辑
    /// </summary>
    public partial class TeachCourseManagement : UserControl
    {
        //数据表操作接口
        private DBase db = SQLConnection.GetDataBase();
        private CourseAdapter courseAdapter = new CourseAdapter(
            SQLConnection.GetDataBase());
        private AcademyAdapter academyAdapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        private TeacherAdapter teacherAdapter = new TeacherAdapter(
            SQLConnection.GetDataBase());
        private TeachCourseAdapter teachCourseAdapter = new TeachCourseAdapter(
            SQLConnection.GetDataBase());
        //数据表
        private DataSet CourseList = new DataSet();         //课程列表
        private DataSet TeacherList = new DataSet();        //教师信息表
        private DataSet TeachCourseList = new DataSet();    //教师授课表
        private ObservableCollection<Academy> AcademyList =
                            new ObservableCollection<Academy>();
        public TeachCourseManagement()
        {
            InitializeComponent();
            
            //初始化窗口
            Init();            
        }

        private void Init()
        {
            try
            {
                AcademyList = academyAdapter.GetAllAcademyC() as
                    ObservableCollection<Academy>;          //获取所有学院信息
                cb_AcademyFilter.ItemsSource = AcademyList;
                cb_AcademyFilter.SelectedIndex = 0;

                //获取所有课程信息
                CourseList = courseAdapter.SelectData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }       

        //选择不同学院时,显示不同教师信息
        private void AcademySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            object item = comboBox.SelectedItem;

            //没选择任何项
            if (item == null)
                return;

            int n = comboBox.SelectedIndex;
            int AcademyNum = AcademyList[n].Anum;

            try
            {
                TeacherList = teacherAdapter.GetTeacherByAcademy(AcademyNum);
                LV_Teacher.ItemsSource = TeacherList.Tables["Teacher"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志                
            }
        }

        //选择不同的教师时,显示其授课信息
        private void TeacherSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = LV_Teacher.SelectedItem;

            //没选择任何项
            if (item == null)
                return;

            try
            {
                /*
                string Tnum = (item as DataRowView)["Tnum"].ToString();
                TeachCourseList = teachCourseAdapter.GetCourseByTeacherNum(Tnum);       //获取选中教师的授课信息

                string TeachCoureName = "TeachCourse_T";
                TeachCourseList.Tables["TeachCourse"].TableName = TeachCoureName;  //修改原表名

                DataTable CourseTable = CourseList.Tables["Course"];
                CourseList.Tables.Remove("Course");
                TeachCourseList.Tables.Add(CourseTable);        //添加子表

                TeachCourseList.Relations.Add("TCAndC", TeachCourseList.Tables["Course"].Columns["Cnum"],
                                              TeachCourseList.Tables[TeachCoureName].Columns["Cnum"]);

                DataSetHelper dsHelper = new DataSetHelper(ref TeachCourseList);
                dsHelper.SelectJoinInto("TeachCourse", TeachCourseList.Tables[TeachCoureName],
                                        "Cnum,Tnum,TCplace,TCAndC.Cname", "1=1", "Cnum");

                */
                string Tnum = (item as DataRowView)["Tnum"].ToString();
                string Query = string.Format("SELECT * FROM TeachCourse First,Course Second WHERE First.Cnum=Second.Cnum AND Tnum='{0}'", Tnum);
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

        //添加或修改教师
        private void UpdateTeachCourse_Click(object sender, RoutedEventArgs e)
        {
            object item = LV_Teacher.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择一名教师", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            string TeacherNum = (item as DataRowView)["Tnum"].ToString();       //获取选择的教师的工号

            TeachCourseUpdate tcu = new TeachCourseUpdate(TeacherNum);
            tcu.ShowDialog();

            TeacherSelectionChanged(null, null);
        }        
    }
}
