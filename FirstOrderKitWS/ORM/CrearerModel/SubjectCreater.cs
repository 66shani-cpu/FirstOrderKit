using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS
{
    public class SubjectCreater : IModelCreater<Subject>
    {
        public Subject CreateModel(IDataReader dataReader)
        {
            Subject subject = new Subject();
            subject.SubjectId = Convert.ToString(dataReader["SubjectId"]);
            subject.SubjectName= Convert.ToString(dataReader["SubjectName"]);
            return subject;
        }
      
    }
}
