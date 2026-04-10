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
            ApiClient<AddQuestionViewModel> apiClient = new ApiClient<AddQuestionViewModel>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/AddQuestion";
            this.addQuestionViewModel = await apiClient.GetAsync();
           
            this.DataContext = this.addQuestionViewModel;
        }
        private async void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            bool ok = false;
            AddQuestionViewModel newQuestion = new AddQuestionViewModel();
            newQuestion.Question = new Question();
            newQuestion.Question.QuestionId = "0";
            newQuestion.Question.LevelQuestions = (ComboBoxLevel.SelectedIndex+1).ToString();
            newQuestion.Question.QuestionText = TextBoxQuestionText.Text;            
            newQuestion.Answers = new List<Answer>();
            Answer answer1 = new Answer();
            answer1.QuestionId = "0";   
            answer1.Answerid = "0";
            answer1.AnswerText=TextBoxAnswerFirst.Text;
            answer1.TrueFalse = RadioButtonFirst.IsChecked.ToString();
            newQuestion.Answers.Add(answer1);
            Answer answer2 = new Answer();
            answer2.QuestionId = "0";
            answer2.Answerid = "0";
            answer2.AnswerText = TextBoxAnswerSecond.Text;
            answer2.TrueFalse = RadioButtonSecond.IsChecked.ToString();
            newQuestion.Answers.Add(answer2);
            Answer answer3 = new Answer();
            answer3.Answerid = "0";
            answer3.QuestionId = "0";
            answer3.AnswerText = TextBoxAnswerThird.Text;
            answer3.TrueFalse = RadioButtonThird.IsChecked.ToString();
            newQuestion.Answers.Add(answer3);
            newQuestion.Question.Validate();
            if (newQuestion.Question.HasErrors == false)
            {
                ApiClient<AddQuestionViewModel> apiClient = new ApiClient<AddQuestionViewModel>();
                apiClient.Schema = "http";
                apiClient.Host = "localhost";
                apiClient.Port = 5239;
                apiClient.Path = "api/Manager/AddNewQuestion";                
                ok = await apiClient.PostAsync(newQuestion);
            }
            if (ok == true)
            {
                MessageBox.Show("New question have been added");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed try again!");
            }
        }

        private void TextBoxQuestionText_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void buttonClose_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
