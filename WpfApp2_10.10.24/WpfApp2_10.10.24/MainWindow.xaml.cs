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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2_10._10._24
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

        private void Button_CE_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text=string.Empty;
        }

        private void Button_Split_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += "/";
        }
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 1;
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 2;
        }

        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 3;
        }

        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 4;
        }

        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 5;
        }

        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 6;
        }

        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 7;
        }

        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 8;
        }

        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 9;
        }
        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text += 0;
        }

        private void Button_ChangeOfSign_Click(object sender, RoutedEventArgs e)
        {
            int num;
            num = Int32.Parse(TextBox_Field.Text) * (-1);
            TextBox_Field.Text = num.ToString();
        }

        private void Button_point_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Plus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Subract_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Multiply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Squared_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Persent_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
