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
using FirstOrderKitModel;
using FirstKitWSClient;


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

    }
}
