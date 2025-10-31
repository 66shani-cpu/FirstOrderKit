using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS
{
    public class QuestionCreater:IModelCreater<Question>
    {
        public Question CreateModel(IDataReader dataReader)
        {
            Question question = new Question();
            question.QuestionId= Convert.ToString(dataReader["questionId"]);
            question.QuestionText= Convert.ToString(dataReader["question"]);
            question.LevelQuestions= Convert.ToString(dataReader["levelQuestions"]);
            return question;
        }
    }
}
