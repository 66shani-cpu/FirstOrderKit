using FirstKitWebApp.Models;
using FirstKitWSClient;
using FirstOrderKitModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        [HttpGet]
        public IActionResult ViewAbout()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ViewGradeForm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpDate(Student student)
        {
            // 1. כאן אנחנו מגדירים את ה-Client עם סוג הנתונים Student
            ApiClient<Student> client = new ApiClient<Student>();

            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Student/UpDate";

            // 2. עכשיו כשה-Client מבין מה זה Student, הוא יסכים לשלוח אותו ב-Post
            // (הערה: ה-ApiClient שלך כנראה מחזיר אובייקט Student או bool, תלוי איך בנית אותו)
            var result = await client.PostAsync(student);

            // 3. בדיקה אם העדכון הצליח (בהנחה ש-ok הוא בוליאני)
            if (result != null)
            {
                return RedirectToAction("ViewStudentCreateTest", "Student");
            }

            // אם נכשל, חוזרים לתצוגה עם הנתונים כדי שהמשתמש יתקן
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
            if (id == null)
                return View("ViewLoginForm");
            HttpContext.Session.SetString("studentId", id);
            ViewBag.StudentId = HttpContext.Session.GetString("studentId");
            // get sdata from WS
            return View("ViewStudentCreateTest");
        }
        [HttpGet]
        public  IActionResult GetTest(/*string testId*/)
        {
            //ApiClient<Test> client = new ApiClient<Test>();
            //client.Schema = "http";
            //client.Host = "localhost";
            //client.Port = 5239;
            //client.Path = "api/Guest/GetTest";
            //client.AddParameter("testId", testId);
            //Test test = await client.GetAsync();
            return View(/*test*/);
        }
        [HttpGet]
        public async Task<IActionResult> ViewRequestNewTestForm()
        {
            ApiClient<RequestNewTest> client = new ApiClient<RequestNewTest>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Student/GetUD";
            RequestNewTest requestNewTest = await client.GetAsync();
            return View(requestNewTest);
        }

        [HttpGet]
        public async Task<IActionResult> ViewStudentCreateTest()
        {
            ApiClient<Student> client = new ApiClient<Student>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Student/GetStudent";
            client.AddParameter("studentId", HttpContext.Session.GetString("studentId"));
            Student student = await client.GetAsync();
            if(student == null)
            {
                ViewBag.ImageExt = "1.jpg";
            }
            ViewBag.ImageExt = student.StudentImage;
            ViewBag.StudentId = HttpContext.Session.GetString("studentId");
            return View();
        }
        //פעולה שתפקידה להציג את הטופס של מילוי פרטים  של נושא ורמת קושי למבחן חדש 
        //כדי להציג טופס זה אני צריכה אובייקט של request new test שאותו אני צריכה לקבל מWeb Service ולהעביר את זה לView
        [HttpPost]
        public async Task<IActionResult> SaveTest(TestAnswer testAnswer)
        {
            int sum = 0;
            if (testAnswer != null)
            {
                sum = testAnswer.answer1 == "True" ? sum += 10 : sum;
                sum = testAnswer.answer2 == "True" ? sum += 10 : sum;
                sum = testAnswer.answer3 == "True" ? sum += 10 : sum;
                sum = testAnswer.answer4 == "True" ? sum += 10 : sum;
                sum = testAnswer.answer5 == "True" ? sum += 10 : sum;
                sum = testAnswer.answer6 == "True" ? sum += 10 : sum;
                sum = testAnswer.answer7 == "True" ? sum += 10 : sum;
                sum = testAnswer.answer8 == "True" ? sum += 10 : sum;
                sum = testAnswer.answer9 == "True" ? sum += 10 : sum;
                sum = testAnswer.answer10 == "True" ? sum += 10 : sum;
            }
            //ליצור מודל שאותו צריך לשלוח 
            Test test = new Test();
            test.TestId = "0";
            //test.TestName = $"{testAnswer.unitId}-{testAnswer.levelQuestion}-" + DateTime.UtcNow.ToString();
            test.TestName = $"{testAnswer.unitId}-{testAnswer.levelQuestion}-{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
            test.UnitId = testAnswer.unitId;
            test.StudentId = HttpContext.Session.GetString("studentId");
            test.LevelQuestion = testAnswer.levelQuestion;
            test.Grade = sum.ToString();
            ApiClient<Test> client = new ApiClient<Test>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Student/SaveTest";
           bool ok = await client.PostAsync(test);
            if (ok==true)
            {
                return View("ViewGradeForm", sum);
            }
            return View("ViewGradeForm", sum);
        }

        [HttpGet]
        public async Task<IActionResult> NewTestForm(string unitId, string difficulty)
        {
            ApiClient<List<TestQuestionViewModel>> client = new ApiClient<List<TestQuestionViewModel>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Student/GetNewTest";
            client.AddParameter("unitId", unitId);
            client.AddParameter("difficulty", difficulty);
            ViewBag.UnitId = unitId;
            ViewBag.Difficulty = difficulty;
            List<TestQuestionViewModel> testQuestionViewModel = await client.GetAsync();
            return View(testQuestionViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetTestHistoryById()
        {
            ApiClient<List<Test>> client = new ApiClient<List<Test>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Student/GetTestByStudent";
            string studentId = HttpContext.Session.GetString("studentId");
            client.AddParameter("studentId", studentId);
            List<Test> testHistory = await client.GetAsync();
            return View("TestHistory", testHistory);
        }

        
        
    }
}
