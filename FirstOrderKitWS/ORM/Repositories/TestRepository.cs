using FirstOrderKitModel;
using FirstOrderKitModel.ViewModel;
using System.Data;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class TestRepository : Repository, IRepository<Test>
    {
        public TestRepository(DBHelperOledb dbhelperOledb, ModelCreaters modelCreaters) : base(dbhelperOledb, modelCreaters)
        {

        }
        public bool Create(Test model)
        {
            string sql = @$"Insert into Tests 
                           (
                              TestName,UnitId,LevelQuestion,StudentId,Grade
                           )
                          values
                            (
                                 @TestName,@UnitId,@LevelQuestion,@StudentId,@Grade
                            )";
            this.helperOledb.AddParameter("@TestName", model.TestName);
            this.helperOledb.AddParameter("@UnitId", model.UnitId);
            this.helperOledb.AddParameter("@LevelQuestion", model.LevelQuestion);
            this.helperOledb.AddParameter("@StudentId", model.StudentId);
            this.helperOledb.AddParameter("@Grade", model.Grade);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Tests where TestId=@TestId";
            this.helperOledb.AddParameter("@TestId", id);
            return this.helperOledb.Delete(sql) > 0;
        }
        //מעביר את הטבלה למודל
        public List<Test> GetAll()
        {
            string sql = " Select * from Tests";

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
            string sql = " Select * from Tests  where TestId=@TestId";
            this.helperOledb.AddParameter("@TestId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreaters.TestCreater.CreateModel(reader);
                //if (!reader.Read())
                //{
                //    return null; // לא נמצא
                //}

                //return this.modelCreaters.TestCreater.CreateModel(reader);
            }
        }
        public List<Test> GetByStudentId(string studentId)
        {
            // יצירת רשימה ריקה שאליה נאסוף את המבחנים
            List<Test> testsList = new List<Test>();

            string sql = $@"SELECT Tests.Grade, Tests.TestName
FROM Tests
WHERE (((Tests.StudentId)=@studentId));
";
            this.helperOledb.AddParameter("@studentId", studentId);

            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    // יצירת מודל מבחן בודד מתוך השורה הנוכחית
                    Test currentTest = this.modelCreaters.TestHistoryCreator.CreateModel(reader);
                    // הוספת המבחן לרשימה
                    testsList.Add(currentTest);
                }
            }
            return testsList;
        }

        public bool Update(Test model)
        {
            string sql = @"Update Tests set TestName=@TestName";
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

        public List<ReportsViewModel> TestPast()
        {
            string sql = @"SELECT Tests.TestId,Count(StudentTest.grade) 
                                AS StudentCount,Tests.TestName
                           FROM Tests
                             INNER JOIN StudentTest ON Tests.TestId = StudentTest.TestId
                             GROUP BY Tests.TestId,StudentTest.grade,Tests.TestName
                             HAVING (((StudentTest.grade) > 50));";
            List<ReportsViewModel> tests = new List<ReportsViewModel>();
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    tests.Add(this.modelCreaters.ReportsViewModelCreator.CreateModel(reader));
                }
            }
            return tests;
        }
        public bool AddQuestion(string testId,string questionId)
        {
            string sql = @"INSERT INTO QuestionTest VALUES (@TestId, @QuestionId)";
            this.helperOledb.AddParameter("@TestId", testId);
            this.helperOledb.AddParameter("@QuestionId", questionId);
            return this.helperOledb.Insert(sql) > 0;
        }
    }
}
