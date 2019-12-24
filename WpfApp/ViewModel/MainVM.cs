using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.ViewModel
{
    class MainVM:ViewModelBase
    {
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


    }
}
