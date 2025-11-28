using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class TestRepository : Repository, IRepository<Test>
    {
        public TestRepository(DBHelperOledb dbhelperOledb, ModelCreaters modelCreaters) : base(dbhelperOledb, modelCreaters)
        {

        }
        public bool Create(Test model)
        {
            string sql = @$"Insert into Test 
                           (TestName)
                          values(@TestName)";
            this.helperOledb.AddParameter("@TestName", model.TestName);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Test where TestId=@TestId";
            this.helperOledb.AddParameter("@TestId", id);
            return this.helperOledb.Delete(sql) > 0;
        }
        //מעביר את הטבלה למודל
        public List<Test> GetAll()
        {
            string sql = " Select * from Test";

            List<Test> tests = new List<Test>();
            //אחרי שימוש ברידר למחוק אותו בזיכרון במחשב כדי שלא יהיה הרבה זבל
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    tests.Add(this.modelCreaters.TestCreater.CreateModel(reader));
                }
            }

            return tests;
        }

        public Test GetById(string id)
        {
            string sql = " Select * from Test  where TestId=@TestId";
            this.helperOledb.AddParameter("@TestId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreaters.TestCreater.CreateModel(reader);
            }
        }

        public bool Update(Test model)
        {
            string sql = @"Update Test set TestName=@TestName";
            this.helperOledb.AddParameter("@TestName", model.TestName);
            return this.helperOledb.Update(sql) > 0;
        }

        public double TestsAvg()
        {
            string sql = @"SELECT Avg(StudentTest.Grade) AS Avg
                           FROM  Tests
                           INNER JOIN StudentTest ON Tests.TestId = StudentTest.TestId;";
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return Convert.ToDouble(reader["Avg"]);
            }
        }
    }
}
