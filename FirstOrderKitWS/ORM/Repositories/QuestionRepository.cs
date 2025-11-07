using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class QuestionRepository : Repository, IRepository<Question>
    {
        public bool Create(Question model)
        {
            string sql = @$"Insert into Question
                           (LevelQuestions,QuestionText)
                          values(@LevelQuestions,@QuestionText)";
            this.helperOledb.AddParameter("@LevelQuestions", model.LevelQuestions);
            this.helperOledb.AddParameter("@QuestionText", model.QuestionText);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Question where QuestionId=@QuestionId";
            this.helperOledb.AddParameter("@QuestionId", id);
            return this.helperOledb.Delete(sql) > 0;
        }
        //מעביר את הטבלה למודל
        public List<Question> GetAll()
        {
            string sql = " Select * from Question";

            List<Question> questions = new List<Question>();
            //אחרי שימוש ברידר למחוק אותו בזיכרון במחשב כדי שלא יהיה הרבה זבל
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    questions.Add(this.modelCreaters.QuestionCreater.CreateModel(reader));
                }
            }

            return questions;
        }

        public Question GetById(string id)
        {
            string sql = " Select * from Question  where QuestionId=@QuestionId";
            this.helperOledb.AddParameter("@QuestionId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreaters.QuestionCreater.CreateModel(reader);
            }
        }

        public bool Update(Question model)
        {
            string sql = @"Update Question set LevelQuestions=@LevelQuestions,
              QuestionText=@QuestionText";
            this.helperOledb.AddParameter("@LevelQuestions", model.LevelQuestions);
            this.helperOledb.AddParameter("@QuestionText", model.QuestionText);
            return this.helperOledb.Update(sql) > 0;
        }
        public List<Question> GetLevelQuestion(string questionId)
        {
            string sql = @"SELECT
    Question.QuestionId,
    Question.LevelQuestions,
    Question.Question
FROM
    Question
WHERE
    (((Question.LevelQuestions) = QuestionId))";
this.helperOledb.AddParameter("@QuestionId", questionId);
            List<Question> questions = new List<Question>();
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    questions.Add(this.modelCreaters.QuestionCreater.CreateModel(reader));
                }
            }

            return questions;


        }
    }
}
