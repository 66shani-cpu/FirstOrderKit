using FirstOrderKitModel;
using FirstOrderKitModel.ViewModel;
using System.Data;

namespace FirstOrderKitWS.ORM
{
    public class ReportsViewModelCreator
    {
        public ReportsViewModel CreateModel(IDataReader reader)
        {
            ReportsViewModel model = new ReportsViewModel();
            model.TestId = Convert.ToString(reader["TestId"]);
            model.StudentCoun = Convert.ToInt32(reader["StudentCount"]);
            model.TestName = Convert.ToString(reader["TestName"]);
            return model;
        }
    }
}
