using FirstKitWSClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private async void buttonLoin_Click(object sender, RoutedEventArgs e)
        {
            string nickName=this.textBoxNickName.Text;
            string password = this.textBoxPassword.Text;
            if(String.IsNullOrEmpty(nickName) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("מילוי נתונים", "כותרת", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //פניה לWS בקשה
            ApiClient<string> apiClient = new ApiClient<string>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/LogInStudent";
            apiClient.AddParameter("nickName", nickName);
            apiClient.AddParameter("password", password);
            string id = "";
            try
            {
                id = await apiClient.GetAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The log in was not successful", "The log in was not successful", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           // string id = "1234";
           
                MessageBox.Show("The log in was successful", "The log in was successful", MessageBoxButton.OK,MessageBoxImage.Information);
                MainWindow mw = Application.Current.MainWindow as MainWindow;
                mw.SetAdmin(true);
        }

       
    }
}
