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
        Random random = new Random();
        public StudentController()
        {
            this.repositoryUOF = new RepositoryUOF();
        }
        [HttpGet]
        //ברירת מחדל null
        public List<TestQuestionViewModel> GetNewTest ()
        {
            List<TestQuestionViewModel> requestANewTestViewModel = new List<TestQuestionViewModel>();
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                List<Question> questionLevel = new List<Question>();
                for(int i = 0;i<10; i++)
                {
                    int num = random.Next(questionLevel.Count);
                    TestQuestionViewModel testQuestionViewModel= new TestQuestionViewModel();
                    Question question=questionLevel[num];
                    while (IfQuestionExist(requestANewTestViewModel, question.QuestionId)==true)
                    {
                         num = random.Next(questionLevel.Count);
                         question = questionLevel[num];
                    }
                    testQuestionViewModel.QuestionAnswer = repositoryUOF.AnswerRepository.GetAnswersByQuestion(testQuestionViewModel.Question.QuestionId);
                    requestANewTestViewModel.Add(testQuestionViewModel);
                }
                return requestANewTestViewModel;
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
                return repositoryUOF.StudentRepository.LogIn(nickName,password);
                
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
                return repositoryUOF.StudentRepository.Update(student);
                
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
        
    }
}
