using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModel
{
    class MainVM:ViewModelBase
    {
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
