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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_StudetsDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StudentName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StudentsSureName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StudentFatherName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(sender is MenuItem) 
            {
            MenuItem menuItem = (MenuItem)sender;
                if(menuItem.Header.ToString() == "Создать аккаунт студента") 
                {
                    Tab_AddStudent.Visibility= Visibility.Visible;
                }
            }

            
        }
    }
}
