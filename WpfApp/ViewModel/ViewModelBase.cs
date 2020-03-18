using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net;
using WpfApp.Config;
using log4net.Config;
using log4net.Repository;
using System.Windows.Input;
using System.IO;

namespace WpfApp.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

        public ViewModelBase()
        {
            InitLog4Net();
            LogInfo.Info("开始记录log");
        }




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
            LogDebug = LogManager.GetLogger(repository.Name, "logDebug");
            LogInfo = LogManager.GetLogger(repository.Name, "loginfo");
            LogError = LogManager.GetLogger(repository.Name, "logError");
        }

        #endregion
    }

    /// <summary>
    /// 路由命令
    /// </summary>
    internal class RelayCommand : ICommand
    {
        public Predicate<object> CanExecutePredicate { get; set; }
        public Action<object> ExecuteAction { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="canExecute"></param>
        /// <param name="execute"></param>
        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            CanExecutePredicate = canExecute;
            ExecuteAction = execute;
        }
        /// <summary>
        /// ICommand字段
        /// </summary>
        public RelayCommand()
        { }
        /// <summary>
        /// ICommand字段
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        /// <summary>
        /// ICommad字段
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return CanExecutePredicate == null || CanExecutePredicate(parameter);
        }
        /// <summary>
        /// ICommand字段
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }
    }
}
