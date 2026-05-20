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
        Student student;
        Home home;
        QuestionManagement questionManagement;
        Unit_SubjectManagement unit_SubjectManagement;
        UnitReportUserControl unitReportUserControl;
        ExitPage exitPage;

        bool isLogin = false;
        public void SetAdmin(bool isadmin)
        {
            this.isLogin=isadmin;
            HyperLinkState();
            if (this.isLogin==true)
            {
                ViewStartPage();
            }
        }
        private void HyperLinkState()
        {
            this.hyperlinkQuestion.IsEnabled = this.isLogin;
            this.hyperlinkStudent.IsEnabled = this.isLogin;
            this.hyperlinkUnit_Subject.IsEnabled = this.isLogin;
            this.hyperlinkUnit_ReportUserControl.IsEnabled = this.isLogin;
        }
       
        public MainWindow()
        {
            InitializeComponent();
            ViewStartPage();
            //frameMain.Navigate(new StartPage());
            HyperLinkState();
        }
        public void ViewStartPage()
        {
            if (this.startPage == null)
            {
                this.startPage = new StartPage();
                
            }this.frameMain.Content = this.startPage;
        }
        private void ViewLogInPage()
        {
            if (this.logInPage == null)
            {
                this.logInPage = new LogInPage();
                
            }this.frameMain.Content = this.logInPage;
        }
        private void ViewStudentManagement()
        {
            if (this.student == null)
            {
                this.student = new Student();
                
            }this.frameMain.Content = this.student;
        }
        private void ViewUnitReportUserControl()
        {
            if (this.unitReportUserControl == null)
            {
                this.unitReportUserControl = new UnitReportUserControl();
                
            }this.frameMain.Content = this.unitReportUserControl;
        }
        private void ViewQuestionManagement()
        {
            if (this.questionManagement == null)
            {
                this.questionManagement = new QuestionManagement();
                
            }this.frameMain.Content = this.questionManagement;
        }
        private void ViewUnit_SubjectManagement()
        {
            if (this.unit_SubjectManagement == null)
            {
                this.unit_SubjectManagement = new Unit_SubjectManagement();
               
            } this.frameMain.Content = this.unit_SubjectManagement;
        }
        private void ViewExit()
        {
            if (this.exitPage == null)
            {
                this.exitPage = new ExitPage();
                
            }this.frameMain.Content = this.exitPage;
        }

        private void hyperlinkLogin_Click(object sender, RoutedEventArgs e)
        {
            if(this.isLogin == false)
            {
                ViewLogInPage();
                this.hyperlinkLogin.Inlines.Clear();
                this.hyperlinkLogin.Inlines.Add("Logout");
            }
            else
            {
                this.isLogin = false;
                this.hyperlinkLogin.Inlines.Clear();
                this.hyperlinkLogin.Inlines.Add("Login");
                HyperLinkState();
            }
        }
        private void hyperlinkStartPage_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new StartPage());
        }
        private void hyperlinkQuestion_Click(object sender, RoutedEventArgs e)
        {
            ViewQuestionManagement();
        }
        private void hyperlinkStudent_Click(object sender, RoutedEventArgs e)
        {
            ViewStudentManagement();
        }
        private void hyperlinkUnit_Subject_Click(object sender, RoutedEventArgs e)
        {
            ViewUnit_SubjectManagement();
        }

        private void hyperlinkUnitReportUserControl(object sender, RoutedEventArgs e)
        {
            ViewUnitReportUserControl();
        }
        private void hyperlinkExit_Click(object sender, RoutedEventArgs e)
        {
            ViewExit();
            //Application.Current.Shutdown();
        }
        //להוסיף שדה בטבלה שרוצים פעיל או לא פעיל 
        // בגט אולל לשנות עם סינון WHERE שם של דה שווה נכון או לא נכון


    }
}