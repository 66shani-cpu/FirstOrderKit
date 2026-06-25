using FirstKitWSClient;
using FirstOrderKitModel;
using ManagerApp.Windows;
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
    /// Interaction logic for Unit_SubjectManagement.xaml
    /// </summary>
    public partial class Unit_SubjectManagement : UserControl
    {
        List<Unit> units;
        public Unit_SubjectManagement()
        {
            InitializeComponent();
            GetUnitList();
        }
        //אסינכרוני כדי שיהיה מהיר
        private async Task GetUnitList()
        {
           ApiClient<List<Unit>> apiClient = new ApiClient<List<Unit>>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port =5239;
            apiClient.Path = "api/Manager/GetListUnit";
            this.units=await apiClient.GetAsync();
            ListViewUnit.ItemsSource = this.units;
            this.DataContext= this.units;
        }

        //private void btnInfo_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private async void buttonAddNewUnit_Click(object sender, RoutedEventArgs e)
        {
            NewUnit newUnitWin = new NewUnit();
            bool? ok = newUnitWin.ShowDialog();
            if (ok == true)
            {
                ListViewUnit.ItemsSource = null;
                await GetUnitList();
            }
        }
        private async void btnViewInactiveUnits_Click(object sender, RoutedEventArgs e)
        {
            InactiveUnitsWindow archiveWindow = new InactiveUnitsWindow();
            archiveWindow.Owner = Window.GetWindow(this);
            archiveWindow.ShowDialog();

            // רענון הרשימה הראשית לאחר סגירת הארכיון
            await GetUnitList();
        }
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            FrameworkElement btnDelete = sender as FrameworkElement;
            if (btnDelete != null)
            {
                FirstOrderKitModel.Unit selectedQuestion = (FirstOrderKitModel.Unit)clickedButton.DataContext;
                string unitId = selectedQuestion.UnitId;
                try
                {
                    ApiClient<bool> apiClient = new ApiClient<bool>();
                    apiClient.Schema = "http";
                    apiClient.Host = "localhost";
                    apiClient.Port = 5239;
                    apiClient.Path = "api/Manager/DeleteUnit";
                    apiClient.AddParameter("unitId", unitId);
                    bool isSuccess = await apiClient.GetAsync();
                    if (isSuccess)
                    {
                        MessageBox.Show("היחידה עודכנה כלא פעילה בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                        await GetUnitList();
                    }
                    else
                    {
                        MessageBox.Show("הפעולה נכשלה בשרת.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("שגיאה בתקשורת עם השרת: " + ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
    }
}
