using Microsoft.SqlServer.Server;
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
using System.Windows.Shapes;

namespace WpfAppProtoBD
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindow form1;
        private List<User> users_ = new List<User>();
        public Window1(MainWindow _form1_)
        {
            InitializeComponent();
            form1 = _form1_;
            users_ = _form1_.users;

            // Добавляем пользователей в comboBox, исключая администратора
            foreach (var item in users_)
            {
                if (item.LastName != "admin") // Исключаем администратора
                {
                    comboBoxUsers.Items.Add(item.LastName);
                }
            }

            foreach (string position in _form1_.positions)
            {
                comboBox_position.Items.Add(position);

            }
        }

        private void ClearcomboBoxUsersFields()
        {
            textBox_name.Text = string.Empty;
            textBox_password.Text = string.Empty;
            comboBox_position.SelectedItem = null;
            textBox_homer_num.Text = string.Empty;
            textBox_mobile_numr.Text = string.Empty;
            checkBox_homer_num.IsChecked = false;
            checkBox_mobile_numr.IsChecked = false;
            radioButton_man.IsChecked = false;
            radioButton_woman.IsChecked = false;

            // Очищаем метки ошибок
            label_name_check.Content = "";
            label_surename_check.Content = "";
            label_position_check.Content = "";
            label_password_check.Content = "";
            label_homer_num_check.Content = "";
            label_mobile_numr_check.Content = "";
            label_gender_check.Content = "";
        }
        private void Button_save_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputValid())
            {
                UpdateUserDetails();
                MessageBox.Show("Изменения прошли успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                comboBoxUsers.SelectedItem = null;
                ClearcomboBoxUsersFields();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
            }
        }

        private bool IsInputValid()
        {
            return !string.IsNullOrWhiteSpace(textBox_name.Text) &&
                   !string.IsNullOrWhiteSpace(comboBox_position.Text) &&
                   !string.IsNullOrWhiteSpace(textBox_password.Text) &&
                   (checkBox_homer_num.IsChecked == false || !string.IsNullOrWhiteSpace(textBox_homer_num.Text)) &&
                   (checkBox_mobile_numr.IsChecked == false || !string.IsNullOrWhiteSpace(textBox_mobile_numr.Text)) &&
                   ((radioButton_man.IsChecked == true || radioButton_woman.IsChecked == true));
        }

        private void UpdateUserDetails()
        {
            var selectedUser = users_.Find(u => u.LastName == comboBoxUsers.Text);
            if (selectedUser != null)
            {
                selectedUser.FirstName = textBox_name.Text;
                selectedUser.Gender = radioButton_man.IsChecked == true ? "Мужской" : "Женский";
                selectedUser.Position = comboBox_position.SelectedItem.ToString();
                selectedUser.HomePhone = checkBox_homer_num.IsChecked == true ? textBox_homer_num.Text : null;
                selectedUser.MobilePhone = checkBox_mobile_numr.IsChecked == true ? textBox_mobile_numr.Text : null;
                selectedUser.Password = textBox_password.Text;
            }
        }

        private void comboBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearcomboBoxUsersFields(); // Очищаем поля перед отображением новой информации

            string lastName = (string)((ComboBox)sender).SelectedItem; // Получаем выбранное значение

            User user = users_.Find(u => u.LastName == lastName);
            if (user != null) // Проверяем, найден ли пользователь
            {
                textBox_name.Text = user.FirstName;
                comboBox_position.Text = user.Position;
                checkBox_homer_num.IsChecked = !string.IsNullOrEmpty(user.HomePhone);
                checkBox_mobile_numr.IsChecked = !string.IsNullOrEmpty(user.MobilePhone);
                textBox_homer_num.Text = user.HomePhone;
                textBox_mobile_numr.Text = user.MobilePhone;
                radioButton_man.IsChecked = user.Gender == "Мужской";
                radioButton_woman.IsChecked = user.Gender == "Женский";
                textBox_password.Text = user.Password;

                textBox_mobile_numr.IsEnabled = checkBox_mobile_numr.IsChecked == true;
                textBox_homer_num.IsEnabled = checkBox_homer_num.IsChecked == true;

                form1.ValidateInput(comboBoxUsers, label_surename_check);
            }
        }


        private void TextBox_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            form1.ValidateTextBox(textBox_name);
            form1.ValidateInput(textBox_name, label_name_check);
        }

        private void CheckBox_homer_num_Checked(object sender, RoutedEventArgs e)
        {
            textBox_homer_num.IsEnabled = true; // Включаем текстовое поле
        }

        private void CheckBox_homer_num_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox_homer_num.IsEnabled = false; // Отключаем текстовое поле
            textBox_homer_num.Text = string.Empty; // Очищаем текстовое поле при отключении
        }

        private void CheckBox_mobile_numr_Checked(object sender, RoutedEventArgs e)
        {
            textBox_mobile_numr.IsEnabled = true; // Включаем текстовое поле
        }

        private void CheckBox_mobile_numr_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox_mobile_numr.IsEnabled = false; // Отключаем текстовое поле
            textBox_mobile_numr.Text = string.Empty; // Очищаем текстовое поле при отключении
        }

        private void TextBox_mobile_numr_TextChanged(object sender, TextChangedEventArgs e)
        {
            form1.ValidatePhoneNumber(textBox_mobile_numr);
            form1.ValidateInput(textBox_mobile_numr, checkBox_mobile_numr, label_mobile_numr_check);
        }

        private void TextBox_homer_num_TextChanged(object sender, TextChangedEventArgs e)
        {
            form1.ValidatePhoneNumber(textBox_homer_num);
            form1.ValidateInput(textBox_homer_num, checkBox_homer_num, label_homer_num_check);
        }

        private void TextBox_password_TextChanged(object sender, TextChangedEventArgs e)
        {
            form1.ValidateInput(textBox_password, label_password_check);
        }


        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void comboBox_position_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            form1.ValidateInput(comboBox_position, label_position_check);
        }
    }
}