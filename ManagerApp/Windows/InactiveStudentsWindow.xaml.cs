using FirstKitWSClient;
using FirstOrderKitModel;
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

namespace ManagerApp.Windows
{
    /// <summary>
    /// Interaction logic for InactiveStudentsWindow.xaml
    /// </summary>
    public partial class InactiveStudentsWindow : Window
    {
        public InactiveStudentsWindow()
        {
            InitializeComponent();
            _ = GetInactiveStudentList(); // טעינת הנתונים מיד עם פתיחת החלון
        }
        private async Task GetInactiveStudentList()
        {
            ApiClient<List<Student>> apiClient = new ApiClient<List<Student>>(); // שמי את שם המחלקה המדויק של סטודנט אצלך
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/GetListInactiveStudent"; // נתיב ה-API בשרת

            var result = await apiClient.GetAsync();
            ListViewInactiveStudents.ItemsSource = result;
        }
        private async void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = (Student)((Button)sender).DataContext; // שמי את שם המחלקה המדויק של סטודנט

            ApiClient<bool> apiClient = new ApiClient<bool>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/RestoreStudent";
            apiClient.AddParameter("studentId", selectedStudent.StudentId);

            if (await apiClient.GetAsync())
            {
                MessageBox.Show("הסטודנט שוחזר בהצלחה!");
                await GetInactiveStudentList(); // רענון רשימת הארכיון
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
