using FirstKitWebApp.Models;
using FirstKitWSClient;
using FirstOrderKitModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;

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
        [HttpGet]
        public IActionResult ViewHorocope()
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
        public async Task<IActionResult> LogInStudent(/*string StudentNickName, string password*/ Student student)
        {
            // אומרים לשרת להתעלם מכל שאר שדות החובה של הסטודנט שלא נמצאים בטופס ההתחברות
            ModelState.Remove(nameof(student.StudentId));
            ModelState.Remove(nameof(student.UnitId));
            ModelState.Remove(nameof(student.StudentFirstName));
            ModelState.Remove(nameof(student.StudentLastName));
            ModelState.Remove(nameof(student.CityId));
            ModelState.Remove(nameof(student.StudentTelephone));
            ModelState.Remove(nameof(student.StudentAdrres));
            ModelState.Remove(nameof(student.StudentImage));
            ModelState.Remove(nameof(student.StudentSalt));
            ModelState.Remove(nameof(student.CityName));
            if (!ModelState.IsValid)
            {
                return View("ViewLoginForm", student);
            }
        //    if (string.IsNullOrEmpty(StudentNickName) || StudentNickName.Length < 2 ||
        //string.IsNullOrEmpty(password) || password.Length < 2)
        //    {
        //        ViewBag.ErrorMessage = "Please enter a valid Nickname and Password (at least 2 characters).";
        //        return View("ViewLoginForm");
        //    }
            ApiClient<string> client = new ApiClient<string>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Student/LogInStudent";
            client.AddParameter("nickName", student.StudentNickName);
            client.AddParameter("password", student.Password/*password*/);
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

        public async Task<IActionResult> ViewHoroscope(string sign, string start)
        {
            // בדיקה: אם המשתמש עדיין לא בחר ערכים, נביא לו את תאריך היום ומזל טלה כברירת מחדל
            if (string.IsNullOrEmpty(sign)) { sign = "aries"; }
            if (string.IsNullOrEmpty(start)) { start = DateTime.Now.ToString("yyyy-MM-dd"); }

            // נשמור את הבחירה הנוכחית של המשתמש כדי להציג אותה חזרה בטופס
            ViewBag.SelectedSign = sign;
            ViewBag.SelectedStart = start;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://horoscope-api-by-apirobots.p.rapidapi.com/v1/horoscopes/{sign}/weekly?week_start={start}"),
                Headers =
        {
            { "x-rapidapi-key", "e2cae94d3fmsh9e56b39a12d6725p117355jsn2d4f39a51cde" },
            { "x-rapidapi-host", "horoscope-api-by-apirobots.p.rapidapi.com" },
        },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    Horskope horskopeData = JsonSerializer.Deserialize<Horskope>(body);

                    ViewBag.HoroscopeOverview = horskopeData?.overview;
                }
            }
            catch (Exception ex)
            {
                ViewBag.HoroscopeOverview = "Could not fetch horoscope data. Please check your connection or try a different date.";
                ViewBag.HoroscopeOverview = "Error Detail: " + ex.Message;
            }

            return View();
        }

        public class Horskope
        {
            public string week_start { get; set; }
            public string week_end { get; set; }
            public string sign { get; set; }
            public string overview { get; set; }
        }
    }
}
