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
        //כל פעם מידע שונה פעם ראשונה מציג הכל אחרי שבחרו התז משתנה וצריך לשים פרמטק
        public async Task<IActionResult> ViewFirstKit(string? subjectId = null, string? unitId = null)
        {
            ApiClient<OrderFirstKitViewModel> client = new ApiClient<OrderFirstKitViewModel>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetFirstKit";
            if (subjectId != null)
            {
                client.AddParameter("subjectId", subjectId);
            }
            if (unitId != null)
            {
                client.AddParameter("unitId", unitId);
            }
            OrderFirstKitViewModel orderFirstKitViewModel = await client.GetAsync();
            return View(orderFirstKitViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetUnitDetails(string unitId)
        {
            ApiClient<Unit> client = new ApiClient<Unit>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetUnitDetails";
            client.AddParameter("unitId", unitId);
            Unit unit = await client.GetAsync();
            return View(unit);
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
        [HttpPost]
        public async Task<IActionResult> InsertStudent(Student student
)
        {
            ApiClient<Student> client = new ApiClient<Student>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/InsertStudent";
           bool ok = await client.PostAsync(student);
            return View(student);
        }






    }
}
