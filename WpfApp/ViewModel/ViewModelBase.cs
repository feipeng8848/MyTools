using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged
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
