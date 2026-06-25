using Microsoft.AspNetCore.Mvc;
using FirstKitWSClient;
using FirstOrderKitModel;

namespace FirstKitWebApp.Controllers
{
    public class GuestController : Controller
    {
        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SampleTests()
        {
            return View();
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
        public IActionResult Logout()
        {
            // 1. מחיקת המידע מה-Session כדי שהמערכת תדע שהסטודנט התנתק
            HttpContext.Session.Remove("studentId");
            HttpContext.Session.Clear(); // לביטחון נוסף, מנקה הכל

            // 2. הפניה מסודרת לפעולת דף הבית של האורח
            return RedirectToAction("ShowUnitsPage");
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Student student, IFormFile formFile)
        {
            RegistationViewModel registationViewModel = new RegistationViewModel();
            if (ModelState.IsValid == false)
            {
                registationViewModel.cities = await GetCitiesAsync();
                registationViewModel.units = await GetUnitsAsync();
                registationViewModel.student = student;
                return View("SignUp", registationViewModel);
            }
            ApiClient<Student> clientStudent = new ApiClient<Student>();
            clientStudent.Schema = "http";
            clientStudent.Host = "localhost";
            clientStudent.Port = 5239;
            clientStudent.Path = "api/Guest/InsertStudent";
            bool ok = await clientStudent.PostAsync(student, formFile.OpenReadStream());
            if (ok == true)
            {
                HttpContext.Session.SetString("studentId", student.StudentId);
                //מעבר לקונטרולר אחר
                return RedirectToAction("ViewStudentCreateTest", "Student");
            }
            else
            {
                ViewBag.Error = true;
                return View("ViewRegistarion", registationViewModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            string studentId = HttpContext.Session.GetString("studentId");
            ApiClient<RegistationViewModel> clientStudent = new ApiClient<RegistationViewModel>();
            clientStudent.Schema = "http";
            clientStudent.Host = "localhost";
            clientStudent.Port = 5239;
            clientStudent.Path = "api/Student/GetRegistationViewModel";
            if(studentId!=null)
                clientStudent.AddParameter("studentId", studentId);
            RegistationViewModel registationViewModel = await clientStudent.GetAsync();
            return View(registationViewModel);
        }

        [HttpGet]
        private async Task<List<Unit>> GetUnitsAsync()
        {
            ApiClient<List<Unit>> client = new ApiClient<List<Unit>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetUnits";
            return await client.GetAsync();
        }

        [HttpGet]
        private async Task<List<City>> GetCitiesAsync()
        {
            ApiClient<List<City>> client = new ApiClient<List<City>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetCities";
            return await client.GetAsync();
        }
        public async Task<List<Unit>> ViewFirstKit()
        {
            ApiClient<List<Unit>> client = new ApiClient<List<Unit>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetUnits";
            //// קריאה לפעולה שמביאה את הנתונים מה-API
            //List<Unit> unitsList = await ViewFirstKit();

            
            return await client.GetAsync();
        }
        [HttpGet]
        public async Task<IActionResult> ShowUnitsPage()
        {
            // קוראים לפעולה הראשונה ומחכים לרשימה
            List<Unit> unitsList = await ViewFirstKit();
            if (unitsList == null)
            {
                Console.WriteLine("--- האזהרה שלי: הרשימה חזרה ריקה מה-API! ---");
                unitsList = new List<Unit>(); // יוצר רשימה ריקה כדי שלא תהיה שגיאת Null
            }

            // מחזירים את ה-View ומעבירים לו את הרשימה
            return View("ViewFirstKit", unitsList);
        }
    }
    
}

