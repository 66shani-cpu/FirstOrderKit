using FirstKitWSClient;
//using FirstKitWSClient;
using FirstOrderKitModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
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
            string password = this.textBoxPassword.Password;
            //if (String.IsNullOrEmpty(nickName) || String.IsNullOrEmpty(password))
            //{
            //    MessageBox.Show("מילוי נתונים", "כותרת", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            // יוצרים מופע של Student רק כדי לבדוק תקינות NickName ו-Password
            FirstOrderKitModel.Student studentToValidate = new FirstOrderKitModel.Student();
            studentToValidate.StudentNickName = nickName;
            studentToValidate.Password = password;

            // הרצת בדיקת תקינות לכל המודל
            studentToValidate.Validate();
            // 4. בדיקה האם נמצאו שגיאות
            Dictionary<string, List<string>> errors = studentToValidate.AllError();
            // מתעלמים מכל שדה שלא קשור לטופס ההתחברות
            errors.Remove(nameof(studentToValidate.StudentId));
            errors.Remove(nameof(studentToValidate.UnitId));
            errors.Remove(nameof(studentToValidate.StudentFirstName));
            errors.Remove(nameof(studentToValidate.StudentLastName));
            errors.Remove(nameof(studentToValidate.CityId));
            errors.Remove(nameof(studentToValidate.StudentTelephone));
            errors.Remove(nameof(studentToValidate.StudentAdrres));
            errors.Remove(nameof(studentToValidate.StudentImage));
            errors.Remove(nameof(studentToValidate.StudentSalt));
            errors.Remove(nameof(studentToValidate.CityName));
            if (errors.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Please correct the following errors:\n");

                foreach (var errorEntry in errors)
                {
                    // שם השדה (למשל: UnitName)
                    string propertyName = errorEntry.Key;
                    // חיבור כל השגיאות של השדה למחרוזת אחת
                    string errorMessage = string.Join(", ", errorEntry.Value);

                    sb.AppendLine($"• {propertyName}: {errorMessage}");
                }

                // הצגת הודעת השגיאה למשתמש
                MessageBox.Show(sb.ToString(), "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return; // חשוב! עוצרים כאן ולא ממשיכים לשלוח לשרת
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
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("The log in was not successful - User not found or incorrect credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // עוצרים כאן ולא נותנים לו להיכנס!
            }


            MessageBox.Show("The log in was successful", "The log in was successful", MessageBoxButton.OK,MessageBoxImage.Information);
                MainWindow mw = Application.Current.MainWindow as MainWindow;
                mw.SetAdmin(true);
        }

       
    }
}
