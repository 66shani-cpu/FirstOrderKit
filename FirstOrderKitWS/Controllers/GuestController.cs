using FirstOrderKitModel;
using FirstOrderKitWS.ORM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings;
using System.Text.Json;

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
                Console.WriteLine(ex.ToString());
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
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
        [HttpGet]
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
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }

        }
        [HttpGet]
        public List<City> GetCities()
        {
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
               return this.repositoryUOF.CityRepository.GetAll();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }

        }
        [HttpPost]
        public bool InsertStudent ()
        {
            string json = HttpContext.Request.Form["model"];
            Student student = JsonSerializer.Deserialize<Student>(json);
            IFormFile file = null;
            if(HttpContext.Request.Form.Files.Count>0)
            {
                file=HttpContext.Request.Form.Files[0];
            }
            try
            {
                repositoryUOF.DBHelperOledb.OpenTransaction();
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                 bool ok= this.repositoryUOF.StudentRepository.Create(student);
                string path = $@"{Directory.GetCurrentDirectory()}\wwwroot\Images\Students\{student.StudentId}.{student.StudentImage}";
                 using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    fileStream.CopyTo(fileStream);
                }
                 this.repositoryUOF.DBHelperOledb.Commit();
                  return true;
            }
            catch (Exception ex)
            {
                this.repositoryUOF.DBHelperOledb.Rollback();
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }

        }
    }
}
