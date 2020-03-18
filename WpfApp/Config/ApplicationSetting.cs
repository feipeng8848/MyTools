using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp.ViewModel;

namespace WpfApp.Config
{
    public class ApplicationSetting : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static ApplicationSetting Settings = new ApplicationSetting();
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

        private int mainWindowWidth = 1200;
        /// <summary>
        /// main窗体宽度
        /// </summary>
        public int MainWindowWidth
        {
            get => mainWindowWidth;
            set => SetProperty(ref mainWindowWidth, value);
        }


        private bool maxmized;


        public bool Maxmized
        {
            get => maxmized;
            set => SetProperty(ref maxmized, value);
        }

        /// <summary>
        /// log4net仓库
        /// </summary>
        public string DefaultLogRepository
        {
            get => "WpfRepository";            
        }




    }
}
