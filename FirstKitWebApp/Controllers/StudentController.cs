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
        public async Task<IActionResult> UpDate(string StudentId,
            string UnitId, string StudentNickName, string passwword,
            string StudentFirstName, string StudentLastName, string CityId,
            string StudentTelephone, string StudentAdrres, string StudentImage)
        {
            ApiClient<Student> client = new ApiClient<Student>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/UpDate";
            client.AddParameter("StudentId", StudentId);
            client.AddParameter("UnitId", UnitId);
            client.AddParameter("StudentNickName", StudentNickName);
            client.AddParameter("passwword", passwword);
            client.AddParameter("StudentFirstName", StudentFirstName);
            client.AddParameter("StudentLastName", StudentLastName);
            client.AddParameter("CityId", CityId);
            client.AddParameter("StudentTelephone", StudentTelephone);
            client.AddParameter("StudentAdrres", StudentAdrres);
            client.AddParameter("StudentImage", StudentImage);
            Student student = await client.GetAsync();
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> LogInStusent(string StudentNickName, string passwword)
        {
            ApiClient<Student> client = new ApiClient<Student>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/LogInStusent";
            client.AddParameter("StudentNickName", StudentNickName);
            client.AddParameter("passwword", passwword);
            Student student = await client.GetAsync();
            return View(student);
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
        //לשאול מה עושים עם זה
        public async Task<IActionResult> GetNewTest()
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


    }
}
