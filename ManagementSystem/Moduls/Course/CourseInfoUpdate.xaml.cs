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
    /// CourseInfoUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class CourseInfoUpdate : Window
    {
        //接口       
        private CourseAdapter cadapter = new CourseAdapter(
            SQLConnection.GetDataBase());
        //数据表
        private ObservableCollection<Academy> AcademyList = 
            new ObservableCollection<Academy>();        //学院列表
        //其他
        private DataSet CourseInfo = new DataSet();
        private bool bAddCourse = true;

        public CourseInfoUpdate()
        {
            InitializeComponent();

            //初始化窗口
            Init();
        }

        public CourseInfoUpdate(bool baddCourse, DataSet courseInfo)
        {
            InitializeComponent();

            bAddCourse = baddCourse;
            CourseInfo = courseInfo;

            //初始化窗口
            Init();
        }
        private void Init()
        {
            try
            {                                
                AcademyAdapter adapter = new AcademyAdapter(
                    SQLConnection.GetDataBase());
                AcademyList = adapter.GetAllAcademyC() as ObservableCollection<Academy>;
                foreach (Academy academy in AcademyList)
                {
                    cb_Anum.Items.Add(academy.Aname);
                }                

                if (!bAddCourse)     //修改课程信息
                {
                    tb_Cnum.IsReadOnly = true;
                    if (DBNull.Value.Equals(CourseInfo.Tables["Course"].Rows[0]["Anum"]))
                    {
                        cb_Anum.SelectedIndex = -1;
                    }
                    else
                    {
                        int Anum = Convert.ToInt32(CourseInfo.Tables["Course"].Rows[0]["Anum"].ToString());
                        for (int i = 0; i != AcademyList.Count; i++)
                        {
                            if (AcademyList[i].Anum == Anum)
                            {
                                cb_Anum.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    CourseInfo.AcceptChanges();
                    Grid_CourseInfo.DataContext = CourseInfo.Tables["Course"].DefaultView;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
                this.Close();
            }            
        }

        //保存更改
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(tb_Cnum.Text == "" ||
                tb_Cname.Text == "" ||
                tb_Ccredit.Text == "" ||
                tb_Chours.Text == "")
            {
                MessageBox.Show("请输入完整的课程信息", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            
            int n = cb_Anum.SelectedIndex;

            try
            {
                if (bAddCourse)
                {
                    Course course = new Course(tb_Cnum.Text,
                                       tb_Cname.Text,
                                       Convert.ToDouble(tb_Ccredit.Text),
                                       Convert.ToInt32(tb_Chours.Text),
                                       AcademyList[n].Anum);
                    cadapter.AddOneCourse(course);
                    MessageBox.Show("添加课程信息成功", "提醒", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    cadapter.UpdateData(CourseInfo);
                    MessageBox.Show("更新课程信息成功", "提醒", MessageBoxButton.OK,
                        MessageBoxImage.Information);                    
                }
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接或数据输入是否符合标准", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }                      
        }

        private void AcademySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = cb_Anum.SelectedItem;
            if (item == null)
                return;
            if (!bAddCourse)
            {
                int n = cb_Anum.SelectedIndex;
                CourseInfo.Tables["Course"].Rows[0]["Anum"] = AcademyList[n].Anum;
            }            
        }
    }
}
