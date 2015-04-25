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

namespace ManagementSystem
{
    /// <summary>
    /// AcademyInfoUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class AcademyInfoUpdate : Window
    {
        private AcademyAdapter adapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        public AcademyInfoUpdate()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if(tb_Anum.Text == "" ||
                    tb_Aname.Text == "")
                {
                    MessageBox.Show("请输入完整的学院信息", "错误", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
                adapter.AddOneAcademy(new Academy(Convert.ToInt32(tb_Anum.Text), tb_Aname.Text));

                MessageBox.Show("添加学院信息成功", "提醒", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接或数据输入是否符合标准", "错误", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                //写入日志
            }
        }
    }
}
