using FirstOrderKitModel;
using FirstOrderKitWS.ORM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;

namespace FirstOrderKitWS.Controllers
{//ATTRIBUTE
    //איך אנחנו פונים לWBSERVICE
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        RepositoryUOF repositoryUOF;
        public GuestController()
        {
            this.repositoryUOF = new RepositoryUOF();
        }

        [HttpGet]
        //ברירת מחדל null
        public OrderFirstKitViewModel GetFirstKit(string subjectId = null)
        {
            OrderFirstKitViewModel orderFirstKitViewModel = new OrderFirstKitViewModel();
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                orderFirstKitViewModel.Subject = this.repositoryUOF.SubjectRepository.GetAll();
                if (subjectId == null)
                {
                    orderFirstKitViewModel.Units = this.repositoryUOF.UnitRepository.GetAll();
                }
                else if (subjectId != null)
                {
                    orderFirstKitViewModel.Units = this.repositoryUOF.UnitRepository.FilterBySubject(subjectId);
                }
                return orderFirstKitViewModel;
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
        [HttpGet]
        public Unit GetUnitDetails(string unitId)
        {

            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return this.repositoryUOF.UnitRepository.GetById(unitId);

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
        public Test GetTest(string testId)
        {
            
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
              Test  test= this.repositoryUOF.TestRepository.GetById(testId);
                return test;
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
  
    }
}
