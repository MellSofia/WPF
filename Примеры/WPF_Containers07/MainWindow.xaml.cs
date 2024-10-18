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

namespace WPF_Containers07
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Human> students = new List<Human>
            {
                new Human{Name = "Иван", Age = 25},
                new Human{Name = "Олеся", Age = 18},
                new Human{Name = "Олег", Age = 20}
            };
            lBox_Two.ItemsSource = students;
            lView.ItemsSource = students;
        }

        private void lBox_One_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(lBox_One.SelectedItem.ToString());
        }

        private void lBox_Two_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(lBox_Two.SelectedItem.ToString());
        }
    }

    public class Human
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public override string ToString()
        {
            return $"Имя: {Name} Возраст: {Age}";
        }
    }
}
