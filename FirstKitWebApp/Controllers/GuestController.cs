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
        //[HttpGet]
        //כל פעם מידע שונה פעם ראשונה מציג הכל אחרי שבחרו התז משתנה וצריך לשים פרמטק
        //public async Task<IActionResult> ViewFirstKit(string? subjectId = null, string? unitId = null)
        //{
        //    ApiClient<OrderFirstKitViewModel> client = new ApiClient<OrderFirstKitViewModel>();
        //    client.Schema = "http";
        //    client.Host = "localhost";
        //    client.Port = 5239;
        //    client.Path = "api/Guest/GetFirstKit";
        //    if (subjectId != null)
        //    {
        //        client.AddParameter("subjectId", subjectId);
        //    }
        //    if (unitId != null)
        //    {
        //        client.AddParameter("unitId", unitId);
        //    }
        //    OrderFirstKitViewModel orderFirstKitViewModel = await client.GetAsync();
        //    return View(orderFirstKitViewModel);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetUnitDetails(string unitId)
        //{
        //    ApiClient<Unit> client = new ApiClient<Unit>();
        //    client.Schema = "http";
        //    client.Host = "localhost";
        //    client.Port = 5239;
        //    client.Path = "api/Guest/GetUnitDetails";
        //    client.AddParameter("unitId", unitId);
        //    Unit unit = await client.GetAsync();
        //    return View(unit);
        //}
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
        [HttpPost]
        //public async Task<IActionResult> InsertStudent(Student student)
        //{
        //    ApiClient<Student> client = new ApiClient<Student>();
        //    client.Schema = "http";
        //    client.Host = "localhost";
        //    client.Port = 5239;
        //    client.Path = "api/Guest/InsertStudent";
        //    bool ok = await client.PostAsync(student);
        //    return RedirectToAction("HomePage");
        //}
        public async Task<IActionResult> Registration(Student student, IFormFile formFile)
        {
            RegistationViewModel registationViewModel = new RegistationViewModel();
            if (ModelState.IsValid == false)
            {
                registationViewModel.cities = await GetCitiesAsync();
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

            //    registationViewModel.student = student;
            //ApiClient<List<City>> client = new ApiClient<List<City>>();
            //client.Schema = "http";
            //client.Host = "localhost";
            //client.Port = 5239;
            //client.Path = "api/Guest/GetCities";
            //registationViewModel.cities = await client.GetAsync();
            //ViewBag.Error = true;
            //return View("ViewRegistarion", registationViewModel);
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
        public async Task<IActionResult> ShowUnitsPage() // השם של ה-Action שקורא ל-View
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

