using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.CrearerModel
{
    public class TestHistoryCreator : IModelCreater<Test>
    {
        public Test CreateModel(IDataReader dataReader)
        {
            Test test = new Test();
            test.TestName = Convert.ToString(dataReader["TestName"]);
            test.Grade = Convert.ToString(dataReader["Grade"]);
            return test;
        }
    }
}
