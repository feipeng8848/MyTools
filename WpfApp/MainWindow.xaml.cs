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
using WpfApp.Config;
using log4net;
using log4net.Repository;
using System.IO;
using log4net.Config;
using System.Globalization;

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

            InitLog4Net();
            LogInfo.Info("MainWindow 启动");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        MainVM MVM = null;


        #region 标题栏
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
            //最大化的时候也能响应
            if (this.WindowState == WindowState.Maximized && e.LeftButton == MouseButtonState.Pressed)
            {
                Grid obj = (Grid)sender;
                System.Windows.Point position = Mouse.GetPosition(obj);
                System.Windows.Point point = obj.PointToScreen(new System.Windows.Point(0.0, 0.0));
                CompositionTarget compositionTarget = PresentationSource.FromVisual(obj)?.CompositionTarget;
                if (compositionTarget != null)
                {
                    point = compositionTarget.TransformFromDevice.Transform(point);
                }
                base.Top = point.Y;
                double num = 0.0;
                if (!double.TryParse(ApplicationSetting.Settings.MainWindowWidth.ToString(CultureInfo.InvariantCulture), out double result))
                {
                    result = 1000.0;
                }
                if (position.X > result / 2.0)
                {
                    num = position.X - result / 2.0;
                }
                base.Left = point.X + num;
                base.WindowState = WindowState.Normal;
                ApplicationSetting.Settings.Maxmized = false;
                DragMove();
                e.Handled = true;
            }
        }

        private void StopDragMove(object sender, MouseButtonEventArgs e)
        {
            //canDragToNormal = false;
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
            base.WindowState = ((base.WindowState != WindowState.Maximized) ? WindowState.Maximized : WindowState.Normal);
            ApplicationSetting.Settings.Maxmized = (base.WindowState == WindowState.Maximized);
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
            MVM.CloseAllCammerCommand.Execute(null);
            Close();
            Application.Current.Shutdown();
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





        private void BtnTestIDReader_Click(object sender, RoutedEventArgs e)
        {
            TestIdCardReader window = new TestIdCardReader();
            window.Show();
        }

       
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string Age { get; set; }
        public string LastName { get; set; }
    }
}
