using FirstKitWSClient;
using FirstOrderKitModel;
using ManagerApp.Windows;
using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using FirstKitWSClient;
using FirstOrderKitModel;

namespace ManagerApp.UserControls
{
    /// <summary>
    /// Interaction logic for QuestionManagement.xaml
    /// </summary>
    public partial class QuestionManagement : UserControl
    {
        
        List<Question> questions;
        NewQuestion newQuestionWindow;
        string imagePath;
        public QuestionManagement()
        {
            InitializeComponent();
            GetQuestionList();
        }
        private async void buttonAddNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            NewQuestion question = new NewQuestion();           
            bool? ok=  question.ShowDialog(); 
            if(ok==true)
            {
                ListViewQuestion.ItemsSource = null;
                await GetQuestionList();
            }
        }
        //שואו דיאלוג מציג חלונות
        //bool? נכון לא מכון ריק
        private bool? ViewNewQuestionWindow()
        {
            if (this.newQuestionWindow == null)
                this.newQuestionWindow = new NewQuestion();
            this.newQuestionWindow.Owner = Window.GetWindow(this);
            this.newQuestionWindow = null;
           return this.newQuestionWindow.ShowDialog();
           
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
