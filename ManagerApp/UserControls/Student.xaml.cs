using FirstKitWSClient;
using FirstOrderKitModel;
using FirstOrderKitModel;
using ManagerApp.Windows;
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

namespace ManagerApp.UserControls
{
    /// <summary>
    /// Interaction logic for Student.xaml
    /// </summary>
    /// 
    public partial class Student : UserControl
    {
        List<FirstOrderKitModel.Student> students;
        public Student()
        {
            InitializeComponent();
            GetStudentList();
        }
        private async Task GetStudentList()
        {
            ApiClient<List<FirstOrderKitModel.Student>> apiClient = 
                new ApiClient<List<FirstOrderKitModel.Student>>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/GetListStudent";
            this.students = await apiClient.GetAsync();
            ListViewStudent.ItemsSource = this.students;
            this.DataContext = this.students;
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string studentId = button.Tag.ToString();
            StudentInfo question = new StudentInfo(studentId);
            bool? ok = question.ShowDialog();
        }
        private void btnViewInactiveStudents_Click(object sender, RoutedEventArgs e)
        {
            // יצירה ופתיחה של חלון הארכיון החדש של הסטודנטים
            InactiveStudentsWindow inactiveWin = new InactiveStudentsWindow();
            inactiveWin.Owner = Window.GetWindow(this);
            inactiveWin.ShowDialog();

            // רענון הרשימה הראשית לאחר סגירת הארכיון (למקרה ששוחזר סטודנט)
             _ = GetStudentList(); 
        }
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            FrameworkElement btnDelete = sender as FrameworkElement;
            if (btnDelete != null)
            {
                FirstOrderKitModel.Student selectedQuestion = (FirstOrderKitModel.Student)clickedButton.DataContext;
                string studentId = selectedQuestion.StudentId;
                try
                {
                    ApiClient<bool> apiClient = new ApiClient<bool>();
                    apiClient.Schema = "http";
                    apiClient.Host = "localhost";
                    apiClient.Port = 5239;
                    apiClient.Path = "api/Manager/DeleteStudent";
                    apiClient.AddParameter("studentId", studentId);
                    bool isSuccess = await apiClient.GetAsync();
                    if (isSuccess)
                    {
                        MessageBox.Show("התלמיד עודכן כלא פעיל בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                        await GetStudentList();
                    }
                    else
                    {
                        MessageBox.Show("הפעולה נכשלה בשרת.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("שגיאה בתקשורת עם השרת: " + ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
    }
}
