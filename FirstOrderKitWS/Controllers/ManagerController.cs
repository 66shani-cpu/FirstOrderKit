using FirstOrderKitWS.ORM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstOrderKitModel;
using FirstOrderKitModel.ViewModel;

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

        [HttpGet]
        public Message Message(string messageId)
        {
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                Message message = repositoryUOF.MessageRepository.GetById(messageId);
                return message;
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
        public string LogInStusent(string nickName, string password)
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
        [HttpGet]
        public double GetReportsAVG()
        {
            try
            {

                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return this.repositoryUOF.TestRepository.TestsAvg();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }

        [HttpGet]
        public List<ReportsViewModel> GetReportsPast(int min)
        {
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                return this.repositoryUOF.TestRepository.TestPast();
                //List<Test> tests = repositoryUOF.TestRepository.GetAll();
                //for (int i = 0; i < tests.Count; i++)
                //{
                //    int count+= tests[i].
                //}
                //return count;
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
    }
}
