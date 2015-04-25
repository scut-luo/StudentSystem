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
using System.Data;
using DatabaseControlLib;

namespace ManagementSystem
{
    /// <summary>
    /// Interaction logic for TeacherInfoManagement.xaml
    /// </summary>
    public partial class TeacherInfoManagement : UserControl
    {
        //数据接口
        private DBase db = SQLConnection.GetDataBase();
        private TeacherAdapter teacherAdapter = new TeacherAdapter(
            SQLConnection.GetDataBase());
        private AcademyAdapter academyAdapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        //数据表
        private DataSet TeacherList = new DataSet();
        private DataSet AcademyList = new DataSet();
        public TeacherInfoManagement()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            try
            {
                AcademyList = academyAdapter.SelectData();
                cb_AcademyFilter.ItemsSource = AcademyList.Tables["Academy"].DefaultView;

                //DataSet dataSet = academyAdapter.SelectData();
                cb_AcademyList.ItemsSource = AcademyList.Tables["Academy"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
        }

        private void Clear()
        {
            cb_AcademyList.SelectedIndex = -1;
        }

        private void AcademySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = cb_AcademyFilter.SelectedItem;
            //没选择任何项
            if (item == null)
                return;

            int AcademyNum = Convert.ToInt32((item as DataRowView)["Anum"].ToString());
            try
            {
                TeacherList = teacherAdapter.GetTeacherByAcademy(AcademyNum);
                LV_Teacher.ItemsSource = TeacherList.Tables["Teacher"].DefaultView;
                Clear();
               
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
                img_Teacher.ImageSource = image;  //替换照片，将自动调用BinaryImageConverter第2个方法
            }
        }

        private void TeacherSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = LV_Teacher.SelectedItem;
            if (item == null)
                return;
            
            int n = cb_AcademyFilter.SelectedIndex;
            cb_AcademyList.SelectedIndex = n;
        }

        private void UpdateTeacherInfo_Click(object sender, RoutedEventArgs e)
        {
            object item = LV_Teacher.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择一名教师", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            int n;
            DataRowView oneRow = item as DataRowView;

            //修改教师数据到数据库
            try
            {
                oneRow["Tname"] = tb_Tname.Text;
                oneRow["Tphone"] = tb_Tphone.Text;
                oneRow["Tphoto"] = ConvertFunction.ConvertImageToBinary(img_Teacher.ImageSource);
                //选择不同学院时
                n = cb_AcademyList.SelectedIndex;
                oneRow["Anum"] = Convert.ToInt32(AcademyList.Tables["Academy"].Rows[n]["Anum"].ToString());

                teacherAdapter.UpdateData(TeacherList);
                MessageBox.Show("更新教师信息成功", "提醒", MessageBoxButton.OK,
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TeacherInfoAdd tia = new TeacherInfoAdd();
            tia.ShowDialog();
            AcademySelectionChanged(null, null);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int n = LV_Teacher.SelectedIndex;
            if (n == -1)
            {
                MessageBox.Show("请选择一个教师", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            try
            {
                TeacherList.Tables["Teacher"].Rows[n].Delete();
                teacherAdapter.UpdateData(TeacherList);
                MessageBox.Show("删除教师信息成功", "提醒", MessageBoxButton.OK,
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

    }
}
