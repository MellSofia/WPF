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

        

        private void Button_Split_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Field.Text.Length > 0 && char.IsDigit(TextBox_Field.Text[TextBox_Field.Text.Length - 1]))
            {
                TextBox_Field.Text += "/";
            }
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
            // Проверяем, что поле не пустое
            if (!string.IsNullOrWhiteSpace(TextBox_Field.Text))
            {
                try
                {
                    // Пробуем преобразовать текст в число
                    double num = double.Parse(TextBox_Field.Text, System.Globalization.CultureInfo.InvariantCulture);
                    // Меняем знак числа
                    num = -num;
                    // Отображаем результат в TextBox
                    TextBox_Field.Text = num.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    // Обработка ошибки, если ввод некорректен
                    MessageBox.Show("Введите корректное число.");
                }
                catch (OverflowException)
                {
                    // Обработка ошибки переполнения
                    MessageBox.Show("Число слишком велико или слишком мало.");
                }
            }
        }

        private void Button_point_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Field.Text.Length > 0 && char.IsDigit(TextBox_Field.Text[TextBox_Field.Text.Length - 1]))
            {
                if (!TextBox_Field.Text.Contains("."))
                {
                    TextBox_Field.Text += ".";
                }
            }
        }

        private void Button_Plus_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Field.Text.Length > 0 && char.IsDigit(TextBox_Field.Text[TextBox_Field.Text.Length - 1]))
            {
                TextBox_Field.Text += "+";
            }
        }

        private void Button_Subract_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Field.Text.Length > 0 && char.IsDigit(TextBox_Field.Text[TextBox_Field.Text.Length - 1]))
            {
                TextBox_Field.Text += "-";
            }
        }

        private void Button_Multiply_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Field.Text.Length > 0 && char.IsDigit(TextBox_Field.Text[TextBox_Field.Text.Length - 1]))
            {
                TextBox_Field.Text += "*";
            }
        }

        private void Button_Delite_Click(object sender, RoutedEventArgs e)
        {
            string exam = "";

            if (TextBox_Field.Text.Length > 0)
            {
                exam = TextBox_Field.Text.Substring(0, TextBox_Field.Text.Length - 1);
                TextBox_Field.Text = exam;
            }
        }
        private void Button_CE_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Field.Text.Length > 0)
            {
                int lastIndex = TextBox_Field.Text.Length - 1;

                while (lastIndex >= 0 && char.IsDigit(TextBox_Field.Text[lastIndex]))
                {
                    lastIndex--;
                }

                TextBox_Field.Text = TextBox_Field.Text.Substring(0, lastIndex + 1);
            }
        }
        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Field.Text = string.Empty;
        }



        private void Button_result_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TextBox_Field.Text))
                {
                    // Заменяем запятую на точку
                    string expression = TextBox_Field.Text.Replace(',', '.').Trim();
                    Console.WriteLine("Вычисляемое выражение: " + expression);

                    var dataTable = new System.Data.DataTable();
                    var result = dataTable.Compute(expression, string.Empty);

                    // Преобразуем результат в строку
                    string resultString = result.ToString();

                    // Убираем точку и ноль, если они стоят в конце
                    if (resultString.EndsWith(".0"))
                    {
                        resultString = resultString.Substring(0, resultString.Length - 2);
                    }

                    TextBox_Field.Text = resultString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в выражении: " + ex.Message);
                Button_CE_Click(sender, e);
            }
        }
    }
}
