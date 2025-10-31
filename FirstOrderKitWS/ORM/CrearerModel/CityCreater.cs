using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS
{
    public class CityCreater : IModelCreater<City>
    {
        public City CreateModel(IDataReader dataReader)
        {
            City city = new City();
            city.CityId = Convert.ToString(dataReader["CityId"]);
            city.CityName= Convert.ToString(dataReader["CityName"]);
            return city;
        }
    }
}
