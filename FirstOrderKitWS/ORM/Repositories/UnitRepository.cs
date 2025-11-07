using FirstOrderKitModel;
using System.Data;
using System.Reactive;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class UnitRepository : Repository, IRepository<Unit>
    {
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
            string sql = @"Delete from Unit where UnitId=@UnitId";
            this.helperOledb.AddParameter("@UnitId", id);
            return this.helperOledb.Delete(sql) > 0;
        }
        //מעביר את הטבלה למודל
        public List<Unit> GetAll()
        {
            string sql = " Select * from Unit";

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
            string sql = " Select * from Unit  where UnitId=@UnitId";
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
        
        
    }
}
