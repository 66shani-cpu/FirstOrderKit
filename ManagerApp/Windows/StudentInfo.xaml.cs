using FirstKitWSClient;
using FirstOrderKitModel;
using Microsoft.AspNetCore.Mvc;
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
    /// Interaction logic for NewUnit.xaml
    /// </summary>
    public partial class StudentInfo : Window
    {
        Student student;
        string unitName;
        double averageGrade;
        public StudentInfo(string studentId)
        {
            
            InitializeComponent();
            _ = LoadAllData(studentId);
            //GetStudentInfo(studentId);

        }
        private async Task LoadAllData(string studentId)
        {
            //await GetStudentInfo(studentId);
            //await GetUnitName(studentId);
            try
            {
                await GetStudentInfo(studentId);
                await GetAverageGrade(studentId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "שגיאה");
            }
        }

        private async Task GetStudentInfo(string studentId)
        {
            ApiClient<Student> apiClient = new ApiClient<Student>();
            apiClient.Schema = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5239;
            apiClient.Path = "api/Manager/GetStudentInfo";
            apiClient.AddParameter("studentId", studentId);
            this.student = await apiClient.GetAsync();
            this.DataContext = this.student;
           // // 2. מביאים את שם היחידה בנפרד
           // ApiClient<string> unitApiClient = new ApiClient<string>();
           // unitApiClient.Schema = "http";
           // unitApiClient.Host = "localhost";
           // unitApiClient.Port = 5239;
           // unitApiClient.Path = "api/Manager/GetUnitNameByStudentId";
           // unitApiClient.AddParameter("studentId", studentId);
           //this.unitName = await unitApiClient.GetAsync();
           // this.textBlockUnitName.Text = this.unitName;
        }
        //[HttpGet]
        //private async Task GetUnitName(string studentId)
        //{
        //    ApiClient<string> unitApiClient = new ApiClient<string>();
        //    unitApiClient.Schema = "http";
        //    unitApiClient.Host = "localhost";
        //    unitApiClient.Port = 5239;
        //    unitApiClient.Path = "api/Manager/GetUnitNameByStudentId";
        //    unitApiClient.AddParameter("studentId", studentId);
        //    this.unitName = await unitApiClient.GetAsync();
        //    this.textBlockUnitName.Text = this.unitName;
        //}
        private async Task GetAverageGrade(string studentId)
        {
            ApiClient<double> averageApiClient = new ApiClient<double>();
            averageApiClient.Schema = "http";
            averageApiClient.Host = "localhost";
            averageApiClient.Port = 5239;
            averageApiClient.Path = "api/Manager/GetAverageGradeByStudentId";
            averageApiClient.AddParameter("studentId", studentId);
            this.averageGrade = await averageApiClient.GetAsync();
            this.textBlockAverageGrade.Text = this.averageGrade.ToString("0.00");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
