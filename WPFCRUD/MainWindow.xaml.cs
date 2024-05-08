using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Person Person;
        PersonContext db;
        ObservableCollection<Person> persons;

        public MainWindow()
        {
            InitializeComponent();
            db = new PersonContext();
            persons = new ObservableCollection<Person>(db.Persons);

            spInput.DataContext = persons;
            lstboxPerson.ItemsSource = persons;
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private async void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Person p= (Person)lstboxPerson.SelectedItem;
            db.Remove(p);
            await db.SaveChangesAsync();
            RefreshPerson();

        }

        private void RefreshPerson()
        {
            persons.Clear();
            foreach (var item in db.Persons) persons.Add(item);
        }

        private async void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            RefreshPerson();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Windowadd windowadd = new Windowadd();
            windowadd.Owner = this;
            windowadd.Show();
            RefreshPerson();
        }
    }
}
