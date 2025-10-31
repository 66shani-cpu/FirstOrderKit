using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS
{
    public class AnswerCreator : IModelCreater<Answer>
    {
        public Answer CreateModel(IDataReader dataReader)
        {
            Answer answer = new Answer();
            answer.AnswerText = Convert.ToString(dataReader["AnswerText"]);
            answer.Answerid = Convert.ToString(dataReader["Answerid"]);
            return answer;
            //Answer answer = new Answer();
            //{
            //    answer.AnswerText = Convert.ToString(dataReader["AnswerText"]),
            //    answer.Answerid = Convert.ToString(dataReader["Answerid"]),

            //};
            //return answer;
        }
    }
}
