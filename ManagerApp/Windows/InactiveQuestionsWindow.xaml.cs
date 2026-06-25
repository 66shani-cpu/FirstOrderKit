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
    /// Interaction logic for InactiveQuestionsWindow.xaml
    /// </summary>
    public partial class InactiveQuestionsWindow : Window
    {
        private List<Question> inactiveQuestions;
        public InactiveQuestionsWindow()
        {
            InitializeComponent();
            GetInactiveQuestionList();
        }
        private async Task GetInactiveQuestionList()
        {
            try
            {
                ApiClient<List<Question>> apiClient = new ApiClient<List<Question>>();
                apiClient.Schema = "http";
                apiClient.Host = "localhost";
                apiClient.Port = 5239;
                apiClient.Path = "api/Manager/GetListInactiveQuestion"; // ודאי שזהו הנתיב המדויק ב-API שלך לשאלות מחוקות

                this.inactiveQuestions = await apiClient.GetAsync();
                ListViewInactiveQuestions.ItemsSource = this.inactiveQuestions;
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בטעינת הארכיון: " + ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Question selectedQuestion = (Question)clickedButton.DataContext;
                string questionId = selectedQuestion.QuestionId;

                try
                {
                    ApiClient<bool> apiClient = new ApiClient<bool>();
                    apiClient.Schema = "http";
                    apiClient.Host = "localhost";
                    apiClient.Port = 5239;
                    apiClient.Path = "api/Manager/RestoreQuestion"; // פונקציית ה-API החדשה לשחזור
                    apiClient.AddParameter("questionId", questionId);

                    bool isSuccess = await apiClient.GetAsync();
                    if (isSuccess)
                    {
                        MessageBox.Show("השאלה שוחזרה והוחזרה לרשימה הפעילה בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);

                        // רענון הרשימה המקומית של הארכיון לאחר השחזור
                        ListViewInactiveQuestions.ItemsSource = null;
                        await GetInactiveQuestionList();
                    }
                    else
                    {
                        MessageBox.Show("פעולת השחזור נכשלה בשרת.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("שגיאה בתקשורת עם השרת בזמן שחזור: " + ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

