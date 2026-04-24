using FirstOrderKitModel;
using FirstOrderKitModel.ViewModel;
using System.Data;



namespace FirstOrderKitWS.ORM.Repositories
{
    public class UnitRepository : Repository, IRepository<Unit>
    {
        public UnitRepository(DBHelperOledb dbhelperOledb, ModelCreaters modelCreaters) : base(dbhelperOledb, modelCreaters)
        {

        }
        public bool Create(Unit model)
        {
            string sql = @$"Insert into Unit 
                           (UnitName,UnitPicture)
                          values(@UnitName,@UnitPicture)";
            this.helperOledb.AddParameter("@UnitName", model.UnitName);
            this.helperOledb.AddParameter("@UnitPicture", model.UnitPicture);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Units where UnitId=@UnitId";
            this.helperOledb.AddParameter("@UnitId", id);
            return this.helperOledb.Delete(sql) > 0;
        }
        //מעביר את הטבלה למודל
        public List<Unit> GetAll()
        {
            string sql = " Select * from Units";

            List<Unit> units = new List<Unit>();
            //אחרי שימוש ברידר למחוק אותו בזיכרון במחשב כדי שלא יהיה הרבה זבל
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    units.Add(this.modelCreaters.UnitCreater.CreateModel(reader));
                }
            }

            return units;
        }

        public Unit GetById(string id)
        {
            string sql = " Select * from Units where UnitId=@UnitId";
            this.helperOledb.AddParameter("@UnitId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreaters.UnitCreater.CreateModel(reader);
            }
        }

        public bool Update(Unit model)
        {
            string sql = @"Update Unit set UnitName=@UnitName,UnitPicture=@UnitPicture";
            this.helperOledb.AddParameter("@UnitName", model.UnitName);
            this.helperOledb.AddParameter("@UnitPicture", model.UnitPicture);
            return this.helperOledb.Update(sql) > 0;
        }
        public List<Unit> FilterBySubject(string subjectId)
        {
            List<Unit> units = new List<Unit>();
            string sql = @" SELECT
            Units.UnitId,
            Units.UnitName,
            Units.UnitPicture,
            SubjectUnit.SubjectId
        FROM
            Units
            INNER JOIN SubjectUnit ON Units.UnitId = SubjectUnit.SubjectId
        WHERE
            (((SubjectUnit.SubjectId) = @SubjectId));";
            this.helperOledb.AddParameter("@SubjectId", subjectId);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    units.Add(this.modelCreaters.UnitCreater.CreateModel(reader));
                }
            }

            return units;
        }

        public UnitBarData GetBarData()
        {
            string sql = @"SELECT Units.UnitName,
                    Sum(IIf(StudentTest.Grade > 60, 1, 0)) AS Passed,
                    Sum(IIf(StudentTest.Grade <= 60, 1, 0)) AS Failed
                FROM
                    (
                        Units
                        INNER JOIN UnitTests ON Units.UnitId = UnitTests.UnitId
                    )
                    INNER JOIN StudentTest ON UnitTests.TestId = StudentTest.TestId
                GROUP BY
                    Units.UnitName;";
            IDataReader reader = this.helperOledb.Select(sql);
          
            UnitBarData data = new UnitBarData();
            data.NotPass = new List<int>();
            data.Pass = new List<int>(); 
            data.Labels= new List<string>();

            while (reader.Read())
            {
                data.Labels.Add( reader["UnitName"].ToString() );
                data.NotPass.Add(Convert.ToInt32(reader["Failed"] ));
                data.Pass.Add(Convert.ToInt32(reader["Passed"]));
            }
            return data;    
        }
    }


    }
