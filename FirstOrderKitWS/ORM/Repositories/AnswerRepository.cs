using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class AnswerRepository : Repository, IRepository<Answer>
    {
        public bool Create(Answer model)
        {
            string sql = @$"Insert into Answer  
                           (
                              TrueFalse,QuestionId,AnswerText
                           )
                          values
                             (
                                @TrueFalse,@QuestionId,@AnswerText
                              )";
            this.helperOledb.AddParameter("@TrueFalse", model.TrueFalse.ToString());
            this.helperOledb.AddParameter("@QuestionId", model.QuestionId.ToString());
            this.helperOledb.AddParameter("@AnswerText", model.AnswerText);
            return this.helperOledb.Insert(sql) > 0;

        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Answer where AnswerId=@AnswerId";
            this.helperOledb.AddParameter("@AnswerId", id);
            return this.helperOledb.Delete(sql) > 0;
        }
        //מעביר את הטבלה למודל
        public List<Answer> GetAll()
        {
            string sql = " Select * from Answer";

            List<Answer> answers = new List<Answer>();
            //אחרי שימוש ברידר למחוק אותו בזיכרון במחשב כדי שלא יהיה הרבה זבל
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    answers.Add(this.modelCreaters.AnswerCreator.CreateModel(reader));
                }
            }

            return answers;
        }

        public Answer GetById(string id)
        {
            string sql = " Select * from Answer  where AnswerId=@AnswerId";
            this.helperOledb.AddParameter("@AnswerId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreaters.AnswerCreator.CreateModel(reader);
            }
        }

        public bool Update(Answer model)
        {
            string sql = @"Update Answer set TrueFalse=@TrueFalse,QuestionId=@QuestionId,
                                             AnswerText=@AnswerText";
            this.helperOledb.AddParameter("@TrueFalse", model.TrueFalse);
            this.helperOledb.AddParameter("@QuestionId", model.QuestionId);
            this.helperOledb.AddParameter("@AnswerText", model.AnswerText);
            return this.helperOledb.Update(sql) > 0;
        }
    }
}
