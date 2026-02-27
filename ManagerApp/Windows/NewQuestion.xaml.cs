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
using System.IO;

namespace ManagerApp.Windows
{
    /// <summary>
    /// Interaction logic for NewQuestion.xaml
    /// </summary>
    /// 
    public partial class NewQuestion : Window
    {
        AddQuestionViewModel addQuestionViewModel;
        public NewQuestion()
        {
            InitializeComponent();
            GetNewQuestionViewModel();
        }
      private async Task GetNewQuestionViewModel()
        {
            ApiClient<List<Answer>> apiClient = new ApiClient<List<Answer>>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/AddQuestion";
            this.addQuestionViewModel = await apiClient.GetAsync();
            ListBoxAnswer.ItemsSource = this.addQuestionViewModel.Answers;
            this.DataContext = this.addQuestionViewModel;
        }

        private async void buttonLoin_Click(object sender, RoutedEventArgs e)
        {
            bool ok;
            AddQuestionViewModel newQuestion = new AddQuestionViewModel();
            newQuestion.Question= new Question();
            newQuestion.Question.QuestionText = TextBox;
            newQuestion.Question.LevelQuestions = TextBox;
            newQuestion. = TextBox;
            newQuestion.Question.Validate();
            if (newQuestion.Question.HasErrors==false)
            {
                //שליחת נתונים לשרת בשיטת פוסט
                ApiClient<List<Answer>> apiClient = new ApiClient<List<Answer>>();
                apiClient.Schema = "http";
                apiClient.Host = "localhost";
                apiClient.Port = 5239;
                apiClient.Path = "api/Manager/AddQuestion";
                //תוספת לתמונה במידה ורוצים
                Stream stream = new FileStream();
                ok =  await apiClient.PostAsync(newQuestion);

            }
            if (ok=true)
            {
                MessageBox.Show("New Question have been added");
                this.DialogResult=true;
                this.Close();
            }
        }
    }
}
