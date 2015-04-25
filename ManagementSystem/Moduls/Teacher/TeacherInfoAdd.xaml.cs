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

using Microsoft.Win32;
using System.Data;
using DatabaseControlLib;

namespace ManagementSystem
{
    /// <summary>
    /// Interaction logic for TeacherInfoAdd.xaml
    /// </summary>
    public partial class TeacherInfoAdd : Window
    {
        private DataSet AcademyList = new DataSet();
        private DataSet Teacher = new DataSet();
        private AcademyAdapter academyAdapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        private TeacherAdapter teacherAdapter = new TeacherAdapter(
            SQLConnection.GetDataBase());
        public TeacherInfoAdd()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            try
            {
                AcademyList = academyAdapter.SelectData();
                cb_AcademyList.ItemsSource = AcademyList.Tables["Academy"].DefaultView;

                Teacher = teacherAdapter.CreateEmptyDataSet();
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(tb_Tnum.Text == "" ||
                tb_Tname.Text == "" ||
                tb_Tphone.Text == "" ||
                cb_AcademyList.SelectedIndex == -1)
            {
                MessageBox.Show("请输入完整教师信息", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            try
            {
                int n = cb_AcademyList.SelectedIndex;
                int Anum = Convert.ToInt32(AcademyList.Tables["Academy"].Rows[n]["Anum"].ToString());
                Teacher.Tables["Teacher"].Rows.Add(
                    new object[] {tb_Tnum.Text,
                                  tb_Tname.Text,
                                  tb_Tphone.Text,
                                  ConvertFunction.ConvertImageToBinary(img_Teacher.ImageSource),
                                  Anum});
                teacherAdapter.UpdateData(Teacher);
                MessageBox.Show("插入教师信息成功");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接或数据输入是否符合标准", "错误", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                //写入日志
                this.Close();
            }
        }
    }
}
