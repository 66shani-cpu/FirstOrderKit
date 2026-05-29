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
            test.TestId = Convert.ToString(dataReader["TestId"]);
            test.TestName = Convert.ToString(dataReader["TestName"]);
            test.UnitId= Convert.ToString(dataReader["unitId"]);
            test.LevelQuestion= Convert.ToString(dataReader["levelQuestion"]);
            test.StudentId= Convert.ToString(dataReader["StudentId"]);
            test.Grade= Convert.ToString(dataReader["Grade"]);
            return test;
        }
    }
}
