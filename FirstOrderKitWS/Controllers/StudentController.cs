using FirstOrderKitModel;
using FirstOrderKitWS.ORM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reactive.Subjects;

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
        [HttpGet] //ברירת מחדל null
        public List<TestQuestionViewModel> GetNewTest(string subjectId, string difficulty)
        {
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {         
                List<TestQuestionViewModel> testQuestionViewModelList = new List<TestQuestionViewModel>();
                this.repositoryUOF.DBHelperOledb.OpenConnection();

                List<Question> questions = this.repositoryUOF.QuestionRepository.GetQuestion(subjectId, difficulty);
                foreach (Question question in questions)
                {
                    TestQuestionViewModel testQuestionViewModel = new TestQuestionViewModel();
                    testQuestionViewModel.Question=question;
                    testQuestionViewModel.QuestionAnswer = this.repositoryUOF.AnswerRepository.GetAnswersByQuestion(question.QuestionId);
                    testQuestionViewModelList.Add(testQuestionViewModel);
                }
                #region stam
                //List<Question> questionLevel = this.repositoryUOF.QuestionRepository.GetAll();
                //int k = 0;
                //for (int i = 0; i < questionLevel.Count; i++)
                //{
                //    int num = random.Next(questionLevel.Count);
                //    TestQuestionViewModel testQuestionViewModel = new TestQuestionViewModel();
                //    Question question = questionLevel[num];
                //    while (IfQuestionExist(testQuestionViewModelList, question.QuestionId) == true)
                //    {
                //        num = random.Next(questionLevel.Count);
                //        question = questionLevel[num];
                //    }
                //    testQuestionViewModel.QuestionAnswer = repositoryUOF.AnswerRepository.GetAnswersByQuestion(testQuestionViewModel.Question.QuestionId);
                //    testQuestionViewModelList.Add(testQuestionViewModel);
                //    if (k == 10)
                //        break;
                //}

                #endregion
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

        private bool IfQuestionExist(List<TestQuestionViewModel> questionList, string questionId)
        {
            foreach (TestQuestionViewModel question in questionList)
            {
                if (question.Question.QuestionId == questionId) return true;
            }
            return false;
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
        public List<TestQuestionViewModel> GetRequestNewTest(string unitId, string difficulty)
          //פעולה שתפקידה ליצור ולהחזיר ViewModel מטיפוס RequestNewTest
        {
            List<TestQuestionViewModel> testQuestionViewModel = new List<TestQuestionViewModel>();
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                List < Question > questionList = repositoryUOF.QuestionRepository.GetQuestion(unitId, difficulty); 
                foreach ( Question question in questionList)
                {
                    TestQuestionViewModel tqwm = new TestQuestionViewModel();
                    tqwm.Question = question;
                    tqwm.QuestionAnswer = repositoryUOF.AnswerRepository.GetAnswersByQuestion(question.QuestionId);
                    testQuestionViewModel.Add(tqwm);

                }

               // לשמור מבחן בתוך מסד הנתונים

                return testQuestionViewModel;

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

        private bool SaveNewTest(string unitId, string difficulty, List<TestQuestionViewModel> testQuestionViews )
        {
            Test test = new Test();
            test.TestName = "";
            return true;
        }
      
    }
}
