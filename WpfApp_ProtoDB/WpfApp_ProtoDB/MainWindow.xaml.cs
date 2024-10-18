using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfApp_ProtoDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<User> users = new List<User>();
        public String[] positions = new String[] { "Директор", "Управляющий", "Рабочий", "Маркетолог" };
        public MainWindow()
        {
            InitializeComponent();
            users.Add(new User 
            { 
                LastName = "admin", 
                Password = "admin" 
            });
            panel_regist.Visibility=Visibility.Hidden;
            panel_login.Visibility=Visibility.Hidden;
            panel_regist.Margin = new Thickness(50, 7, 36, 15);
            panel_login.Margin = new Thickness(50, 7, 36, 15);
            textBox_home_num.IsReadOnly=true;
            textBox_mobile_num.IsReadOnly=true;
            foreach (string position in positions)
            {
                comboBox_position.Items.Add(position);
            }
        }
        //private void ShowPanel(Panel panelToShow)
        //{
        //    panel_regist.Visibility = panelToShow == panel_regist ? Visibility.Visible : Visibility.Hidden;
        //    panel_login.Visibility = panelToShow == panel_login ? Visibility.Visible : Visibility.Hidden;

        //    button_login.Visibility = panelToShow == panel_login ? Visibility.Hidden : Visibility.Visible;
        //    button_regist.Visibility = panelToShow == panel_regist ? Visibility.Hidden : Visibility.Visible;
        //    if (panelToShow == null) 
        //    { 
        //        button_regist.Visibility = Visibility.Visible;
        //        button_login.Visibility = Visibility.Visible;
        //    }
        //}

        private void button_regist_Click(object sender, RoutedEventArgs e)
        {
            //ShowPanel(panel_regist);
            panel_regist.Visibility = panel_regist.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            button_regist.Visibility = button_regist.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            button_login.Visibility = button_login.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

        }
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            //ShowPanel(null);
            panel_regist.Visibility = panel_regist.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            button_login.Visibility = button_login.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            button_regist.Visibility = button_regist.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            //ShowPanel(panel_login);
            panel_login.Visibility = panel_login.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            button_regist.Visibility = button_regist.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            button_login.Visibility = button_login.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }


        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            //ShowPanel(null);
            panel_login.Visibility = panel_login.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            button_regist.Visibility = button_regist.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            button_login.Visibility = button_login.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Button_sent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_name.Text) || string.IsNullOrWhiteSpace(textBox_surname.Text) ||
                string.IsNullOrWhiteSpace(comboBox_position.Text) || string.IsNullOrWhiteSpace(textBox_password.Text) ||
                (checkBox_home_num.IsChecked == true && string.IsNullOrWhiteSpace(textBox_home_num.Text)) ||
                (checkBox_mobile_num.IsChecked == true && string.IsNullOrWhiteSpace(textBox_mobile_num.Text)) ||
                (!radioButton_man.IsChecked == true && !radioButton_woman.IsChecked == true))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            if (users.Any(u => u.LastName == textBox_surname.Text && u.FirstName == textBox_name.Text))
            {
                MessageBox.Show("Такой пользователь уже существует.");
                return;
            }

            User newUser = new User
            {
                FirstName = textBox_name.Text,
                LastName = textBox_surname.Text,
                Gender = radioButton_man.IsChecked == true ? "Мужской" : "Женский",
                Position = comboBox_position.Text,
                HomePhone = checkBox_home_num.IsChecked == true ? textBox_home_num.Text : null,
                MobilePhone = checkBox_mobile_num.IsChecked == true ? textBox_mobile_num.Text : null,
                Password = textBox_password.Text
            };

            users.Add(newUser);
            MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearRegistrationFields();

        }
        private void ClearRegistrationFields()
        {
            textBox_name.Clear();
            textBox_surname.Clear();
            comboBox_position.SelectedIndex = -1;
            textBox_password.Clear();
            checkBox_home_num.IsChecked = false;
            textBox_home_num.Clear();
            checkBox_mobile_num.IsChecked = false;
            textBox_mobile_num.Clear();
            radioButton_man.IsChecked = false;
            radioButton_woman.IsChecked = false;
        }
        private void Button_enter_Click(object sender, RoutedEventArgs e)
        {
            string lastName = comboBoxSurname.Text;
            string password = textBoxPassword.Text;

            // Поиск пользователя по фамилии и паролю
            User user = users.Find(u => u.LastName == lastName && u.Password == password);
            if (user != null)
            {
                if (user.LastName == "admin" && user.Password == "admin")
                {
                    MessageBoxResult result = MessageBox.Show("Хотите ли вы войти как админ?", "Подтверждение", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        //EditUserWindow editform = new EditUserWindow(this);
                        //editform.Owner = this;
                        //editform.ShowDialog(this);
                    }
                    else
                    {
                        MessageBox.Show(user.ShowInfo());
                    }
                }
                else
                {
                    MessageBox.Show(user.ShowInfo());
                }
            }
            else
            {
                MessageBox.Show("Ошибка входа: неверная фамилия или пароль.");
            }

            ClearLoudFields();
        }
        private void ClearLoudFields()
        {
            comboBoxSurname.SelectedIndex = -1;
            textBoxPassword.Text = string.Empty;
        }

        public void ValidatePhoneNumber(TextBox textBox)
        {
            string num = textBox.Text.Trim();

            if (!string.IsNullOrEmpty(num) && !Int32.TryParse(num, out _))
            {
                MessageBox.Show("Ошибка ввода: номер должен состоять из цифр.");
                textBox.Text = string.Empty; // Очищаем поле только после предупреждения
            }
        }

        public void ValidateTextBox(TextBox textBox)
        {
            string input = textBox.Text.Trim();

            // Проверяем, состоит ли ввод только из букв
            if (!string.IsNullOrEmpty(input) && !input.All(char.IsLetter))
            {
                MessageBox.Show("Ошибка ввода: поле должно содержать только буквы.");
                textBox.Text = string.Empty; // Очищаем поле только после предупреждения
            }
        }

        public void ValidateInput(TextBox textBox, CheckBox checkBox, Label indicatorLabel)
        {
            string input = textBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input) || !checkBox.IsChecked.GetValueOrDefault())
            {
                indicatorLabel.Content = "✗"; // Крестик для пустого поля
                indicatorLabel.Foreground = Brushes.Red;
            }
            else
            {
                indicatorLabel.Content = "✓"; // Галочка для корректного ввода
                indicatorLabel.Foreground = Brushes.Green;
            }
        }

        public void ValidateInput(TextBox textBox, Label indicatorLabel)
        {
            string input = textBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                indicatorLabel.Content = "✗"; // Крестик для пустого поля
                indicatorLabel.Foreground = Brushes.Red;
            }
            else
            {
                indicatorLabel.Content = "✓"; // Галочка для корректного ввода
                indicatorLabel.Foreground = Brushes.Green;
            }
        }

        public void ValidateInput(ComboBox comboBox, Label indicatorLabel)
        {
            if (comboBox.SelectedItem == null)
            {
                indicatorLabel.Content = "✗"; // Крестик для пустого выбора
                indicatorLabel.Foreground = Brushes.Red;
            }
            else
            {
                indicatorLabel.Content = "✓"; // Галочка для корректного выбора
                indicatorLabel.Foreground = Brushes.Green;
            }
        }
        private void CheckBox_home_num_CheckedChanged(object sender, RoutedEventArgs e)
        {
            textBox_home_num.IsReadOnly = !checkBox_home_num.IsChecked.GetValueOrDefault();
        }
        private void CheckBox_mobile_num_CheckedChanged(object sender, RoutedEventArgs e)
        {
            textBox_mobile_num.IsReadOnly = !checkBox_mobile_num.IsChecked.GetValueOrDefault();
        }

        private void TextBox_surname_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(textBox_name);
            ValidateInput(textBox_name, label_name_check);
        }

        private void TextBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_home_num_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_mobile_num_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
    public class User : INotifyPropertyChanged
    {
        private string lastName;
        private string firstName;
        private string position;
        private string homePhone;
        private string mobilePhone;
        private string gender;
        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        public string Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public string HomePhone
        {
            get => homePhone;
            set => SetProperty(ref homePhone, value);
        }

        public string MobilePhone
        {
            get => mobilePhone;
            set => SetProperty(ref mobilePhone, value);
        }

        public string Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string ShowInfo()
        {
            string text = "Вход выполнен успешно!" +
                          "\n Фамилия: " + this.LastName +
                          "\n Имя: " + this.FirstName +
                          "\n Должность: " + this.Position;
            if (!string.IsNullOrEmpty(this.HomePhone))
            {
                text += "\n Домашний номер: " + this.HomePhone;
            }
            if (!string.IsNullOrEmpty(this.MobilePhone))
            {
                text += "\n Мобильный номер: " + this.MobilePhone;
            }
            text += "\n Пол: " + this.Gender;
            return text;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }

}
