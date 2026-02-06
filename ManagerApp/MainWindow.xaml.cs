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
        public MainWindow()
        {
            InitializeComponent();
            ViewStartPage();
        }
        private void ViewStartPage()
        {
            if(this.startPage == null)
            {
                this.startPage= new StartPage();
                this.frameMain.Content= this.startPage;
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
    }