using FirstKitWSClient;
using FirstOrderKitModel;
using Microsoft.AspNetCore.Http.HttpResults;
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
using System.Windows.Shapes;

namespace ManagerApp.Windows
{
    /// <summary>
    /// Interaction logic for NewUnit.xaml
    /// </summary>
    public partial class NewUnit : Window
    {
        string imagePath;
        public NewUnit()
        {
            InitializeComponent();
        }
     
private string selectedImagePath = ""; // משתנה שיחזיק את הנתיב

    private void buttonSelectImage_Click(object sender, RoutedEventArgs e)
    {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Only(*.jpg, *.png, *.gif)|*.jpg; *.png; *.gif";
            bool? dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == true)
            {
                string fileName = openFileDialog.FileName;
                imagePath = fileName;
                Uri uri = new Uri(fileName);
                BitmapImage bitmapImage = new BitmapImage(uri);
              
                this.labelSelectedFileName.Visibility = Visibility.Hidden;
            }
        }
        private async void buttonAddUnit_Click(object sender, RoutedEventArgs e)
        {

            string unitName = TextBoxQuestionText.Text;
           
            FirstOrderKitModel.Unit unit = new FirstOrderKitModel.Unit();
            unit.UnitId = "0";
            unit.UnitName= unitName;
            unit.UnitPicture = System.IO.Path.GetExtension(imagePath) ;

            // 3. הרצת בדיקת תקינות
            unit.Validate();

            // 4. בדיקה האם נמצאו שגיאות
            Dictionary<string, List<string>> errors = unit.AllError();
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
            // כאן נכנס הקוד של מסד הנתונים שלך (SQL/Entity Framework)
            ApiClient<Unit> apiClient = new ApiClient<Unit>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/AddNewUnit";
            Stream stream = new FileStream(this.imagePath,
                                            FileMode.Open,
                                            FileAccess.Read);
           bool ok= await apiClient.PostAsync(unit, stream);
            if (ok)
            {
                MessageBox.Show("היחידה נוספה בהצלחה!");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("הוספת יחידה נכשלה");
            }
        }


        private void TextBoxQuestionText_TextChanged(object sender, TextChangedEventArgs e)
        {
            // הקוד שירוץ כשהטקסט משתנה (אפשר להשאיר ריק כרגע)
        }

        //private void buttonAddUnit_Click(object sender, RoutedEventArgs e)
        //{
        //    // הקוד שירוץ כשלוחצים על "הוסף יחידה"
        //}


        private void buttonClose_Click_1(object sender, RoutedEventArgs e)
        {
             this.DialogResult = true;
            this.Close();
        }
    }
}
