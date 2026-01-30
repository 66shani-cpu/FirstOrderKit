using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class AnswerRepository : Repository, IRepository<Answer>
    {
        //שליחה למחלקת בסיס
        public AnswerRepository(DBHelperOledb dbhelperOledb, ModelCreaters modelCreaters) : base(dbhelperOledb, modelCreaters)
        {

        }

        //public bool Create(Answer model)
        //{
        //    string sql = @$"Insert into Answer  
        //                   (
        //                      TrueFalse,QuestionId,AnswerText
        //                   )
        //                  values
        //                     (
        //                        @TrueFalse,@QuestionId,@AnswerText
        //                      )";
        //    this.helperOledb.AddParameter("@TrueFalse", model.TrueFalse.ToString());
        //    this.helperOledb.AddParameter("@QuestionId", model.QuestionId.ToString());
        //    this.helperOledb.AddParameter("@AnswerText", model.AnswerText);
        //    return this.helperOledb.Insert(sql) > 0;

        //}
        public bool Create(Answer model)
        {
            string sql = @"Insert into Answer
                (
                    TrueFalse,AnswerText
                )
                values
                (
                    @TrueFalse,@AnswerText
                )";
            this.helperOledb.AddParameter("@TrueFalse", model.TrueFalse.ToString());
            //this.helperOledb.AddParameter("@AnswerText", model.AnswerText);
            this.helperOledb.AddParameter("@AnswerText", model.AnswerText ?? "");
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
        public List<Answer> GetAnswersByQuestion(string questionId)
        {
            string sql = @"SELECT
    Answer.AnswerId,
    Answer.TrueFalse,
    Answer.OuestionId,
    Answer.AnswerText
FROM
    Answer
WHERE
    (((Answer.OuestionId) = questionId));";
            this.helperOledb.AddParameter("@questionId", questionId);
            List<Answer> answers = new List<Answer>();
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    answers.Add(this.modelCreaters.AnswerCreator.CreateModel(reader));
                }
            }

            return answers;
        }

        internal List<Answer> GetAnswersByQuestionId(string questionId)
        {
            throw new NotImplementedException();
        }
    }
}
