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
    /// Interaction logic for QuestionManagement.xaml
    /// </summary>
    public partial class QuestionManagement : UserControl
    {
        List<Question> questions;
        public QuestionManagement()
        {
            InitializeComponent();
            GetQuestionList();
        }
        private async Task GetQuestionList()
        {
            ApiClient<List<Question>> apiClient = new ApiClient<List<Question>>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/GetListQuestion";
            this.questions = await apiClient.GetAsync();
            ListViewQuestion.ItemsSource = this.questions;
            this.DataContext = this.questions;
        }
    }
}
