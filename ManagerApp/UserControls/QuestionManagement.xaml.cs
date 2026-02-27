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
        AddQuestionViewModel addQuestionViewModel;
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
            bool ok = false;
            AddQuestionViewModel newQuestion = new AddQuestionViewModel();
            newQuestion.Question = new Question();
            newQuestion.Question.QuestionId = "0";
            newQuestion.Question.LevelQuestions = textBoxLevelQuestions.Text;
            newQuestion.Question.QuestionText = textBoxQuestionText.Text;
            //newQuestion.Book.BookImage = System.IO.Path.GetExtension(this.imagePath);
            newQuestion.Answers = listBoxAnswers.SelectedItems.Cast<Answer>().ToList<Answer>();
            //newQuestion.Authors = listBoxAuthors.SelectedItems.Cast<Author>().ToList<Author>();
            newQuestion.Question.Validate();
            if (newQuestion.Question.HasErrors == false)
            {
                ApiClient<AddQuestionViewModel> apiClient = new ApiClient<AddQuestionViewModel>();
                apiClient.Scheme = "http";
                apiClient.Host = "localhost";
                apiClient.Port = 5273;
                apiClient.Path = "api/Manager/AddNewQuestion";
                Stream stream = new FileStream(this.imagePath,
                                                FileMode.Open,
                                                FileAccess.Read);
                ok = await apiClient.PostAsync(newQuestion, stream);
            }
            if (ok == true)
            {
                MessageBox.Show("New question have been addad");
                this.DialogResult = true;
                this.Close();
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
            apiClient.Path = "api/Manager/GetNewQuestionViewModel";
            this.addQuestionViewModel = await apiClient.GetAsync();
            this.addQuestionViewModel.Question = new Question();
            this.listBoxAuthors.ItemsSource = this.addQuestionViewModel.Answers;
            //this.listBoxGenres.ItemsSource = this.addQuestionViewModel.Genres;
            this.DataContext = this.addQuestionViewModel;
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        //private void ButtonSelectImage_Click()
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "Image Only(*.JPG,*.PNG)|*.JPG;*.PNG";
        //        imagePath.Source = BitmapImage;
        //}
    }
}
