using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ManagerApp.UserControls;

namespace ManagerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartPage startPage;
        LogInPage logInPage;
        Update update;
        Student student;
        Home home;
        Reports reports;
        QuestionManagement questionManagement;
        Unit_SubjectManagement unit_SubjectManagement;
        public MainWindow()
        {
            InitializeComponent();
            //ViewStartPage();
            frameMain.Navigate(new StartPage());
        }
        private void ViewStartPage()
        {
            if (this.startPage == null)
            {
                this.startPage = new StartPage();
                this.frameMain.Content = this.startPage;
            }
        }
        private void ViewLogInPage()
        {
            if (this.logInPage == null)
            {
                this.logInPage = new LogInPage();
                this.frameMain.Content = this.logInPage;
            }
        }
        private void ViewUpdatePage()
        {
            if (this.update == null)
            {
                this.update = new Update();
                this.frameMain.Content = this.update;
            }
        }
        private void ViewStudentManagement()
        {
            if (this.student == null)
            {
                this.student = new Student();
                this.frameMain.Content = this.student;
            }
        }
        private void ViewHome()
        {
            if (this.home == null)
            {
                this.home = new Home();
                this.frameMain.Content = this.home;
            }
        }
        private void ViewReports()
        {
            if (this.reports == null)
            {
                this.reports = new Reports();
                this.frameMain.Content = this.reports;
            }
        }
        private void ViewQuestionManagement()
        {
            if (this.questionManagement == null)
            {
                this.questionManagement = new QuestionManagement();
                this.frameMain.Content = this.questionManagement;
            }
        }
        private void ViewUnit_SubjectManagement()
        {
            if (this.unit_SubjectManagement == null)
            {
                this.unit_SubjectManagement = new Unit_SubjectManagement();
                this.frameMain.Content = this.unit_SubjectManagement;
            }
        }

        private void hyperlinkLogin_Click(object sender, RoutedEventArgs e)
        {
            ViewLogInPage();
        }
        private void hyperlinkStartPage_Click(object sender, RoutedEventArgs e)
        {
            //ViewStartPage();
            frameMain.Navigate(new StartPage());
        }
        private void hyperlinkLogin_Click(object sender, RoutedEventArgs e)
        {
            ViewLogInPage();
        }

    }
}