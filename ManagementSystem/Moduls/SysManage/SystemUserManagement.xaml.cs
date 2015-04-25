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
    /// SystemUserManagement.xaml 的交互逻辑
    /// </summary>
    public partial class SystemUserManagement : UserControl
    {
        private SystemUserAdapter adapter;
        private DataSet UserList;

        public SystemUserManagement()
        {
            InitializeComponent();

            Init();         
        }

        private void Init()
        {
            SQLServerDatabase db = new SQLServerDatabase(ConnectionInfo.dbName, ConnectionInfo.serverName);
            adapter = new SystemUserAdapter(db);
            UserList = null;

            //判断用户是否有权限添加、删除、更改和修改权限的权限
            if (LoginInfo.LoginID > 500)        //普通用户
            {
                foreach (Button btn in UserTools.Items)
                {
                    btn.IsEnabled = false;
                    Image image = btn.Content as Image;
                    image.Opacity = 0.5;
                }
            }
        }

        //加载用户数据
        private void LoadData()
        {
            UserList = adapter.SelectData();
            if (UserList == null)
                MessageBox.Show("获取不到用户信息", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                SysUserList.ItemsSource = UserList.Tables[0].DefaultView;
        }

        //添加用户
        private void UserAdd_Click(object sender, RoutedEventArgs e)
        {
            SystemUserAdd userAdd = new SystemUserAdd();
            userAdd.ShowDialog();
            LoadData(); 
        }

        //修改用户信息
        private void UserUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        //删除用户
        private void UserDelete_Click(object sender, RoutedEventArgs e)
        {
            Object item = SysUserList.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("选择要删除的用户", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            DataRowView dataRowView = SysUserList.SelectedItem as DataRowView;
            dataRowView.Delete();
            try
            {
                adapter.UpdateData(UserList);
                MessageBox.Show("删除用户成功", "提醒", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常发生,请检查数据库连接", "错误", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //写入日志
            }
            /*
            if (!adapter.UpdateData(UserList))
            {
                MessageBox.Show("删除用户失败", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                LoadData();
                return;
            }
            else
            {
                MessageBox.Show("删除用户成功", "提醒", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            */

        }

        //初始化
        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
