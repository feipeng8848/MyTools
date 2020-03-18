using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Config;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.ViewModel;
using log4net;
using log4net.Repository;
using log4net.Config;
using System.IO;

namespace WpfApp.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window10 : Window
    {
        MainVM MVM = null;
        public Window10()
        {
            InitializeComponent();
            MVM = new MainVM(); ;
            MVM.Name = "GitHub";
            DataContext = MVM;
            InitLog4Net();
        }

        #region 标题栏
        private bool canDragToNormal = true;
        /// <summary>
        /// 鼠标移动标题栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
                e.Handled = true;
            }
        }

        private void StopDragMove(object sender, MouseButtonEventArgs e)
        {
            canDragToNormal = false;
        }



        private void HeaderGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TryCloseSearch(onlyHideInpute: true);
            //if (Mouse.GetPosition((Grid)sender).X > ProjectMenu.ActualWidth)
            //{
            //    HideProjectMenu();
            //}
            //if ((DateTime.Now - _headerClickOldTime).TotalMilliseconds <= 300.0)
            //{
            //    ToggleWindowState();
            //}
            //else
            //{
            //    if (base.WindowState == WindowState.Maximized)
            //    {
            //        canDragToNormal = true;
            //    }
            //    if (e.LeftButton == MouseButtonState.Pressed)
            //    {
            //        DragMove();
            //        e.Handled = false;
            //    }
            //}
            //_headerClickOldTime = DateTime.Now;
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            LogInfo.Info("MinButtonClick is click ");
            base.WindowState = WindowState.Minimized;
        }

        private void MaxButtonClick(object sender, RoutedEventArgs e)
        {
            LogInfo.Info("MaxButtonClick is click "); 
            ToggleWindowState();
        }

        /// <summary>
        /// 切换window状态，不管是哪个按钮按下，都是调用这个方法
        /// </summary>
		private void ToggleWindowState()
        {
            LogInfo.Info("ToggleWindowState is exe ");
            //base.WindowState = ((base.WindowState != WindowState.Maximized) ? WindowState.Maximized : WindowState.Normal);
            //LocalSettings.Settings.Maxmized = (base.WindowState == WindowState.Maximized);
            //SafeSetWindowSize();
            //ProjectMenu.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void NormalButtonClick(object sender, RoutedEventArgs e)
        {
            LogInfo.Info("NormalButtonClick is click ");
            ToggleWindowState();
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnClickButtonMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (base.Width < 800.0)
            {
                CloseCover.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(26, 0, 0, 0));
            }
            X.Visibility = Visibility.Collapsed;
            XX.Visibility = Visibility.Visible;
        }

        private void OnClickButtonMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CloseCover.Background = System.Windows.Media.Brushes.Transparent;
            X.Visibility = Visibility.Visible;
            XX.Visibility = Visibility.Collapsed;
        }
        #endregion



        #region Log4Net
        public static ILog LogDebug = null;
        public static ILog LogInfo = null;
        public static ILog LogError = null;

        /*使用方法
          LogDebug.Debug("logDebug");
          LogInfo.Info("loginfo");
          LogError.Error("error");
        */

        static void InitLog4Net()
        {
            ILoggerRepository repository = null;//LogManager.CreateRepository("NETRepository");
            try
            {
                repository = LogManager.GetRepository(ApplicationSetting.Settings.DefaultLogRepository);
            }
            catch (Exception)
            {
                repository = LogManager.CreateRepository(ApplicationSetting.Settings.DefaultLogRepository);
            }
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            //LogInfo = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            LogDebug = LogManager.GetLogger(repository.Name, "logDebug");
            LogInfo = LogManager.GetLogger(repository.Name, "loginfo");
            LogError = LogManager.GetLogger(repository.Name, "logError");
        }

        #endregion



        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Max_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized == this.WindowState ? WindowState.Normal : WindowState.Maximized;
        }

        private void Btn_Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
