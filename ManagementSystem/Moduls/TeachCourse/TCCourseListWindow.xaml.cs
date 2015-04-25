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

using System.Data;
using DatabaseControlLib;
using System.Collections.ObjectModel;

namespace ManagementSystem
{
    /// <summary>
    /// CourseList.xaml 的交互逻辑
    /// </summary>
    public partial class TCCourseListWindow : Window
    {
        //数据库接口
        private AcademyAdapter academyAdapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        private CourseAdapter courseAdapter = new CourseAdapter(
            SQLConnection.GetDataBase());
        //数据表
        private DataSet CourseList = new DataSet();         //课程列表
        private ObservableCollection<Academy> AcademyList =
                            new ObservableCollection<Academy>();        //学院列表

        public TCCourseListWindow()
        {
            InitializeComponent();

            //初始化窗口
            Init();
        }

        //初始化窗口
        private void Init()
        {
            try
            {
                AcademyList = academyAdapter.GetAllAcademyC() as
                    ObservableCollection<Academy>;
                cb_AcademyFilter.ItemsSource = AcademyList;
                cb_AcademyFilter.SelectedIndex = 0;     //选中第一项
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }

        private void LoadData()
        {
            AcademySelectionChanged(null, null);
        }

        //开始搜索
        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        //选中不同的学院
        private void AcademySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = cb_AcademyFilter.SelectedItem;

            //没选择任何项
            if (item == null)
                return;

            int n = cb_AcademyFilter.SelectedIndex;
            int AcademyNum = AcademyList[n].Anum;

            try
            {
                CourseList = courseAdapter.GetCourseByAcademy(AcademyNum);
                LV_CourseList.ItemsSource = CourseList.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }

        //添加选中的课程
        private void AddSelectedCourse_Click(object sender, RoutedEventArgs e)
        {
            object item = LV_CourseList.SelectedItem;
            if(item == null)
            {
                MessageBox.Show("请选择一门课程","提醒",MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            
            //获取选择的课程的课程号和课程名
            string Cnum = (item as DataRowView)["Cnum"].ToString();
            string Cname = (item as DataRowView)["Cname"].ToString();

            TeachCourseUpdate tcu = this.Owner as TeachCourseUpdate;
            tcu.tb_Cname.Text = Cname;
            tcu.tb_Cname.Tag = Cnum;
        }

        private void ManageCourse_Click(object sender, RoutedEventArgs e)
        {
            CourseManage cm = new CourseManage();
            cm.ShowDialog();
            LoadData();
        }
    }
}
