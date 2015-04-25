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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string RadioButtonState
        {
            get			//只读属性，得到选择的角色
            {
                string s = null;
                if (rb_Student.IsChecked == true)
                    s = rb_Student.Content.ToString();
                else if (rb_Teacher.IsChecked == true)
                    s = rb_Teacher.Content.ToString();
                else if (rb_Admin.IsChecked == true)
                    s = rb_Admin.Content.ToString();
                return s;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        //点击“登录”按钮
        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {           
            if (tb_username.Text == "" || pb_passwd.Password == "")
            {
                MessageBox.Show("所有项都必须填写！", "错误", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string selected = RadioButtonState;
            string userType = UserType.Student;
            SQLServerDatabase db = new SQLServerDatabase(ConnectionInfo.dbName, ConnectionInfo.serverName);
            UserAdapter adapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

           
            //登录验证
            switch (selected)
            {
                case "学生":
                    adapter = new UserAdapter(db, "StudentUser");
                    userType = UserType.Student;
                    break;

                case "教师":
                    adapter = new UserAdapter(db, "TeacherUser");
                    userType = UserType.Teacher;
                    break;

                case "管理员":
                    adapter = new UserAdapter(db, "SystemUser");
                    userType = UserType.Admin;
                    break;
            }
            
            dataSet = adapter.SelectData("*",string.Format("Username = '{0}'",tb_username.Text));
            if (dataSet != null)
            {
                dataTable = dataSet.Tables[0];
                if (dataTable != null)
                {
                    if (dataTable.Rows.Count != 0)   //记录数为0，表示没有该用户
                    {
                        if (!((bool)dataTable.Rows[0]["Canlogin"]))    //账号已过期
                        {
                            MessageBox.Show("账号已过期！\n激活请联系管理员", "错误",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (dataTable.Rows[0]["Password"].ToString() == pb_passwd.Password)  //密码输入正确
                        {
                            //登录主界面                            
                            LoginInfo.LoginName = tb_username.Text;
                            LoginInfo.LoginType = userType;

                            //获取LoginUID
                            if (userType == UserType.Admin)
                                LoginInfo.LoginID = (int)dataTable.Rows[0]["UID"];

                            this.Visibility = System.Windows.Visibility.Hidden;

                            MainFrame mainFrame = new MainFrame(userType);
                            mainFrame.Owner = this;
                            mainFrame.ShowDialog();
                            this.Visibility = System.Windows.Visibility.Visible;

                            tb_username.Text = "";
                            pb_passwd.Password = "";
                        }
                        else
                            MessageBox.Show("密码错误！", "错误",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("用户名不存在！", "错误",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("发生未知错误！", "错误",
                            MessageBoxButton.OK, MessageBoxImage.Error);
            }

            #region 老方式验证
            /*
            string selected = RadioButtonState;
            SQLServerDatabase db = new SQLServerDatabase(ConnectionInfo.dbName,ConnectionInfo.serverName);
            DataTable table = null;
            DataSet dataSet = null;

            switch (selected)
            {
                case "学生":
                    StudentUserControl suc = new StudentUserControl(db);
                    table = suc.GetDataByStudentNum(tb_username.Text);
                    if (table != null)
                    {
                        if (table.Rows.Count != 0)   //记录数为0，表示没有该学号学生
                        {
                            if (!((bool)table.Rows[0]["Scanlogin"]))    //账号已过期
                            {
                                MessageBox.Show("账号已过期！\n激活请联系管理员", "错误",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            if (table.Rows[0]["Spasswd"].ToString() == pb_passwd.Password)  //密码输入正确
                            {
                                //登录主界面
                                LoginInfo.LoginName = tb_username.Text;
                                LoginInfo.LoginType = "Student";

                                this.Visibility = System.Windows.Visibility.Hidden;

                                MainFrame mainFrame = new MainFrame("Student");
                                mainFrame.ShowDialog();
                                this.Close();
                            }
                            else
                                MessageBox.Show("密码错误！", "错误",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                            MessageBox.Show("用户名不存在！", "错误", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("发生未知错误！", "错误",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case "教师":                   
                    TeacherUserControl tuc = new TeacherUserControl(db);
                    table = tuc.GetDataByTeacherNum(tb_username.Text);
                    if (table != null)
                    {
                        if (table.Rows.Count != 0)   //记录数为0，表示没有该工号的教师
                        {
                            if (!((bool)table.Rows[0]["Tcanlogin"]))    //账号已过期
                            {
                                MessageBox.Show("账号已过期！\n激活请联系管理员", "错误",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            if (table.Rows[0]["Tpasswd"].ToString() == pb_passwd.Password)  //密码输入正确
                            {
                                //登录主界面
                                LoginInfo.LoginName = tb_username.Text;
                                LoginInfo.LoginType = "Teacher";

                                this.Visibility = System.Windows.Visibility.Hidden;

                                MainFrame mainFrame = new MainFrame("Teacher");
                                mainFrame.ShowDialog();
                                this.Close();
                            }
                            else
                                MessageBox.Show("密码错误！", "错误",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                            MessageBox.Show("用户名不存在！", "错误", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("发生未知错误！", "错误",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case "管理员":
                    SystemUserControl sysuc = new SystemUserControl(db);
                    table = sysuc.GetDataFromSystemUsername(tb_username.Text);
                    if (table != null)
                    {
                        if (table.Rows.Count != 0)   //记录数为0，表示没有该学号学生
                        {
                            if (!((bool)table.Rows[0]["SysCanlogin"]))    //账号已过期
                            {
                                MessageBox.Show("账号已过期！\n激活请联系管理员", "错误",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            if (table.Rows[0]["SysPassword"].ToString() == pb_passwd.Password)  //密码输入正确
                            {
                                //登录主界面
                                LoginInfo.LoginID = Convert.ToInt32(table.Rows[0]["SysUID"]);
                                LoginInfo.LoginName = tb_username.Text;
                                LoginInfo.LoginType = "Administrator";

                                this.Visibility = System.Windows.Visibility.Hidden;

                                MainFrame mainFrame = new MainFrame("Administrator");
                                mainFrame.ShowDialog();
                                this.Close();
                            }
                            else
                                MessageBox.Show("密码错误！", "错误",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                            MessageBox.Show("用户名不存在！", "错误", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("发生未知错误！", "错误",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                    break;                    
            }
             */
            #endregion

        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            //测试是否能够连接数据库
            if (!TestConnection())
            {
                MessageBox.Show("数据库不能连接！", "错误",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private bool TestConnection()
        {
            SQLServerDatabase ssdb = new SQLServerDatabase(ConnectionInfo.dbName, ConnectionInfo.serverName);
            bool reValue = true;

            try
            {
                ssdb.openConnection();  //打开数据库
            }
            catch
            {
                reValue = false;
            }
            finally
            {
                ssdb.closeConnection();
            }
            return reValue;
        }
    }

    public class LoginInfo
    {
        public static string LoginName;
        public static string LoginType;
        public static int LoginID;       
    }

    public class UserType
    {
        public static string Student = "Student";
        public static string Teacher = "Teacher";
        public static string Admin = "Administrator";
    }
}
