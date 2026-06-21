using FirstOrderKitModel;
using FirstOrderKitModel.ViewModel;
using FirstOrderKitWS.ORM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FirstOrderKitWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        RepositoryUOF repositoryUOF;
        public ManagerController()
        {
            this.repositoryUOF = new RepositoryUOF();
        }
        [HttpPost]
        //ברירת מחדל null
        public bool AddQuestion(AddQuestionViewModel addQuestionViewModel)
        {
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                bool ok = this.repositoryUOF.QuestionRepository.Create(addQuestionViewModel.Question);
                foreach (Answer answer in addQuestionViewModel.Answers)
                {
                    ok = this.repositoryUOF.AnswerRepository.Create(answer);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
        [HttpPost]
        public async Task<bool> AddNewQuestion([FromBody] AddQuestionViewModel addQuestionViewModel)
        {
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                this.repositoryUOF.BeginTransaction();
               bool ok= this.repositoryUOF.QuestionRepository.Create(addQuestionViewModel.Question);
                string id = this.repositoryUOF.GetLastInsertId();
                foreach(Answer answer in addQuestionViewModel.Answers)
                {
                    this.repositoryUOF.AnswerRepository.Create(id,answer.AnswerText,answer.TrueFalse);
                }         
                this.repositoryUOF.Commit();
                return true;

            }
            catch
            {
                this.repositoryUOF.RollBack();
                return false;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
 
        [HttpGet]
        [Produces("application/json")]// כופה על הפעולה להחזיר JSON 
        public string LogInStudent(string nickName, string password)
        {
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return repositoryUOF.StudentRepository.LogInManager(nickName, password);

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
        public List<Unit> GetListUnit()
        {
            try
            {

                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return this.repositoryUOF.UnitRepository.GetAll();
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
        public List<Student> GetListStudent()
        {
            try
            {

                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return this.repositoryUOF.StudentRepository.GetAll();
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
        public List<Question> GetListQuestion()
        {
            try
            {

                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return this.repositoryUOF.QuestionRepository.GetAll();
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
        public Student GetStudentInfo(string studentId)
        {
            try
            {

                this.repositoryUOF.DBHelperOledb.OpenConnection();
               Student student= this.repositoryUOF.StudentRepository.GetById(studentId);
                City city = this.repositoryUOF.CityRepository.GetById(student.CityId.ToString());
                student.CityName=city.CityName;
                return student;
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
        public string GetUnitNameByStudentId(string studentId)
        {
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return this.repositoryUOF.StudentRepository.GetUnitNameByStudentId(studentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
        [HttpGet]
        public UnitBarData GetUnitBarData()
        {

            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return this.repositoryUOF.UnitRepository.GetBarDataDRAFT();

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
        public async Task<bool> DeleteQuestion(string questionId)
        {
            try
            {

                this.repositoryUOF.DBHelperOledb.OpenConnection();
                bool ok =repositoryUOF.QuestionRepository.Active(questionId);
               return ok;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
        [HttpGet]
        public async Task<bool> DeleteUnit(string unitId)
        {
            try
            {

                this.repositoryUOF.DBHelperOledb.OpenConnection();
                bool ok = repositoryUOF.UnitRepository.Active(unitId);
                return ok;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
        [HttpGet]
        public async Task<bool> DeleteStudent(string studentId)
        {
            try
            {

                this.repositoryUOF.DBHelperOledb.OpenConnection();
                bool ok = repositoryUOF.StudentRepository.Active(studentId);
                return ok;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
        [HttpPost]
        public async Task<bool> AddNewUnit()
        {
            string json = Request.Form["model"].ToString();
            FirstOrderKitModel.Unit unit =JsonSerializer.Deserialize<FirstOrderKitModel.Unit>(json);
            IFormFile image = Request.Form.Files["file"];
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                this.repositoryUOF.BeginTransaction();
                bool ok = this.repositoryUOF.UnitRepository.Create(unit);
                string id = this.repositoryUOF.GetLastInsertId();
                string fileName = $"{id}{unit.UnitPicture}";
                this.repositoryUOF.UnitRepository.UpdateImageName(id, fileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                                           "wwwroot",
                                           "Images",
                                           "Units",
                                           fileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                this.repositoryUOF.Commit();
                return true;
            }
            catch
            {
                this.repositoryUOF.RollBack();
                return false;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
    }
}
