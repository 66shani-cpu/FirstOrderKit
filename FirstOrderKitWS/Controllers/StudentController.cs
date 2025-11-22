using FirstOrderKitWS.ORM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstOrderKitModel;

namespace FirstOrderKitWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        RepositoryUOF repositoryUOF;
        public StudentController()
        {
            this.repositoryUOF = new RepositoryUOF();
        }
        [HttpGet]
        //ברירת מחדל null
        public RequestANewTestViewModel GetNewTest ()
        {
            RequestANewTestViewModel requestANewTestViewModel = new RequestANewTestViewModel();
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();

                return requestANewTestViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
        //[HttpPost]
        //public LogIn()
        //{

        //}
    }
}
