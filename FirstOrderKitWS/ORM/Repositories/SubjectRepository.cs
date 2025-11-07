using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class SubjectRepository : Repository, IRepository<Subject>
    {
        public bool Create(Subject model)
        {
            string sql = @$"Insert into Subject 
                           (SubjectName)
                          values(@SubjectName)";
            this.helperOledb.AddParameter("@SubjectName", model.SubjectName);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Subject where SubjectId=@SubjectId";
            this.helperOledb.AddParameter("@SubjectId", id);
            return this.helperOledb.Delete(sql) > 0;
        }

        public List<Subject> GetAll()
        {
            string sql = " Select * from Subject";

            List<Subject> subjects = new List<Subject>();
            //אחרי שימוש ברידר למחוק אותו בזיכרון במחשב כדי שלא יהיה הרבה זבל
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    subjects.Add(this.modelCreaters.SubjectCreater.CreateModel(reader));
                }
            }

            return subjects;
        }

        public Subject GetById(string id)
        {
            string sql = " Select * from Subject where SubjectId=@SubjectId";
            this.helperOledb.AddParameter("@SubjectId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreaters.SubjectCreater.CreateModel(reader);
            }
        }

        public bool Update(Subject model)
        {
            string sql = @"Update Subject set SubjectName=@SubjectName";
            this.helperOledb.AddParameter("@SubjectName", model.SubjectName);
            return this.helperOledb.Update(sql) > 0;
        }
    }
}
