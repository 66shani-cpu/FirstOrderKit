using FirstOrderKitModel;
using FirstOrderKitWS.ORM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reactive;
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
        public OrderFirstKitViewModel GetFirstKit()
        {
            OrderFirstKitViewModel orderFirstKitViewModel = new OrderFirstKitViewModel();
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                orderFirstKitViewModel.Subject = this.repositoryUOF.SubjectRepository.GetAll();
                orderFirstKitViewModel.Units = this.repositoryUOF.UnitRepository.GetAll();
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
        public FirstOrderKitModel.Unit GetUnitDetails(string unitId)
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
            //string json = HttpContext.Request.Form["model"];
            string json = Request.Form["model"].ToString();
            Student student = JsonSerializer.Deserialize<Student>(json);
            IFormFile image = null;
            if (Request.Form.Files.Count > 0)
            {
                image = Request.Form.Files[0]; // לוקח את הקובץ הראשון שנשלח מהטופס
            }
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                repositoryUOF.DBHelperOledb.OpenTransaction();              
                 bool ok= this.repositoryUOF.StudentRepository.Create(student);
                string fileName = $"{student.StudentId}{student.StudentImage}";
                this.repositoryUOF.StudentRepository.UpdateImageName(student.StudentId, fileName);
                //string path = $@"{Directory.GetCurrentDirectory()}\wwwroot\Images\Students\{student.StudentId}.{student.StudentImage}";
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                                           "wwwroot",
                                           "Images",
                                           "Students",
                                           fileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                     image.CopyTo(stream);
                }
                this.repositoryUOF.DBHelperOledb.Commit();
                  return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.repositoryUOF.DBHelperOledb.Rollback();
                return false;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }

        }
    }
}
