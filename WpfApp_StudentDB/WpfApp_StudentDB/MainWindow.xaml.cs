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

namespace WpfApp_StudentDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Student> students = new List<Student>();
        private List<Teacher> teachers = new List<Teacher>();
        private List<Group> groups = new List<Group>();

        public MainWindow()
        {
            InitializeComponent();
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
        }

        private void AddStudButton_Click(object sender, RoutedEventArgs e)
        {
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
                Gender = "Мужской" // или "Женский" в зависимости от выбора
            };

            students.Add(newStudent);

            MessageBox.Show("Студент успешно добавлен!");
        }
        private void CancelTeachButton_Click(object sender, RoutedEventArgs e)
        {
            AddTeacher.Visibility = Visibility.Hidden;
            Me_nu.Visibility = Visibility.Visible;
            Tab_Item.Visibility = Visibility.Visible;
        }

        private void AddTeachButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void CancelGroupButton_Click(object sender, RoutedEventArgs e)
        {
            AddGroup.Visibility = Visibility.Hidden;
            Me_nu.Visibility = Visibility.Visible;
            Tab_Item.Visibility = Visibility.Visible;
        }

        private void AddSaveGroupButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; } // Опционально
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public string Gender { get; set; }
        public bool IsExpelled { get; set; } // Пометка об отчислении
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
        public string MiddleName { get; set; } // Опционально
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Education { get; set; }
        public string Degree { get; set; }
        public List<string> Subjects { get; set; }
        public bool IsFired { get; set; } // Пометка об увольнении
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
