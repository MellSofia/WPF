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

namespace WPF_Controls06
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

        private void chBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Чекбокс активирован.");
        }

        private void chBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Чекбокс деактивирован.");
        }

        private void chBox_Indeterminate(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Чекбокс в промежуточном состоянии.");
        }

        private void RadioButton_MouseEnter(object sender, MouseEventArgs e)
        {
            popup.IsOpen = true;
        }
    }
}
