using FirstKitWSClient;
using FirstOrderKitModel.ViewModel;
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
    /// Interaction logic for UnitReportUserControl.xaml
    /// </summary>
    public partial class UnitReportUserControl : UserControl
    {
        UnitBarData unitBarData;
        public UnitReportUserControl()
        {
            InitializeComponent();
            GetUnitData();
        }
        private async void GetUnitData()
        {
            ApiClient<UnitBarData> apiClient =
               new ApiClient<UnitBarData>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/GetUnitBarData";
            this.unitBarData = await apiClient.GetAsync();

            this.DataContext = this.unitBarData;
        }
    }
}
