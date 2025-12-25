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
        public async Task<IActionResult> ViewFirstKit(string subjectId = null, string unitId = null)
        {
            ApiClient<OrderFirstKitViewModel> client = new ApiClient<OrderFirstKitViewModel>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/OrderFirstKitViewModel";
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
        public async Task<IActionResult> InsertStudent(string StudentId,
            string UnitId, string StudentNickName,string passwword,
            string StudentFirstName, string StudentLastName, string CityId,
            string StudentTelephone, string StudentAdrres, string StudentImage)
        {
            ApiClient<Student> client = new ApiClient<Student>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/InsertStudent";
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






    }
}
