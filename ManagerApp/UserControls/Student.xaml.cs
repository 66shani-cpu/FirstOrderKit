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
        List<Student> students;
        public Student()
        {
            InitializeComponent();
        }
        private async Task GetStudentList()
        {
            ApiClient<List<Student>> apiClient = new ApiClient<List<Student>>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/פעולה של לקיחת רשימה של תלמידים";
            this.students = await apiClient.GetAsync();
           // ListViewStudent.ItemsSource = this.students;
            this.DataContext = this.students;
        }
    }
}
