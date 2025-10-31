using FirstOrderKitModel;
using System.Data;
using System.Reactive.Subjects;

namespace FirstOrderKitWS
{
    public class TestCreater : IModelCreater<Test>
    {
        public Test CreateModel(IDataReader dataReader)
        {
            Test test = new Test();
            test.TestId = Convert.ToString(dataReader["testId"]);
            test.TestName = Convert.ToString(dataReader["testName"]);

            return test;
        }
    }
}
