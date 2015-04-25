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
    public partial class CourseListWindow : Window
    {
        private DataSet SelectedCourseList;         //选课管理界面的选课列表
        private DataSet CourseList = new DataSet();
        private AcademyAdapter academyAdapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        private CourseAdapter courseAdapter = new CourseAdapter(
            SQLConnection.GetDataBase());
        private ObservableCollection<Academy> AcademyList =
                            new ObservableCollection<Academy>();

        public CourseListWindow(DataSet dataSet)
        {
            InitializeComponent();

            SelectedCourseList = dataSet;

            //初始化窗口
            Init();
        }
        public CourseListWindow()
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

        //开始搜索
        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        //选中不同的学院
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
            if (SelectedCourseList == null)
                return;
           
            if(LV_CourseList.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一门课程","提醒",MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            int n = cb_AcademyFilter.SelectedIndex;
            int AcademyNum = AcademyList[n].Anum;       //获取选中的学院编号
            string AcademyName = AcademyList[n].Aname;

            foreach (object selectedItem in LV_CourseList.SelectedItems)
            {
                DataRowView selectedRow = selectedItem as DataRowView;
                //注意这个地方(和选课界面中数据表有很大关系,列顺序不能错)
                SelectedCourseList.Tables["SelectedCourse"].Rows.Add(
                    new object[] { selectedRow["Cnum"].ToString(),
                                   selectedRow["Cname"].ToString(),
                                   Convert.ToDouble(selectedRow["Ccredit"].ToString()),
                                   Convert.ToInt32(selectedRow["Chours"].ToString()),
                                   AcademyNum,
                                   LoginInfo.LoginName});
            }
        }
    }
}
