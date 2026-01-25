using FirstKitWSClient;
using FirstOrderKitModel;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace FirstKitWebApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewLoginForm()
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
        public async Task<IActionResult> LogInStudent(string StudentNickName, string password)
        {
            ApiClient<string> client = new ApiClient<string>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Student/LogInStudent";
            client.AddParameter("nickName", StudentNickName);
            client.AddParameter("password", password);
            string id = await client.GetAsync();
            HttpContext.Session.SetString("studentId", id);
            ViewBag.StudentId = HttpContext.Session.GetString("studentId");
            // get sdata from WS
            return View("ViewStudentCreateTest");
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
        public async Task<IActionResult> GetNewTestForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetNewTest(RequestNewTest requestNewTest)
        {
            ApiClient<Test> client = new ApiClient<Test>();
            client.Schema = "http";
            client.Host difficulty= "localhost";
            client.Port = 5239;
            client.Path = "api/Student/GetNewTest";
            client.AddParameter("subjectId",requestNewTest.SubjectId);
            client.AddParameter("", requestNewTest.Difficulty);
            Test test = await client.GetAsync();

            return View(test);
        }


        [HttpGet]
        public async Task<IActionResult> ViewStudentCreateTest()
        {
            ViewBag.StudentId = HttpContext.Session.GetString("studentId");
            return View();
        }

       
      
    }
}
