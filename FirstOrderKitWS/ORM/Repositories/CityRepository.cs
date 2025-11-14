using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class CityRepository : Repository, IRepository<City>
    {
        public CityRepository(DBHelperOledb dbhelperOledb, ModelCreaters modelCreaters) : base(dbhelperOledb, modelCreaters)
        {

        }
        public bool Create(City model)
        {
            string sql = @$"Insert into Cities
                   (CityName)
                   values
                    (@CityName)";
            this.helperOledb.AddParameter("@CityName", model.CityName);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Cities where CityId=@CityId";
            this.helperOledb.AddParameter("@CityId", id);
            return this.helperOledb.Delete(sql) > 0;
        }
        //מעביר את הטבלה למודל
        public List<City> GetAll()
        {
            string sql = " Select * from Cities";

            List<City> cities = new List<City>();
            //אחרי שימוש ברידר למחוק אותו בזיכרון במחשב כדי שלא יהיה הרבה זבל
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    cities.Add(this.modelCreaters.CityCreater.CreateModel(reader));
                }
            }

            return cities;
        }

        public City GetById(string id)
        {
            string sql = " Select * from City  where CityId=@CityId";
            this.helperOledb.AddParameter("@CityId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreaters.CityCreater.CreateModel(reader);
            }
        }

        public bool Update(City model)
        {
            string sql = @"Update Student set CityName";
            this.helperOledb.AddParameter("@CityName", model.CityName);
            return this.helperOledb.Update(sql) > 0;
        }
    }
}
