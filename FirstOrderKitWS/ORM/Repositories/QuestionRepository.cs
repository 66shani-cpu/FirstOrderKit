using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class QuestionRepository : Repository, IRepository<Question>
    {
        public QuestionRepository(DBHelperOledb dbhelperOledb, ModelCreaters modelCreaters) : base(dbhelperOledb, modelCreaters)
        {

        }
        public bool Create(Question model)
        {
            string sql = @$"Insert into Question
                           (LevelQuestions,Question)
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
        //סינון לפי קושי של שאלה
        public List<Question> GetLevelQuestion(string levelQuestion)
        {
            string sql = @"SELECT
    Question.QuestionId,
    Question.LevelQuestions,
    Question.Question
FROM
    Question
WHERE
    (((Question.LevelQuestions) = @levelQuestion))";
this.helperOledb.AddParameter("@levelQuestion", levelQuestion);
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

        public List<Question> GetQuestion(string subjectId, string difficulty)
        {
            string sql = " Select * from Question ";

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
    }
}
