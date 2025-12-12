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
        public async Task <IActionResult> ViewFirstKit(string subjectId=null,string unitId = null)
        {
            ApiClient<F> client = new ApiClient<F>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetUnitDetails";
            if (subjectId != null)
            {
                client.AddParameter("subjectId", subjectId);
            }
            if(unitId != null)
            {
                client.AddParameter("unitId", unitId);
            }
            F f = await client.GetAsync();
            return View(f);
        }
        [HttpGet]
        public async Task<IActionResult> ViewUnitDetailes(unitId = null)
        {
            ApiClient<GetUnitDetails> client = new ApiClient<GetUnitDetails>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5239;
            client.Path = "api/Guest/GetUnitDetails";
        }
    }
}
