using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp.ViewModel
{
    class MainVM : ViewModelBase
    {
        private string _connStr;

        public string connStr
        {
            get { return _connStr; }
            set => SetProperty(ref _connStr, value);
        }


        private CornerRadius windowCorner = new CornerRadius(3);

        public CornerRadius WindowCorner
        {
            get { return windowCorner; }
            set => SetProperty(ref windowCorner, value);
        }

        private CornerRadius titleCorner = new CornerRadius(3.5,3.5,0,0);

        public CornerRadius TiterCorner
        {
            get { return titleCorner; }
            set => SetProperty(ref titleCorner, value);
        }


        private string name;

        public string Name
        {
            get { return name; }
            set => SetProperty(ref name, value);
        }

        private List<Person> people;

        public List<Person> People
        {
            get { return people; }
            set => SetProperty(ref people, value);
        }

        public ICommand BtnClick
        {
            get
            {
                return new RelayCommand
                {
                    CanExecutePredicate = a => true,
                    ExecuteAction = a =>
                    {
                        MessageBox.Show("hello btn click");
                        LogError.Info("按钮按下log");
                        LogInfo.Info("sdfsad");
                    }
                };
            }
        }


    }
}
