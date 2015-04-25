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

namespace ManagementSystem
{
    /// <summary>
    /// AcademyManagement.xaml 的交互逻辑
    /// </summary>
    public partial class AcademyManagement : UserControl
    {
        private AcademyAdapter adapter = new AcademyAdapter(
            SQLConnection.GetDataBase());
        private DataSet AcademyInfoList;
        public AcademyManagement()
        {
            InitializeComponent();

            //加载数据
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                //获取所有学院信息
                AcademyInfoList = adapter.SelectData();
                LV_Academy.ItemsSource = AcademyInfoList.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                //写入日志
            }
        }

        //添加学院信息
        private void AddAcademy_Click(object sender, RoutedEventArgs e)
        {
            AcademyInfoUpdate aiu = new AcademyInfoUpdate();
            aiu.ShowDialog();
            LoadData();
        }
    }
}
