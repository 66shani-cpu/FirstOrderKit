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
        [HttpGet] //ברירת מחדל null
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
        //public List<TestQuestionViewModel> SaveTest(Test test)
        ////פעולה שתפקידה ליצור ולהחזיר ViewModel מטיפוס RequestNewTest
        //{
        //    List<TestQuestionViewModel> testQuestionViewModel = new List<TestQuestionViewModel>();
        //    try
        //    {
        //        this.repositoryUOF.DBHelperOledb.OpenConnection();
        //        List<Question> questionList = repositoryUOF.QuestionRepository.GetQuestion(unitId, difficulty);
        //        repositoryUOF.DBHelperOledb.OpenTransaction();

        //        Test test = new Test();
        //        test.TestName = $"{unitId}-{difficulty}-" + DateTime.UtcNow.ToString();
        //        repositoryUOF.TestRepository.Create(test);
        //        test.TestId = repositoryUOF.GetLastInsertId();

        //        foreach (Question question in questionList)
        //        {
        //            TestQuestionViewModel tqwm = new TestQuestionViewModel();
        //            tqwm.Question = question;
        //            tqwm.QuestionAnswer = repositoryUOF.AnswerRepository.GetAnswersByQuestion(question.QuestionId);
        //            repositoryUOF.TestRepository.AddQuestion(test.TestId, question.QuestionId);
        //            testQuestionViewModel.Add(tqwm);

        //        }
        //        this.repositoryUOF.DBHelperOledb.Commit();
        //        return testQuestionViewModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.repositoryUOF.DBHelperOledb.Rollback();
        //        Console.WriteLine(ex.ToString());
        //        return null;
        //    }
        //    finally
        //    {
        //        this.repositoryUOF.DBHelperOledb.CloseConnection();
        //    }
        //}

        //private bool SaveNewTest(string unitId, string difficulty, List<TestQuestionViewModel> testQuestionViews )
        //{

        //    string json = Request.Form["model"].ToString();
        //    TestQuestionViewModel testQuestionViewModel = JsonSerializer.Deserialize<TestQuestionViewModel>(json);
        //    Question question = new Question();
        //    List<Answer> answerList = new List<Answer>();
        //    foreach(var answer in testQuestionViews)
        //    {
        //        Answer answer1 = new Answer();
        //        answer1.AnswerText = answer.answer1;
        //        answerList.Add(answer1);

        //        Answer answer2 = new Answer();
        //        answer2.AnswerText = answer.answer2;
        //        answerList.Add(answer2);

        //        Answer answer3 = new Answer();
        //        answer3.AnswerText = answer.answer3;
        //        answerList.Add(answer3);
        //    }

        //    try
        //    {
        //        this.repositoryUOF.DBHelperOledb.OpenConnection();
        //        repositoryUOF.DBHelperOledb.OpenTransaction();
        //        bool ok = this.repositoryUOF.QuestionRepository.Create();
        //        bool ok1 = this.repositoryUOF.AnswerRepository.Create();
        //        bool ok2 = this.repositoryUOF.UnitRepository.Create();
        //        bool ok3 = this.repositoryUOF.TestRepository.Create();
        //        return true;
        //    }

        //}

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
