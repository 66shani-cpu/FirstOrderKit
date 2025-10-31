using System.Data;
using FirstOrderKitModel;
using System.Reactive;
namespace FirstOrderKitWS
{
    public class UnitCreater : IModelCreater<FirstOrderKitModel.Unit>
    {
        public FirstOrderKitModel.Unit CreateModel(IDataReader dataReader)
        {
            FirstOrderKitModel.Unit unit = new FirstOrderKitModel.Unit();
            unit.UnitId = Convert.ToString(dataReader["unitId"]);
            unit.UnitName = Convert.ToString(dataReader["unitName"]);
            unit.UnitPicture = Convert.ToString(dataReader["unitPicture"]);
            return unit;
        }
    }
}
