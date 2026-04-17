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
            StudentInfo question = new StudentInfo();
            bool? ok = question.ShowDialog();
        }
    }
}
