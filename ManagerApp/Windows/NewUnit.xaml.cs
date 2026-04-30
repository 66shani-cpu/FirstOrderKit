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
using System.Windows.Shapes;
using Microsoft.Win32;

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
            //OpenFileDialog openFileDialog = new OpenFileDialog();

            //// הגדרת פילטר לסוגי תמונות
            //openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            //if (openFileDialog.ShowDialog() == true)
            //{
            //    selectedImagePath = openFileDialog.FileName; // הנתיב המלא של הקובץ
            //    labelSelectedFileName.Content = openFileDialog.SafeFileName; // רק שם הקובץ לתצוגה
            //}
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Only(*.jpg, *.png, *.gif)|*.jpg; *.png; *.gif";
            bool? dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == true)
            {
                string fileName = openFileDialog.FileName;
                imagePath = fileName;
                Uri uri = new Uri(fileName);
                BitmapImage bitmapImage = new BitmapImage(uri);
                imageBook.Source = bitmapImage;
                this.textBlockSelectImage.Visibility = Visibility.Hidden;
            }
        }
        private void buttonAddUnit_Click(object sender, RoutedEventArgs e)
        {
            string unitName = TextBoxQuestionText.Text;

            if (string.IsNullOrEmpty(unitName) || string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("נא להזין שם ולבחור תמונה");
                return;
            }

            // כאן נכנס הקוד של מסד הנתונים שלך (SQL/Entity Framework)
            // דוגמה רעיונית:
            SaveUnitToDatabase(unitName, selectedImagePath);

            MessageBox.Show("היחידה נוספה בהצלחה!");
        }




        private void TextBoxQuestionText_TextChanged(object sender, TextChangedEventArgs e)
        {
            // הקוד שירוץ כשהטקסט משתנה (אפשר להשאיר ריק כרגע)
        }

        private void buttonAddUnit_Click(object sender, RoutedEventArgs e)
        {
            // הקוד שירוץ כשלוחצים על "הוסף יחידה"
        }


        private void buttonClose_Click_1(object sender, RoutedEventArgs e)
        {
             this.DialogResult = true;
            this.Close();
        }
    }
}
