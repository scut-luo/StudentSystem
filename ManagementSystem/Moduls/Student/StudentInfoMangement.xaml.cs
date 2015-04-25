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

using Microsoft.Win32;
using DatabaseControlLib;
using System.Data;

namespace ManagementSystem
{
    /// <summary>
    /// Interaction logic for StudentInfoMangement.xaml
    /// </summary>
    public partial class StudentInfoMangement : UserControl
    {
        private AcademyAdapter academyAdapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        private StudentAdapter studentAdapter = new StudentAdapter(
            SQLConnection.GetDataBase());
        private DBase db = SQLConnection.GetDataBase();
        private DataSet StudentList = new DataSet();
        private DataSet AcademyList = new DataSet();        
        public StudentInfoMangement()
        {
            InitializeComponent();

            Init();
        }

        private void Clear()
        {
            cb_Sex.SelectedIndex = -1;
            cb_AcademyList.SelectedIndex = -1;
        }

        private void Init()
        {
            try
            {
                AcademyList = academyAdapter.SelectData();
                cb_AcademyFilter.ItemsSource = AcademyList.Tables["Academy"].DefaultView;
                cb_AcademyList.ItemsSource = AcademyList.Tables["Academy"].DefaultView;

                //AcademyList.Tables["Academy"].Rows.Add(
                //    new object[] { "-1", "无" });

                cb_Sex.Items.Add("男");
                cb_Sex.Items.Add("女");

                cb_Filter.Items.Add("学号");
                cb_Filter.Items.Add("名字");
                cb_Filter.SelectedIndex = 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                //写入日志
            }
        }
        private void UpdatePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog().Value)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(@openFileDialog.FileName);
                image.EndInit();
                img_Student.ImageSource = image;  //替换照片，将自动调用BinaryImageConverter第2个方法
            }
        }

        private void StudentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = LV_Student.SelectedItem;
            if (item == null)
                return;

            int n = cb_AcademyFilter.SelectedIndex;
            cb_AcademyList.SelectedIndex = n;
          
            string isMan = (item as DataRowView)["Ssex"].ToString();
            if (isMan == "0")
                cb_Sex.SelectedIndex = 0;
            else
                cb_Sex.SelectedIndex = 1;
        }

        //选择不同的学院
        private void AcademySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = cb_AcademyFilter.SelectedItem;
            if (item == null)
            {
                return;
            }

            string Anum = (item as DataRowView)["Anum"].ToString();

            try
            {
                StudentList = studentAdapter.GetStudentInfoByAcademyNum(Anum);
                LV_Student.ItemsSource = StudentList.Tables["Student"].DefaultView;
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                //写入日志
            }
        }

        //更新选择的学生的信息
        private void UpdateStudentInfo_Click(object sender, RoutedEventArgs e)
        {
            object item = LV_Student.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择一名学生", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            int n;
            DataRowView oneRow = item as DataRowView;
            //修改选择学生数据到数据库
            try
            {
                oneRow["Sname"] = tb_Sname.Text;
                oneRow["Sphoto"] = ConvertFunction.ConvertImageToBinary(img_Student.ImageSource);
                //选择不同学院时
                n = cb_AcademyList.SelectedIndex;
                oneRow["Anum"] = AcademyList.Tables["Academy"].Rows[n]["Anum"].ToString();
                //选择不同性别时
                switch (cb_Sex.SelectedIndex)
                {
                    case 0:
                        oneRow["Ssex"] = 0;
                        break;
                        
                    case 1:
                        oneRow["Ssex"] = 1;
                        break;    
                }

                studentAdapter.UpdateData(StudentList);
                MessageBox.Show("更新学生信息成功", "提醒", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                AcademySelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }

        //添加一个新的学生信息
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            StudentInfoAdd sia = new StudentInfoAdd();
            sia.ShowDialog();
            AcademySelectionChanged(null,null);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int n = LV_Student.SelectedIndex;
            if (n == -1)
            {
                MessageBox.Show("请选择一名学生", "错误",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                StudentList.Tables["Student"].Rows[n].Delete();
                studentAdapter.UpdateData(StudentList);
                MessageBox.Show("删除学生信息成功", "提醒", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
            AcademySelectionChanged(null, null);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            int n = cb_Filter.SelectedIndex;
            int index = cb_AcademyFilter.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("请选择一个学院", "错误",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int Anum = Convert.ToInt32(AcademyList.Tables["Academy"].Rows[index]["Anum"].ToString());

            try
            {
                string Query;
                if (n == 0) //按照学号查询
                {
                    Query = string.Format("SELECT * FROM Student WHERE Anum = {0} AND Snum LIKE '%{1}%'",Anum, tb_Filter.Text);
                    StudentList = db.openQueryDS(Query, "Student");
                }
                else        //按照名字查询
                {
                    Query = string.Format("SELECT * FROM Student WHERE Anum = {0} AND Sname LIKE '%{1}%'",Anum, tb_Filter.Text);
                    StudentList = db.openQueryDS(Query, "Student");
                }
                LV_Student.ItemsSource = StudentList.Tables["Student"].DefaultView;
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
