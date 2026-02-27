using FirstKitWSClient;
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
using FirstKitWSClient;

namespace ManagerApp.UserControls
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : UserControl
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void buttonLoin_Click(object sender, RoutedEventArgs e)
        {
            string nickName=this.textBoxNickName.Text;
            string password = this.textBoxPassword.Text;
            // ApiClient<string> apiClient
            //פניה לWS בקשה
            string id = "1234";
            if(id!=null)
            {
                MainWindow mw = Application.Current.MainWindow as MainWindow;
                mw.SetAdmin(true);
            }
            else
            {
                //הודעת שגיאה שלא הצליח
            }
        }
    }
}
