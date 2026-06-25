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

namespace ManagerApp.Windows
{
    /// <summary>
    /// Interaction logic for InactiveUnitsWindow.xaml
    /// </summary>
    public partial class InactiveUnitsWindow : Window
    {
        public InactiveUnitsWindow()
        {
            InitializeComponent();
            GetInactiveUnitList();
        }
        private async Task GetInactiveUnitList()
        {
            ApiClient<List<FirstOrderKitModel.Unit>> apiClient = new ApiClient<List<FirstOrderKitModel.Unit>>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/GetListInactiveUnit"; // נתיב API חדש

            var result = await apiClient.GetAsync();
            ListViewInactiveUnits.ItemsSource = result;
        }

        private async void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            FirstOrderKitModel.Unit selectedUnit = (FirstOrderKitModel.Unit)((Button)sender).DataContext;

            ApiClient<bool> apiClient = new ApiClient<bool>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/RestoreUnit"; // פונקציה חדשה בשרת
            apiClient.AddParameter("unitId", selectedUnit.UnitId);

            if (await apiClient.GetAsync())
            {
                MessageBox.Show("היחידה שוחזרה בהצלחה!");
                await GetInactiveUnitList(); // רענון הארכיון
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // סוגר את חלון הארכיון הנוכחי
        }
    }
}
