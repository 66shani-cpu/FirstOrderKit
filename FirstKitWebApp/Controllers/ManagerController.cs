using FirstKitWSClient;
using FirstOrderKitModel;
using FirstOrderKitModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FirstKitWebApp.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogInStusent(string StudentNickName, string passwword)
        {
            ApiClient<string> client = new ApiClient<string>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/LogInStusent";
            client.AddParameter("StudentNickName", StudentNickName);
            client.AddParameter("passwword", passwword);
            string id = await client.GetAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpDate(Student student)
        {
            ApiClient<bool> client = new ApiClient<bool>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/UpDate";
            bool ok = await client.GetAsync();
            return View(student);
        }
        [HttpGet]
        public async Task<IActionResult> Message(string messageId)
        {
            ApiClient<Message> client = new ApiClient<Message>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/Message";
            client.AddParameter("messageId", messageId);
            Message message = await client.GetAsync();
            return View(message);
        }
        [HttpGet]
        public async Task<IActionResult> GetReportsAVG()
        {
            ApiClient<double> client = new ApiClient<double>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetReportsAVG";
            double avg = await client.GetAsync();
            return View(avg);
        }
        [HttpGet]
        public async Task<IActionResult> GetReportsPast()
        {
            ApiClient<List<ReportsViewModel>> client = new ApiClient<List<ReportsViewModel>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetReportsPast";
            //client.AddParameter("messageId", messageId);
            List<ReportsViewModel> past = await client.GetAsync();
            return View(past);
        }

    }
}
