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
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace WpfApp_StudentDB
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        private ObservableCollection<Teacher> teachers = new ObservableCollection<Teacher>();
        private ObservableCollection<Group> groups = new ObservableCollection<Group>();
        private List<string> subjects_ = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
            StudentsDataGrid.Items.Clear();
            StudentsDataGrid.ItemsSource = students;
            TeachersDataGrid.Items.Clear();
            TeachersDataGrid.ItemsSource = teachers;
            GroupsDataGrid.Items.Clear();
            GroupsDataGrid.ItemsSource = groups;
            AddStudent.Visibility = Visibility.Hidden;
            AddTeacher.Visibility = Visibility.Hidden;
            AddGroup.Visibility = Visibility.Hidden;
            Me_nu.Visibility = Visibility.Visible;
            Tab_Item.Visibility = Visibility.Visible;
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

            foreach (var student in students)
            {
                if (student.FirstName.ToLower().Contains(searchTerm) ||
                    student.LastName.ToLower().Contains(searchTerm) ||
                    student.MiddleName.ToLower().Contains(searchTerm) ||
                    student.Age.ToString().Contains(searchTerm) || 
                    student.PhoneNumber.ToLower().Contains(searchTerm) ||
                    student.Address.Street.ToLower().Contains(searchTerm) ||
                    student.Address.HouseNumber.ToString().Contains(searchTerm) || 
                                                                                   
                    student.Address.ApartmentNumber.ToLower().Contains(searchTerm) ||
                    student.Gender.ToLower().Contains(searchTerm) ||
                    student.IsExpelled.ToString().ToLower().Contains(searchTerm))
                                                                                  
                {
                    results.Add($"Студент: {student.FirstName} {student.LastName}");
                }
            }

            foreach (var teacher in teachers)
            {
                if (teacher.FirstName.ToLower().Contains(searchTerm) ||
                    teacher.LastName.ToLower().Contains(searchTerm) ||
                    teacher.MiddleName.ToLower().Contains(searchTerm) ||
                    teacher.Age.ToString().Contains(searchTerm) ||
                    teacher.PhoneNumber.ToLower().Contains(searchTerm) ||
                    teacher.Degree.ToLower().Contains(searchTerm) ||
                    teacher.Education.ToLower().Contains(searchTerm) ||
                    teacher.Subjects.Any(s => s.ToLower().Contains(searchTerm)) ||
                    teacher.Gender.ToLower().Contains(searchTerm) ||
                    teacher.IsFired.ToString().ToLower().Contains(searchTerm)) 
                                                                              
                {
                    results.Add($"Преподаватель: {teacher.FirstName} {teacher.LastName}");
                }
            }

            foreach (var group in groups)
            {
                if (group.Name.ToLower().Contains(searchTerm) ||
                    group.Major.ToLower().Contains(searchTerm) ||
                    group.Course.ToString().Contains(searchTerm) ||
                    group.Degree.ToLower().Contains(searchTerm) ||
                    group.Subjects.Any(s => s.ToLower().Contains(searchTerm)) ||
                    group.Students.Any(s => s.FirstName.ToLower().Contains(searchTerm) ||
                                            s.LastName.ToLower().Contains(searchTerm) ||
                                            s.MiddleName.ToLower().Contains(searchTerm)))
                {
                    results.Add($"Группа: {group.Name}");
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

        #region Students
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
                string.IsNullOrEmpty(HouseNumberStudTextBox.Text) || string.IsNullOrEmpty(ApartmentNumberStudTextBox.Text) ||
                (!RadioButton_Men.IsChecked == true && !RadioButton_Wemen.IsChecked == true) ||
                (RadioButton_Yes.IsChecked == true && RadioButton_No.IsChecked == true))
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
                IsExpelled = RadioButton_Yes.IsChecked == true ? "Отчислен(а)" : "Учится",
            };

            students.Add(newStudent);
            StudentsGroupComboBox.ItemsSource = null;
            StudentsGroupComboBox.ItemsSource = students;
            StudentsGroupComboBox.DisplayMemberPath = "FullName";
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
            StreetStudTextBox.Clear();
            HouseNumberStudTextBox.Clear();
            ApartmentNumberStudTextBox.Clear();
            RadioButton_Men.IsChecked = false;
            RadioButton_Wemen.IsChecked = false;
            RadioButton_Yes.IsChecked = false;
            RadioButton_No.IsChecked = false;
        }
        public void ExpelStudent_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                if (selectedStudent.IsExpelled != "Отчислен(а)")
                {
                    selectedStudent.IsExpelled = "Отчислен(а)";
                    MessageBox.Show($"Студент {selectedStudent.FirstName} {selectedStudent.LastName} успешно отчислен(а).");
                }
                else
                {
                    MessageBox.Show($"Студент {selectedStudent.FirstName} {selectedStudent.LastName} уже отчислен(а).");
                }
                students.Remove(selectedStudent);
                students.Add(selectedStudent);
                
                StudentsDataGrid.ItemsSource = null;
                StudentsDataGrid.ItemsSource = students;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите студента для отчисления.");
            }
        }

        private void EditStudButton_Click(object sender, RoutedEventArgs e)
        {
            AddStudentButton_Click(sender, e);
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                FirstNameStudTextBox.Text = selectedStudent.FirstName;
                LastNameStudTextBox.Text = selectedStudent.LastName;
                MiddleNameStudTextBox.Text = selectedStudent.MiddleName;
                AgeStudTextBox.Text = selectedStudent.Age.ToString();
                PhoneNumberStudTextBox.Text = selectedStudent.PhoneNumber;
                StreetStudTextBox.Text = selectedStudent.Address.Street;
                HouseNumberStudTextBox.Text = selectedStudent.Address.HouseNumber;
                ApartmentNumberStudTextBox.Text = selectedStudent.Address.ApartmentNumber;

                RadioButton_Men.IsChecked = selectedStudent.Gender == "Мужской";
                RadioButton_Wemen.IsChecked = selectedStudent.Gender == "Женский";
                RadioButton_Yes.IsChecked = selectedStudent.IsExpelled == "Отчислен(а)";
                RadioButton_No.IsChecked = selectedStudent.IsExpelled == "Учится";

                SaveStudentButton.Visibility = Visibility.Visible;
                AddStudButton.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите студента для редактирования.");
            }
        }
        private void SaveStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                selectedStudent.FirstName = FirstNameStudTextBox.Text;
                selectedStudent.LastName = LastNameStudTextBox.Text;
                selectedStudent.MiddleName = MiddleNameStudTextBox.Text;
                selectedStudent.Age = int.Parse(AgeStudTextBox.Text);
                selectedStudent.PhoneNumber = PhoneNumberStudTextBox.Text;
                selectedStudent.Address.Street = StreetStudTextBox.Text;
                selectedStudent.Address.HouseNumber = HouseNumberStudTextBox.Text;
                selectedStudent.Address.ApartmentNumber = ApartmentNumberStudTextBox.Text;
                selectedStudent.Gender = RadioButton_Men.IsChecked == true ? "Мужской" : "Женский";
                selectedStudent.IsExpelled = RadioButton_Yes.IsChecked == true ? "Отчислен(а)" : "Учится";

                StudentsDataGrid.ItemsSource = null;
                StudentsDataGrid.ItemsSource = students;

                MessageBox.Show($"Данные студента {selectedStudent.FirstName} {selectedStudent.LastName} обновлены.");

                SaveStudentButton.Visibility = Visibility.Hidden;
                CancelStudButton_Click(sender, e);
                AddStudButton.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите студента для сохранения изменений.");
            }
            ClearStudFeil();
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
        #endregion

        #region Teacher
        private void ClearTeachFeil()
        {
            FirstNameTeachTextBox.Clear();
            LastNameTeachTextBox.Clear();
            MiddleNameTeachTextBox.Clear();
            AgeTeachTextBox.Clear();
            PhoneNumberTeachTextBox.Clear();
            DegreeTeachTextBox.Clear();
            EducationTeachTextBox.Clear();
            RadioButton_MenTeach.IsChecked = false;
            RadioButton_WemenTeach.IsChecked = false;
            RadioButton_IsFiredTeach.IsChecked = false;
            RadioButton_IsnotFiredTeach.IsChecked = false;
            SubjectsTeachTextBox.Text = string.Empty;
            SubjectsTeachTextBox.Text = string.Empty;
        }

        private void AddTeachButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNameTeachTextBox.Text) || string.IsNullOrEmpty(LastNameTeachTextBox.Text) ||
                string.IsNullOrEmpty(AgeTeachTextBox.Text) || string.IsNullOrEmpty(PhoneNumberTeachTextBox.Text) ||
                string.IsNullOrEmpty(DegreeTeachTextBox.Text) || string.IsNullOrEmpty(EducationTeachTextBox.Text) ||
                (!RadioButton_MenTeach.IsChecked == true && !RadioButton_WemenTeach.IsChecked == true) ||
                (RadioButton_IsFiredTeach.IsChecked == true && RadioButton_IsnotFiredTeach.IsChecked == true) ||
                (SubjectsTeachTextBox.Text == null))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            var selectedSubject = SubjectsTeachTextBox.Text.Trim();

            Teacher newTeacher = new Teacher()
            {
                FirstName = FirstNameTeachTextBox.Text,
                LastName = LastNameTeachTextBox.Text,
                MiddleName = MiddleNameTeachTextBox.Text,
                Age = int.TryParse(AgeTeachTextBox.Text, out int age) ? age : 0,
                PhoneNumber = PhoneNumberTeachTextBox.Text,
                Degree = DegreeTeachTextBox.Text,
                Education = EducationTeachTextBox.Text,
                Subjects = new List<string> { selectedSubject },
                Gender = RadioButton_MenTeach.IsChecked == true ? "Мужской" : "Женский",
                IsFired = RadioButton_IsFiredTeach.IsChecked == true ? "Уволен(а)" : "Работает",
            };
            teachers.Add(newTeacher);

            MessageBox.Show("Преподаватель успешно добавлен!");

            ClearTeachFeil();
        }

        private void CancelTeachButton_Click(object sender, RoutedEventArgs e)
        {
            AddTeacher.Visibility = Visibility.Hidden;
            Me_nu.Visibility = Visibility.Visible;
            Tab_Item.Visibility = Visibility.Visible;
            ClearTeachFeil();
        }

        private void DismistTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersDataGrid.SelectedItem is Teacher selectedTeacher)
            {
                if (selectedTeacher.IsFired != "Уволен(а)")
                {
                    teachers.Remove(selectedTeacher);
                    selectedTeacher.IsFired = "Уволен(а)";
                    MessageBox.Show($"Преподаватель {selectedTeacher.FirstName} {selectedTeacher.LastName} успешно уволен.");
                    teachers.Add(selectedTeacher);
                    TeachersDataGrid.ItemsSource = null;
                    TeachersDataGrid.ItemsSource = teachers;
                }
                else
                {
                    MessageBox.Show($"Преподаватель {selectedTeacher.FirstName} {selectedTeacher.LastName} уже уволен.");
                }

            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите преподавателя для увольнения.");
            }
        }

        private void AddSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersDataGrid.SelectedItem is Teacher selectedTeacher)
            {
                if (!string.IsNullOrEmpty(SubjectsTeachTextBox.Text))
                {
                    if (!selectedTeacher.Subjects.Contains(SubjectsTeachTextBox.Text))
                    {
                        selectedTeacher.Subjects.Add(SubjectsTeachTextBox.Text);
                        MessageBox.Show($"Предмет '{SubjectsTeachTextBox.Text}' добавлен к преподавателю {selectedTeacher.FirstName} {selectedTeacher.LastName}.");
                    }
                    else
                    {
                        MessageBox.Show($"Предмет '{SubjectsTeachTextBox.Text}' уже добавлен к преподавателю {selectedTeacher.FirstName} {selectedTeacher.LastName}.");
                    }

                    TeachersDataGrid.ItemsSource = null;
                    TeachersDataGrid.ItemsSource = teachers;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите предмет для добавления.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите преподавателя для добавления предмета.");
            }
        }

        private void RemoveSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersDataGrid.SelectedItem is Teacher selectedTeacher)
            {
                if (!string.IsNullOrEmpty(SubjectsTeachTextBox.Text))
                {
                    if (selectedTeacher.Subjects.Contains(SubjectsTeachTextBox.Text))
                    {
                        selectedTeacher.Subjects.Remove(SubjectsTeachTextBox.Text);
                        MessageBox.Show($"Предмет '{SubjectsTeachTextBox.Text}' удален у преподавателя {selectedTeacher.FirstName} {selectedTeacher.LastName}.");
                    }
                    else
                    {
                        MessageBox.Show($"Предмет '{SubjectsTeachTextBox.Text}' не найден у преподавателя {selectedTeacher.FirstName} {selectedTeacher.LastName}.");
                    }

                    TeachersDataGrid.ItemsSource = null;
                    TeachersDataGrid.ItemsSource = teachers;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите предмет для удаления.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите преподавателя для удаления предмета.");
            }
        }
        private void EditTeachButton_Click(object sender, RoutedEventArgs e)
        {
            AddTeacherButton_Click(sender, e);
            if (TeachersDataGrid.SelectedItem is Teacher selectedTeacher)
            {
                FirstNameTeachTextBox.Text = selectedTeacher.FirstName;
                LastNameTeachTextBox.Text = selectedTeacher.LastName;
                MiddleNameTeachTextBox.Text = selectedTeacher.MiddleName;
                AgeTeachTextBox.Text = selectedTeacher.Age.ToString();
                PhoneNumberTeachTextBox.Text = selectedTeacher.PhoneNumber.ToString();
                PhoneNumberTeachTextBox.Text = selectedTeacher.PhoneNumber;
                DegreeTeachTextBox.Text = selectedTeacher.Degree;
                EducationTeachTextBox.Text = selectedTeacher.Education;
                SubjectsTeachTextBox.Text = string.Join("", selectedTeacher.Subjects);
                if (selectedTeacher.Gender == "Мужской")
                {
                    RadioButton_MenTeach.IsChecked = true;
                }
                else
                {
                    RadioButton_WemenTeach.IsChecked = true;
                }

                if (selectedTeacher.IsFired == "Уволен(а)")
                {
                    RadioButton_IsFiredTeach.IsChecked = true;
                }
                else
                {
                    RadioButton_IsnotFiredTeach.IsChecked = true;
                }
                SaveTeachButton.Visibility = Visibility.Visible;
                AddTeachButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите студента для редактирования.");
            }
        }
        private void SaveTeachButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersDataGrid.SelectedItem is Teacher selectedTeacher)
            {
                var selectedSubject = SubjectsTeachTextBox.Text.Trim();

                selectedTeacher.FirstName = FirstNameTeachTextBox.Text;
                selectedTeacher.LastName = LastNameTeachTextBox.Text;
                selectedTeacher.MiddleName = MiddleNameTeachTextBox.Text;
                selectedTeacher.Age = int.Parse(AgeTeachTextBox.Text);
                selectedTeacher.PhoneNumber = PhoneNumberTeachTextBox.Text;
                selectedTeacher.Degree = DegreeTeachTextBox.Text;
                selectedTeacher.Education = EducationTeachTextBox.Text;
                selectedTeacher.Subjects = new List<string> { selectedSubject };
                selectedTeacher.Gender = RadioButton_MenTeach.IsChecked == true ? "Мужской" : "Женский";
                selectedTeacher.IsFired = RadioButton_IsFiredTeach.IsChecked == true ? "Уволен(а)" : "Работает";

                TeachersDataGrid.ItemsSource = null;
                TeachersDataGrid.ItemsSource = teachers;

                MessageBox.Show($"Данные студента {selectedTeacher.FirstName} {selectedTeacher.LastName} обновлены.");

                SaveTeachButton.Visibility = Visibility.Hidden;
                CancelTeachButton_Click(sender, e);
                AddTeachButton.Visibility = Visibility.Hidden;
                ClearTeachFeil();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите студента для сохранения изменений.");
            }
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
        #endregion

        #region Group
        private void ClearGroupFeil()
        {
            GroupNameTextBox.Clear();
            MajorTextBox.Clear();
            CourseTextBox.Clear();
            DegreeTextBox.Clear();
            SubjectsGroupTextBox.Text = string.Empty;
            StudentsGroupComboBox.Text = string.Empty;
            StudentsGroupComboBox.SelectedItem = null;
        }

        private void AddSaveGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(GroupNameTextBox.Text) || string.IsNullOrEmpty(CourseTextBox.Text) ||
                string.IsNullOrEmpty(MajorTextBox.Text) || string.IsNullOrEmpty(DegreeTextBox.Text) ||
                (SubjectsGroupTextBox.Text == string.Empty && string.IsNullOrEmpty(StudentsGroupComboBox.Text)) || StudentsGroupComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            if (groups.Any(g => g.Name.Equals(GroupNameTextBox.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Группа с таким именем уже существует.");
                return;
            }

            Group newGroup = new Group()
            {
                Name = GroupNameTextBox.Text,
                Course = int.TryParse(CourseTextBox.Text, out int course) ? course : 0,
                Degree = DegreeTextBox.Text,
                Major = MajorTextBox.Text,
                Subjects = new List<string> { SubjectsTeachTextBox.Text },
                Students = new ObservableCollection<Student> { }
            };

            groups.Add(newGroup);
            MessageBox.Show("Группа успешно добавлена!");

            ClearGroupFeil();

        }

        private void CancelGroupButton_Click(object sender, RoutedEventArgs e)
        {
            AddGroup.Visibility = Visibility.Hidden;
            Me_nu.Visibility = Visibility.Visible;
            Tab_Item.Visibility = Visibility.Visible;
        }

        public void DisbandGroup_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem != null || GroupsDataGrid.SelectedCells != null)
            {
                GroupsDataGrid.SelectedCells.Clear();
            }
        }

        private void PromoteGroupButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedGroup = GroupsDataGrid.SelectedItem as Group;
            if (selectedGroup != null && selectedGroup.Course < 4)
            {
                selectedGroup.Course++;
            }
            else
            {
                MessageBox.Show("Группа уже на максимальном курсе.");
            }
        }

        private void TransferStudentButton_Click(object sender, RoutedEventArgs e)
        {
            NewGroupLabel.Visibility = Visibility.Visible;
            NewGroupTextBox.Visibility = Visibility.Visible;
            var selectedStudent = StudentsGroupComboBox.SelectedItem as Student;
            var newGroupName = NewGroupTextBox.Text;

            if (selectedStudent == null)
            {
                MessageBox.Show("Пожалуйста, выберите студента для перевода.");
                return;
            }

            if (string.IsNullOrEmpty(newGroupName))
            {
                MessageBox.Show("Пожалуйста, введите название новой группы.");
                return;
            }

            var currentGroup = groups.FirstOrDefault(g => g.Students.Contains(selectedStudent));

            if (currentGroup != null)
            {
                currentGroup.Students.Remove(selectedStudent);

                var newGroup = groups.FirstOrDefault(g => g.Name.Equals(newGroupName, StringComparison.OrdinalIgnoreCase));

                if (newGroup != null)
                {
                    newGroup.Students.Add(selectedStudent);
                    MessageBox.Show($"Студент {selectedStudent.FirstName} переведен в группу {newGroupName}.");
                }
                else
                {
                    MessageBox.Show($"Группа {newGroupName} не найдена.");
                }
            }
            else
            {
                MessageBox.Show("Студент не найден в выбранной группе.");
            }
        }

        private void AddSubjectToGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem is Group selectedGroup)
            {
                if (!string.IsNullOrEmpty(SubjectsTeachTextBox.Text))
                {
                    if (!selectedGroup.Subjects.Contains(SubjectsTeachTextBox.Text))
                    {
                        selectedGroup.Subjects.Add(SubjectsTeachTextBox.Text); 
                        MessageBox.Show($"Предмет '{SubjectsTeachTextBox.Text}' добавлен к группе {selectedGroup.Name}.");
                    }
                    else
                    {
                        MessageBox.Show($"Предмет '{SubjectsTeachTextBox.Text}' уже добавлен к группе {selectedGroup.Name}.");
                    }

                    GroupsDataGrid.ItemsSource = null; 
                    GroupsDataGrid.ItemsSource = groups; 
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите предмет для добавления.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите группу для добавления предмета.");
            }
        }

        private void RemoveSubjectFromGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem is Group selectedGroup)
            {
                if (!string.IsNullOrEmpty(SubjectsTeachTextBox.Text))
                {
                    if (selectedGroup.Subjects.Contains(SubjectsTeachTextBox.Text))
                    {
                        selectedGroup.Subjects.Remove(SubjectsTeachTextBox.Text); 
                        MessageBox.Show($"Предмет '{SubjectsTeachTextBox.Text}' удален из группы {selectedGroup.Name}.");
                    }
                    else
                    {
                        MessageBox.Show($"Предмет '{SubjectsTeachTextBox.Text}' не найден в группе {selectedGroup.Name}.");
                    }

                    GroupsDataGrid.ItemsSource = null;
                    GroupsDataGrid.ItemsSource = groups; 
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите предмет для удаления.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите группу для удаления предмета.");
            }
        }

        private void EditGroupButton_Click(object sender, RoutedEventArgs e)
        {
            AddGroupButton_Click(sender, e);
            if (GroupsDataGrid.SelectedItem is Group selectedGroup)
            {
                GroupNameTextBox.Text = selectedGroup.Name;
                CourseTextBox.Text = selectedGroup.Course.ToString();
                MajorTextBox.Text = selectedGroup.Major;
                DegreeTextBox.Text = selectedGroup.Degree;
                SubjectsGroupTextBox.Text = string.Join("", selectedGroup.Subjects);
                StudentsGroupComboBox.Text = string.Join("", selectedGroup.Students);
                SaveGroupButton.Visibility = Visibility.Visible;
                AddGroupButton.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите студента для редактирования.");
            }
        }
        private void SaveGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem is Group selectedGroup)
            {
                var selectedSubject = SubjectsGroupTextBox.Text.Trim();
                var selectedStudent = StudentsGroupComboBox.Text;

                selectedGroup.Name = GroupNameTextBox.Text;
                selectedGroup.Course = Int32.Parse(CourseTextBox.Text);
                selectedGroup.Major = MajorTextBox.Text;
                selectedGroup.Degree = DegreeTextBox.Text;

                selectedGroup.Subjects = new List<string> { selectedSubject };
                selectedGroup.Students = new ObservableCollection<Student> { new Student { FirstName = selectedStudent } };

                GroupsDataGrid.ItemsSource = null;
                GroupsDataGrid.ItemsSource = groups;

                MessageBox.Show($"Данные студента {selectedGroup.Name} {selectedGroup.Course} обновлены.");

                SaveGroupButton.Visibility = Visibility.Hidden;
                CancelGroupButton_Click(sender, e);
                AddGroupButton.Visibility = Visibility.Hidden;
                ClearGroupFeil();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите студента для сохранения изменений.");
            }
        }

        private void GroupNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(GroupNameTextBox);
        }

        private void CourseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumber(CourseTextBox);
            if (int.TryParse(CourseTextBox.Text, out int value))
            {
                if (value > 4 || value<1)
                {
                    CourseTextBox.Text = "";
                    MessageBox.Show("Неверное значение курса");
                }
            }
        }

        private void MajorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(MajorTextBox);
        }

        private void DegreeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox(DegreeTextBox);
        }

        #endregion

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

            if (!address.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-' || c == '.' || c == ','))
            {
                MessageBox.Show("Ошибка ввода: адрес может содержать только буквы, цифры, пробелы, дефисы и точки.");

                if (address.Length > 0)
                {
                    textBox.Text = address.Substring(0, address.Length - 1);
                    textBox.SelectionStart = textBox.Text.Length;
                }
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
            public string IsExpelled { get; set; }

            public string FullName => $"{FirstName} {LastName}";
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
            public string IsFired { get; set; }
        }
        public class Group
        {
            public string Name { get; set; }
            public int Course { get; set; }
            public List<string> Subjects { get; set; }
            public ObservableCollection<Student> Students { get; set; }
            public string Major { get; set; }
            public string Degree { get; set; }
        }


    }
}