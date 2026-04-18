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
    /// Interaction logic for NewUnit.xaml
    /// </summary>
    public partial class StudentInfo : Window
    {
        Student student;
        public StudentInfo()
        {
            GetStudentInfo();
            InitializeComponent();
        }
        private async Task GetStudentInfo()
        {
            ApiClient<Student> apiClient = new ApiClient<Student>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/GetStudentInfo";
            this.student = await apiClient.GetAsync();
            this.DataContext = this.student;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
