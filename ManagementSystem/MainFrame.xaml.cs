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

using ResourcesLib.UI;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ManagementSystem
{
    /// <summary>
    /// MainFrame.xaml 的交互逻辑
    /// </summary>
    public partial class MainFrame : Window
    { 
      
        private ListBox ModulDetailList = new ListBox();
        private ArrayList btnList = new ArrayList();
        private string UserType = null;
        private ModulDb mdb = new ModulDb();

        public MainFrame(string userType = "Student")
        {
            InitializeComponent();

            UserType = userType;            

            //初始化程序框架
            InitFrame();
        }

        public void InitFrame()
        {
            int n;       
            ObservableCollection<Modul> moduls = new ObservableCollection<Modul>();
            Button button;

            moduls = mdb.GetModulsFromUserType(UserType)
                        as ObservableCollection<Modul>; 
            n = 0;
            foreach (Modul modul in moduls)
            {
                n++;

                button = new Button();
                button.Name = "modul" + modul.ModulNum.ToString();
                button.Content = modul.ModulName;
                button.Click += new RoutedEventHandler(ModulSelect);        //按钮事件
                if (n == 1)
                    DockPanel.SetDock(button, System.Windows.Controls.Dock.Top);
                else
                    DockPanel.SetDock(button, System.Windows.Controls.Dock.Bottom);
                btnList.Add(button);
                ModulTab.Children.Add(button);
            }
            ModulDetailList.Name = "ModulDetails";            
            ModulDetailList.Style = this.FindResource("ListBoxStyle") as Style;            
            ModulDetailList.SelectionChanged += new SelectionChangedEventHandler(ModulDetailSelect);
            ModulTab.Children.Add(ModulDetailList);

            //选择第一个模块
            ModulSelect(btnList[0], null);

        }


        //选择不同模块
        private void ModulSelect(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string str = btn.Name;
            int ModulNum = Convert.ToInt32(str.Replace("modul", ""));       //获得模块的编号

            foreach (Button button in btnList)
            {
                if (btn == button)
                    DockPanel.SetDock(button, System.Windows.Controls.Dock.Top);
                else
                    DockPanel.SetDock(button, System.Windows.Controls.Dock.Bottom);
            }

            ObservableCollection<ModulDetail> modulDetails = new ObservableCollection<ModulDetail>();

            modulDetails = mdb.GetModulDetailsFromUserType(UserType,ModulNum) as
                ObservableCollection<ModulDetail>;
            if (modulDetails.Count != 0)        //有子模块
            {
                ModulDetailList.ItemsSource = modulDetails;
            }            
        }

        //选择不同的子模块
        private void ModulDetailSelect(object sender, SelectionChangedEventArgs args)
        {
            ModulDetail modulDetail = (sender as ListBox).SelectedItem as ModulDetail;

            if (modulDetail != null)
            {
                Type type = this.GetType();
                Assembly assembly = type.Assembly;
                UserControl win = (UserControl)assembly.CreateInstance(
                    type.Namespace + "." + modulDetail.UriPath.Replace(".xaml",""));

                ModulDetailWindow.Children.Clear();
                if (win != null)
                {
                    win.Width = ModulDetailWindow.Width;
                    win.Height = ModulDetailWindow.Height;
                    ModulDetailWindow.Children.Add(win);
                }
                else
                    MessageBox.Show("找不到子模块");     
            }
        }

        //系统信息
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }    
}
