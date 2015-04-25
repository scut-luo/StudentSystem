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
using DatabaseControlLib;
using System.Data;

namespace ManagementSystem
{
    /// <summary>
    /// Interaction logic for StudentInfoAdd.xaml
    /// </summary>
    public partial class StudentInfoAdd : Window
    {
        private AcademyAdapter academyAdapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        private StudentAdapter studentAdapter = new StudentAdapter(
            SQLConnection.GetDataBase());
        private DataSet Student = new DataSet();
        private DataSet AcademyList = new DataSet(); 
        public StudentInfoAdd()
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

                //AcademyList.Tables["Academy"].Rows.Add(
                //    new object[] { "-1", "无" });

                cb_Sex.Items.Add("男");
                cb_Sex.Items.Add("女");

                Student = studentAdapter.CreateEmptyDataSet();

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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(tb_Snum.Text == "" ||
                tb_Sname.Text == "" ||
                cb_Sex.SelectedIndex == -1 ||
                cb_AcademyList.SelectedIndex == -1)
            {
                MessageBox.Show("请输入完整学生信息", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            try
            {
                int sex = 0;
                switch (cb_Sex.SelectedIndex)
                {
                    case 0:
                        sex = 0;
                        break;

                    case 1:
                        sex = 1;
                        break;
                }
                int n = cb_AcademyList.SelectedIndex;
                int Anum = Convert.ToInt32(AcademyList.Tables["Academy"].Rows[n]["Anum"].ToString());
                Student.Tables["Student"].Rows.Add(
                    new object[] { tb_Snum.Text,
                                   tb_Sname.Text,
                                   sex,
                                   ConvertFunction.ConvertImageToBinary(img_Student.ImageSource),
                                   Anum});
                studentAdapter.UpdateData(Student);
                MessageBox.Show("插入学生信息成功");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接或数据输入是否标准", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
                this.Close();
            }
        }
    }
}
