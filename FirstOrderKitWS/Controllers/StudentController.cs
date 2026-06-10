using FirstOrderKitModel;
using FirstOrderKitWS.ORM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FirstOrderKitWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        RepositoryUOF repositoryUOF;
        Random random = new Random();
        public StudentController()
        {
            this.repositoryUOF = new RepositoryUOF();
        }

        //פעולה שמקבלת את הנושא והרמה שהמשתמש הזין יוצרת רשימה של כל השאלות לפי הדרישה
        // ולאחר מכן עוברת על כל שאלה ומביאה את התשובות שלה ולאחר מכן מכניסה את זה לתוך הרשימה של 
        //הTestQuestionViewModel
        [HttpGet] 
        public List<TestQuestionViewModel> GetNewTest(string unitId, string difficulty)
        {
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {         
                List<TestQuestionViewModel> testQuestionViewModelList = new List<TestQuestionViewModel>();
                this.repositoryUOF.DBHelperOledb.OpenConnection();

                List<Question> questions = this.repositoryUOF.QuestionRepository.GetQuestion(unitId, difficulty);
                foreach (Question question in questions)
                {
                    TestQuestionViewModel testQuestionViewModel = new TestQuestionViewModel();
                    testQuestionViewModel.Question=question;
                    testQuestionViewModel.QuestionAnswer = this.repositoryUOF.AnswerRepository.GetAnswersByQuestion(question.QuestionId);
                    testQuestionViewModelList.Add(testQuestionViewModel);
                }
                return testQuestionViewModelList;
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
        [Produces("application/json")]
        public string LogInStudent(string nickName, string password)
        {
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return repositoryUOF.StudentRepository.LogIn(nickName, password);

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
        public bool UpDate(Student student)
        {
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                repositoryUOF.DBHelperOledb.OpenTransaction();
                bool ok=repositoryUOF.StudentRepository.Update(student);
                this.repositoryUOF.DBHelperOledb.Commit();
                return ok;
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
        [HttpGet]
        public RegistationViewModel GetRegistationViewModel(string studentId=null)
        {
            try
            {
                RegistationViewModel registationViewModel = new RegistationViewModel();
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                registationViewModel.student = (studentId == null) ?  new Student() : repositoryUOF.StudentRepository.GetById(studentId);
                registationViewModel.cities= repositoryUOF.CityRepository.GetAll();
                registationViewModel.units=repositoryUOF.UnitRepository.GetAll();               
               return registationViewModel;
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
        [Produces("application/json")]
        public Student GetStudent(string studentId)
        {
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return repositoryUOF.StudentRepository.GetById(studentId);

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
        public RequestNewTest GetUD()
        {
            RequestNewTest requestNewTest = new RequestNewTest();
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                requestNewTest.Units= repositoryUOF.UnitRepository.GetAll();
                requestNewTest.Difficultys=repositoryUOF.QuestionRepository.GetDifficultys();
                return requestNewTest;
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
        public bool SaveTest([FromBody] Test test)
          //פעולה שתפקידה ליצור ולהחזיר ViewModel מטיפוס RequestNewTest
        {
            try
            {   
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                bool ok =repositoryUOF.TestRepository.Create(test);
                if (ok)
                {
                    return true;
                }
                return false;
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
        public List<Test> GetTestByStudent(string studentId)
        {
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return repositoryUOF.TestRepository.GetByStudentId(studentId);
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

    }
}
