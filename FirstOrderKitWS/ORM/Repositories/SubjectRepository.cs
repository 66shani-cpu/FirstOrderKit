using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class SubjectRepository : Repository, IRepository<Subject>
    {
        public SubjectRepository(DBHelperOledb dbhelperOledb, ModelCreaters modelCreaters) : base(dbhelperOledb, modelCreaters)
        {

        }
        public bool Create(Subject model)
        {
            string sql = @$"Insert into Subjectד 
                           (SubjectName)
                          values(@SubjectName)";
            this.helperOledb.AddParameter("@SubjectName", model.SubjectName);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Subjects where SubjectId=@SubjectId";
            this.helperOledb.AddParameter("@SubjectId", id);
            return this.helperOledb.Delete(sql) > 0;
        }

        public List<Subject> GetAll()
        {
            string sql = " Select * from Subjects";

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
            string sql = " Select * from Subjects where SubjectId=@SubjectId";
            this.helperOledb.AddParameter("@SubjectId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreaters.SubjectCreater.CreateModel(reader);
            }
        }

        public bool Update(Subject model)
        {
            string sql = @"Update Subjects set SubjectName=@SubjectName";
            this.helperOledb.AddParameter("@SubjectName", model.SubjectName);
            return this.helperOledb.Update(sql) > 0;
        }
        //סינון לפי האות הראשונה 
        //public List<Subject> SubjectFilter(string subjectName)
        //{
        //   string sql = $@"SELECT Subjects.SubjectId,
        //                   Subjects.SubjectName
        //                    FROM Subjects
        //                    WHERE Subjects.SubjectName LIKE '{subjectName}%'";
        //    List<Subject> subjects= new List<Subject>();
        //    using (IDataReader reader = this.helperOledb.Select(sql))
        //    {
        //        while (reader.Read())
        //        {
        //            subjects.Add(this.modelCreaters.SubjectCreater.CreateModel(reader));
        //        }
        //    }
        //    return subjects;
        //}
    }
}
