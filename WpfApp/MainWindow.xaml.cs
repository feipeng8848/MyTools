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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        MainVM MVM = null;

        #region 导航栏
        private void NavBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Max_Click(object sender, RoutedEventArgs e)
        {
            if (Top == 0 & Left == 0)
            {
                Width = MinWidth;
                Height = MinHeight;
                Top = (SystemParameters.WorkArea.Height - MinHeight) / 2;
                Left = (SystemParameters.WorkArea.Width - MinWidth) / 2;
            }
            else
            {
                Top = 0;
                Left = 0;
                WindowState = WindowState.Normal;
                Height = SystemParameters.WorkArea.Height;
                Width = SystemParameters.WorkArea.Width;
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
                people.Add(new Person() {
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
