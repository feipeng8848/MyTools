using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp.ViewModel;
using System.Configuration;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using log4net;
using log4net.Repository;
using System.IO;
using log4net.Config;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MVM = new MainVM(); ;
            MVM.Name = "GitHub";
            DataContext = MVM;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModelBase.LogInfo.Info("载入窗体");
        }

        /// <summary>
        /// 读取配置文件.引用：在使用中
        /// 如果出现“ 当前上下文中不存在名称：ConfigurationManager ”，右键引用，添加，搜索 System.Configuration
        /// </summary>
        void getCfg()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                MVM.connStr = appSettings["connStr"];
            }
            catch (ConfigurationErrorsException)
            {
                ViewModelBase.LogInfo.Info("Error reading app settings");
            }
        }
        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        void writeCfg(string item, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[item].Value = value;
                config.AppSettings.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error write app settings");
            }
            getCfg();
        }

        MainVM MVM = null;

        #region 窗体大小
        private const int WM_NCHITTEST = 0x0084;
        private readonly int agWidth = 12; //拐角宽度  
        private readonly int bThickness = 4; // 边框宽度   
        private Point mousePoint = new Point(); //鼠标坐标
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(this.WndProc));
            }
        }
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_NCHITTEST:
                    this.mousePoint.X = (lParam.ToInt32() & 0xFFFF);
                    this.mousePoint.Y = (lParam.ToInt32() >> 16);

                    // 窗口左上角  
                    if (this.mousePoint.Y - this.Top <= this.agWidth
                        && this.mousePoint.X - this.Left <= this.agWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOPLEFT);
                    }
                    // 窗口左下角      
                    else if (this.ActualHeight + this.Top - this.mousePoint.Y <= this.agWidth
                        && this.mousePoint.X - this.Left <= this.agWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOMLEFT);
                    }
                    // 窗口右上角  
                    else if (this.mousePoint.Y - this.Top <= this.agWidth
                        && this.ActualWidth + this.Left - this.mousePoint.X <= this.agWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOPRIGHT);
                    }
                    // 窗口右下角  
                    else if (this.ActualWidth + this.Left - this.mousePoint.X <= this.agWidth
                        && this.ActualHeight + this.Top - this.mousePoint.Y <= this.agWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOMRIGHT);
                    }
                    // 窗口左侧  
                    else if (this.mousePoint.X - this.Left <= this.bThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTLEFT);
                    }
                    // 窗口右侧  
                    else if (this.ActualWidth + this.Left - this.mousePoint.X <= this.bThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTRIGHT);
                    }
                    // 窗口上方  
                    else if (this.mousePoint.Y - this.Top <= this.bThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOP);
                    }
                    // 窗口下方  
                    else if (this.ActualHeight + this.Top - this.mousePoint.Y <= this.bThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOM);
                    }
                    else
                    {
                        return IntPtr.Zero;
                    }
            }
            return IntPtr.Zero;
        }

        public enum HitTest : int
        {
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTSIZE = HTGROWBOX,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTREDUCE = HTMINBUTTON,
            HTZOOM = HTMAXBUTTON,
            HTSIZEFIRST = HTLEFT,
            HTSIZELAST = HTBOTTOMRIGHT,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21,
        }
        #endregion


        #region 导航栏
        private void NavBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        Rect rcnormal;
        private void Max_Click(object sender, RoutedEventArgs e)
        {
            if (Top == 0 && Left == 0)
            {
                Width = rcnormal.Width;
                Height = rcnormal.Height;
                Top = rcnormal.Top;
                Left = rcnormal.Left;
                WindowBorder.Margin = new Thickness(10);
                WindowBorder.CornerRadius = MVM.WindowCorner;
                TitleBorder.CornerRadius = MVM.TiterCorner;
            }
            else if (Top == 0 && Left == SystemParameters.WorkArea.Width)
            {
                Width = rcnormal.Width;
                Height = rcnormal.Height;
                Top = rcnormal.Top;
                Left = rcnormal.Left;
                WindowBorder.Margin = new Thickness(10);
                WindowBorder.CornerRadius = MVM.WindowCorner;
                TitleBorder.CornerRadius = MVM.TiterCorner;
            }
            else
            {
                rcnormal = new Rect(Left, Top, Width, Height);
                if (Left < SystemParameters.WorkArea.Width)
                {
                    Top = 0;
                    Left = 0;
                }
                else if (Left >= SystemParameters.WorkArea.Width)
                {
                    Top = 0;
                    Left = SystemParameters.WorkArea.Width;
                }
                WindowState = WindowState.Normal;
                Height = SystemParameters.WorkArea.Height;
                Width = SystemParameters.WorkArea.Width;
                WindowBorder.Margin = new Thickness(0);
                WindowBorder.CornerRadius = new CornerRadius(0);
                TitleBorder.CornerRadius = new CornerRadius(0);
            }
        }

        private void Min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion


        private void BtnTestIDReader_Click(object sender, RoutedEventArgs e)
        {
            TestIdCardReader window = new TestIdCardReader();
            window.Show();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();
            for (int i = 0; i < 5; i++)
            {
                people.Add(new Person()
                {
                    FirstName = "FirstName_" + i,
                    LastName = "LastName_" + i,
                    Age = (i + 10).ToString()
                });
            }
            MVM.People = people;
            datagrid.DataContext = MVM.People;

            List<string> items = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                items.Add("Hi I am a item , " + i);
            }
            combo.DataContext = items;
        }
        
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string Age { get; set; }
        public string LastName { get; set; }
    }
}
