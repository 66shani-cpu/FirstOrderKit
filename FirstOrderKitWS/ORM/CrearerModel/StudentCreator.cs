using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS
{
    public class StudentCreator: IModelCreater<Student>
    {
        public Student CreateModel(IDataReader dataReader)
        {
            Student student = new Student();
            student.StudentAdrres=Convert.ToString(dataReader["StudentAdrres"]);
            student.StudentTelephone= Convert.ToString(dataReader["StudentTelephone"]);
            student.StudentFirstName= Convert.ToString(dataReader["StudentFirstName"]);
            student.StudentLastName=Convert.ToString(dataReader["StudentLastName"]);
            student.StudentNickName= Convert.ToString(dataReader["StudentNickName"]);
            student.StudentId= Convert.ToString(dataReader["StudentId"]);
            student.CityId = Convert.ToInt16(dataReader["CityId"]);
            student.StudentImage= Convert.ToString(dataReader["StudentImage"]);
            student.UnitId = Convert.ToInt16(dataReader["UnitId"]);
            student.Password = Convert.ToString(dataReader["Password"]);
            student.StudentSalt = Convert.ToString(dataReader["StudentSalt"]);
            return student;
        }

       
    }
}
