using FirstOrderKitWS.ORM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstOrderKitModel;

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
        [HttpGet]
        //ברירת מחדל null
        public bool AddQuestion(AddQuestionViewModel addQuestionViewModel)
        {

          
            //אם והמערכת קורסת הוא סוגק קשר ומחזיר null
            try
            {
                this.repositoryUOF.DBHelperOledb.OpenConnection();
                bool ok= this.repositoryUOF.QuestionRepository.Create(addQuestionViewModel.Question);
                foreach (Answer answer in addQuestionViewModel.Answers)
                {
                    ok = this.repositoryUOF.AnswerRepository.Create(answer);    
                }
                return true;
            }
            catch (Exception ex)
            {
                return false ;
            }
            finally
            {
                this.repositoryUOF.DBHelperOledb.CloseConnection();
            }
        }
    }
}
