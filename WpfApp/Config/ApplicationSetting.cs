using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Config
{
    public class ApplicationSetting : INotifyPropertyChanged
    {
        public ApplicationSetting()
        {
            getCfg();
        }
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





        /// <summary>
        /// 读取配置文件
        /// </summary>
        public void getCfg()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;                
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }
        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public void writeCfg(string item, string value)
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
        }



        
    }
}
