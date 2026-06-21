using FirstKitWSClient;
using FirstOrderKitModel;
using Microsoft.Extensions.Primitives;
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
using System.Windows.Shapes;

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
            newQuestion.QuestionId = "0";
            newQuestion.LevelQuestions = (ComboBoxLevel.SelectedIndex + 1).ToString();
            newQuestion.QuestionText = TextBoxQuestionText.Text;
            newQuestion.Question.QuestionId = "0";
            newQuestion.Question.LevelQuestions = (ComboBoxLevel.SelectedIndex + 1).ToString();
            newQuestion.Question.QuestionText = TextBoxQuestionText.Text;
            newQuestion.Answers = new List<Answer>();
            Answer answer1 = new Answer();
            answer1.QuestionId = "0";
            answer1.Answerid = "0";
            answer1.AnswerText = TextBoxAnswerFirst.Text;
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
            Dictionary<string, List<string>> errors = newQuestion.Question.AllError();
            StringBuilder sb = new StringBuilder();
            // בדיקה - האם נבחרה תשובה נכונה - מצטרפת לאותו StringBuilder
            if (RadioButtonFirst.IsChecked != true &&
                RadioButtonSecond.IsChecked != true &&
                RadioButtonThird.IsChecked != true)
            {
                sb.Append("CorrectAnswer:\n\n");
                sb.Append("You must select the correct answer");
                sb.Append("\n\n");
            }
            foreach (var errorEntry in errors)
            {
                string propertyName = errorEntry.Key;
                sb.Append($"{propertyName}:\n\n");
                foreach (var error in errorEntry.Value)
                {
                    //sb.Append($"{error}, ");
                    //string errorMessage = string.Join(error, "\n");
                    //sb.Append($"{propertyName} : {errorMessage}");
                    sb.Append($"{propertyName} : {error}\n");
                }
                sb.Append($"\n\n");
            }
            // ✅ חדש - Validation על כל Answer בנפרד
            bool allAnswersValid = true;
            int answerNumber = 1;
            foreach (Answer answer in newQuestion.Answers)
            {
                answer.Validate();
                Dictionary<string, List<string>> answerErrors = answer.AllError();
                if (answer.HasErrors)
                {
                    allAnswersValid = false;
                    sb.Append($"Answer {answerNumber}:\n\n");
                    foreach (var errorEntry in answerErrors)
                    {
                        foreach (var error in errorEntry.Value)
                        {
                            sb.Append($"{errorEntry.Key} : {error}\n");
                        }
                    }
                    sb.Append("\n\n");
                }
                answerNumber++;
            }
            //מחבר את כל השגיאות ברשימה למחרוזת אחת מופרדת בפסיקים 
            if (sb.Length > 0)
            {
                //הדפסה בפורמט המבוקש
                MessageBox.Show(sb.ToString(),
                "Please correct the following errors:",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }

               

           
            if (newQuestion.Question.HasErrors == false && allAnswersValid)
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
