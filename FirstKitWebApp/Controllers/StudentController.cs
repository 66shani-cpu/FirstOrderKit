using FirstKitWSClient;
using FirstOrderKitModel;
using Microsoft.AspNetCore.Mvc;

namespace FirstKitWebApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
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
        [HttpGet]
        public async Task<IActionResult> GetTest(string testId)
        {
            ApiClient<Test> client = new ApiClient<Test>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetTest";
            client.AddParameter("testId", testId);
            Test test = await client.GetAsync();
            return View(test);
        }
        [HttpGet]
        public async Task<IActionResult> GetNewTest()
        {
            ApiClient<List<TestQuestionViewModel>> client = new ApiClient<List<TestQuestionViewModel>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetTest";
            List<TestQuestionViewModel> test = await client.GetAsync();
            return View(test);
        }


    }
}
