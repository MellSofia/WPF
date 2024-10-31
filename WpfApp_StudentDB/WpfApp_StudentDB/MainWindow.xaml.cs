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

namespace WpfApp_StudentDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        private ObservableCollection<Teacher> teachers = new ObservableCollection<Teacher>();
        private List<Group> groups = new List<Group>();
        private List<Group> subjects_ = new List<Group>();


        public MainWindow()
        {
            InitializeComponent();
            StudentsDataGrid.ItemsSource = students;
            LoadDataGrids();
            AddStudent.Visibility = Visibility.Hidden;
            AddTeacher.Visibility = Visibility.Hidden;
            AddGroup.Visibility = Visibility.Hidden; 
            Me_nu.Visibility = Visibility.Visible;
            Tab_Item.Visibility = Visibility.Visible;
        }

        private void LoadDataGrids()
        {
            StudentsDataGrid.ItemsSource = students;
            TeachersDataGrid.ItemsSource = teachers;
            GroupsDataGrid.ItemsSource = groups;
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            AddStudent.Visibility = Visibility.Visible;
            Me_nu.Visibility = Visibility.Hidden;
            Tab_Item.Visibility = Visibility.Hidden;
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            AddTeacher.Visibility = Visibility.Visible;
            Me_nu.Visibility = Visibility.Hidden;
            Tab_Item.Visibility = Visibility.Hidden;
        }

        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            AddGroup.Visibility = Visibility.Visible;
            Me_nu.Visibility = Visibility.Hidden;
            Tab_Item.Visibility = Visibility.Hidden;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            List<string> results = new List<string>();

            // Поиск среди студентов
            foreach (var student in students)
            {
                if (student.FirstName.ToLower().Contains(searchTerm) ||
                    student.LastName.ToLower().Contains(searchTerm))
                {
                    results.Add($"{student.FirstName} {student.LastName}");
                }
            }

            // Поиск среди преподавателей
            foreach (var teacher in teachers)
            {
                if (teacher.FirstName.ToLower().Contains(searchTerm) ||
                    teacher.LastName.ToLower().Contains(searchTerm))
                {
                    results.Add($"{teacher.FirstName} {teacher.LastName}");
                }
            }

            ResultsListBox.ItemsSource = results;
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Справка по программе: ...");
        }

        private void CancelStudButton_Click(object sender, RoutedEventArgs e)
        {
            AddStudent.Visibility = Visibility.Hidden;
            Me_nu.Visibility = Visibility.Visible;
            Tab_Item.Visibility = Visibility.Visible;
            ClearStudFeil();
        }

        private void AddStudButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNameStudTextBox.Text) || string.IsNullOrEmpty(LastNameStudTextBox.Text) ||
                string.IsNullOrEmpty(AgeStudTextBox.Text) || string.IsNullOrEmpty(StreetStudTextBox.Text) || 
                string.IsNullOrEmpty(HouseNumberStudTextBox.Text) ||string.IsNullOrEmpty(ApartmentNumberStudTextBox.Text)||
                (!RadioButton_Men.IsChecked == true && !RadioButton_Wemen.IsChecked == true) ||
                (RadioButton_Yes.IsChecked ==  true && RadioButton_Yes.IsChecked == true))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            Student newStudent = new Student
            {
                FirstName = FirstNameStudTextBox.Text,
                LastName = LastNameStudTextBox.Text,
                MiddleName = MiddleNameStudTextBox.Text,
                Age = int.TryParse(AgeStudTextBox.Text, out int age) ? age : 0,
                PhoneNumber = PhoneNumberStudTextBox.Text,
                Address = new Address
                {
                    Street = StreetStudTextBox.Text,
                    HouseNumber = HouseNumberStudTextBox.Text,
                    ApartmentNumber = ApartmentNumberStudTextBox.Text
                },
                Gender = RadioButton_Men.IsChecked == true ? "Мужской" : "Женский",
                IsExpelled = RadioButton_Yes.IsChecked ?? false,
            };

            students.Add(newStudent);

            MessageBox.Show("Студент успешно добавлен!");

            ClearStudFeil();
        }
        private void ClearStudFeil()
        {
            FirstNameStudTextBox.Clear();
            LastNameStudTextBox.Clear();
            MiddleNameStudTextBox.Clear();
            AgeStudTextBox.Clear();
            PhoneNumberStudTextBox.Clear();
            StreetStudTextBox.Clear() ;
            HouseNumberStudTextBox.Clear();
            ApartmentNumberStudTextBox.Clear();
            RadioButton_Men.IsChecked = false;
            RadioButton_Wemen.IsChecked = false;
            RadioButton_Yes.IsChecked = false;
            RadioButton_No.IsChecked = false;
        }

        private void ClearTeachFeil ()
        {

        }

        private void AddTeachButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNameTeachTextBox.Text) || string.IsNullOrEmpty(LastNameTeachTextBox.Text) ||
                string.IsNullOrEmpty(AgeTeachTextBox.Text) || string.IsNullOrEmpty(PhoneNumberTeachTextBox.Text) ||
                string.IsNullOrEmpty(DegreeTeachTextBox.Text) || string.IsNullOrEmpty(EducationTeachTextBox.Text) ||
                (!RadioButton_MenTeach.IsChecked == true && !RadioButton_WemenTeach.IsChecked == true) ||
                (RadioButton_IsFiredTeach.IsChecked == true && RadioButton_IsnotFiredTeach.IsChecked == true))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            var subjectNames = SubjectsTeachTextBox.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var subjectName in subjectNames)
            {
                // Создаем новый объект Group для каждого имени предмета
                var subject = new Group { Name = subjectName.Trim() }; // Предполагаем, что у Group есть свойство Name
                subjects_.Add(subject);
            }


            Teacher newTeacher = new Teacher()
            {
                FirstName = FirstNameTeachTextBox.Text,
                LastName = LastNameTeachTextBox.Text,
                MiddleName = MiddleNameTeachTextBox.Text,
                Age = int.TryParse(AgeTeachTextBox.Text, out int age) ? age : 0,
                PhoneNumber = PhoneNumberTeachTextBox.Text,
                Degree = DegreeTeachTextBox.Text,
                Education = EducationTeachTextBox.Text,
                Gender = RadioButton_MenTeach.IsChecked == true ? "Мужской" : "Женский",
                IsFired = RadioButton_IsFiredTeach.IsChecked ?? false,
            };

            teachers.Add(newTeacher);

            MessageBox.Show("Преподователь успешно добавлен!");

            ClearTeachFeil();
        }

        private void CancelTeachButton_Click(object sender, RoutedEventArgs e)
        {
            AddTeacher.Visibility = Visibility.Hidden;
            Me_nu.Visibility = Visibility.Visible;
            Tab_Item.Visibility = Visibility.Visible;
        }


        private void AddSaveGroupButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CancelGroupButton_Click(object sender, RoutedEventArgs e)
        {
            AddGroup.Visibility = Visibility.Hidden;
            Me_nu.Visibility = Visibility.Visible;
            Tab_Item.Visibility = Visibility.Visible;
        }

        public void ValidateNumber(TextBox textBox)
        {
            string num = textBox.Text.Trim();

            if (!string.IsNullOrEmpty(num) && !Int32.TryParse(num, out _))
            {
                MessageBox.Show("Ошибка ввода: номер должен состоять из цифр.");
                if (num.Length > 0)
                {
                    textBox.Text = num.Substring(0, num.Length - 1);
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        public void ValidateTextBox(TextBox textBox)
        {
            string input = textBox.Text.Trim();

            // Проверяем, состоит ли ввод только из букв
            if (!string.IsNullOrEmpty(input) && !input.All(char.IsLetter))
            {
                MessageBox.Show("Ошибка ввода: поле должно содержать только буквы.");
                if (input.Length > 0)
                {
                    textBox.Text = input.Substring(0, input.Length - 1);
                }
            }
        }

        public void ValidateAddress(TextBox textBox)
        {
            string address = textBox.Text.Trim();

            if (!address.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-' || c == '.'))
            {
                MessageBox.Show("Ошибка ввода: адрес может содержать только буквы, цифры, пробелы, дефисы и точки.");

                if (address.Length > 0)
                {
                    textBox.Text = address.Substring(0, address.Length - 1);
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }
        private void FirstNameStudTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(FirstNameStudTextBox);
        }

        private void LastNameStudTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(LastNameStudTextBox);
        }

        private void MiddleNameStudTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(MiddleNameStudTextBox);
        }

        private void AgeStudTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumber(AgeStudTextBox);
        }

        private void PhoneNumberStudTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumber(PhoneNumberStudTextBox);
        }
        private void StreetStudTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateAddress(StreetStudTextBox);
        }
        private void HouseNumberStudTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateAddress(HouseNumberStudTextBox);
        }

        private void ApartmentNumberStudTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateAddress(ApartmentNumberStudTextBox);
        }

        private void FirstNameTeachTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(FirstNameTeachTextBox);
        }

        private void LastNameTeachTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(LastNameTeachTextBox);
        }

        private void MiddleNameTeachTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(MiddleNameTeachTextBox);
        }

        private void AgeTeachTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumber(AgeTeachTextBox);
        }

        private void PhoneNumberTeachTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumber(PhoneNumberTeachTextBox);
        }

        private void EducationTeachTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(EducationTeachTextBox);
        }

        private void DegreeTeachTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(DegreeTeachTextBox);
        }

        private void SubjectsTeachTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(SubjectsTeachTextBox);
        }
    }
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public string Gender { get; set; }
        public bool IsExpelled { get; set; } 
    }

    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
    }
    public class Teacher
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        public string Education { get; set; }
        public string Degree { get; set; }
        public List<string> Subjects { get; set; }
        public bool IsFired { get; set; }
    }
    public class Group
    {
        public string Name { get; set; }
        public int Course { get; set; }
        public List<string> Subjects { get; set; }
        public List<Student> Students { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
    }
}
