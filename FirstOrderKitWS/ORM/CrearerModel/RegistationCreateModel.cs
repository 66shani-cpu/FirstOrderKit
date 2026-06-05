using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.CrearerModel
{
    public class RegistationCreateModel : IModelCreater<RegistationViewModel>
    {
        public RegistationViewModel CreateModel(IDataReader dataReader)
        {
            RegistationViewModel registationViewModel = new RegistationViewModel();
            //registationViewModel.units = Convert.ToString(dataReader["UnitName"]);
            //registationViewModel.cities = Convert.ToString(dataReader["CityName"]);
            //registationViewModel.student = Convert.ToString(dataReader["MessageId"]);          
            City c = new City();
            c.CityName = Convert.ToString(dataReader["CityName"]);
            registationViewModel.cities.Add(c);
            Unit u = new Unit();
            u.UnitName = Convert.ToString(dataReader["UnitName"]);
            registationViewModel.units.Add(u);
            registationViewModel.student.StudentId = Convert.ToString(dataReader["StudentId"]);
            return registationViewModel;
        }
        
    }
}
