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

namespace ManagementSystem
{
    /// <summary>
    /// SystemUserAdd.xaml 的交互逻辑
    /// </summary>
    public partial class SystemUserAdd : Window
    {
        private SystemUserAdapter adapter;
        public SystemUserAdd()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            SQLServerDatabase db = new SQLServerDatabase(ConnectionInfo.dbName, ConnectionInfo.serverName);
            adapter = new SystemUserAdapter(db);

            //判断可以添加的用户类型
            if (LoginInfo.LoginID == 0) //登录用户为root用户
                CBI_Admin.IsEnabled = true;
            else
                CBI_Admin.IsEnabled = false;
        }
        //添加系统用户
        private void SysUserAdd_Click(object sender, RoutedEventArgs e)
        {
            DataSet dataSet = null;
            DataTable dt = null;            

            //判断数据正确性
            if(tb_username.Text == "" ||
                tb_passwd.Password == "" ||
                tb_passwdAgain.Password == "")
            {
                MessageBox.Show("未输入完整数据", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if(tb_passwd.Password != tb_passwdAgain.Password)
            {
                MessageBox.Show("输入两次的密码不一致", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //判断登录类型,确定UID
            string query;
            int maxUID,uid;
            if(CBI_Admin.IsSelected == true)
            {
                //query = "Select max(SysUID) as MaxID from SystemUser where SysUID <= 500 ;";                   
                //dt = db.openQueryDT(query,"SystemUser");
                maxUID = adapter.GetMaxUIDOfAdmin();
                if (maxUID + 1 > 500)     //超过添加用户上限
                {
                    MessageBox.Show("超过管理员个数上限", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                //query = "Select max(SysUID) as MaxID from SystemUser where SysUID > 500 ;";
                //dt = db.openQueryDT(query,"SystemUser");
                maxUID = adapter.GetMaxUIDOfUser();
            }

            if (maxUID == -1)
            {
                MessageBox.Show("添加用户错误", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            uid = maxUID + 1;

            //判断是否可以登录
            bool bCanlogin = true;
            if(RadionY.IsChecked == true)       //允许登录
                bCanlogin = true;
            else
                bCanlogin = false;

            SystemUser sysUser = new SystemUser(uid, tb_username.Text, tb_passwd.Password, bCanlogin);
            if (!adapter.AddOneSystemUser(sysUser))
            {
                MessageBox.Show("添加用户错误", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MessageBox.Show("添加用户成功", "提醒", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
