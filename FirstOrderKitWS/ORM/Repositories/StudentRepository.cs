using FirstOrderKitModel;
using System.Data;
using System.Reflection;

namespace FirstOrderKitWS
{
    public class StudentRepository : Repository, IRepository<Student>
    {
        public StudentRepository(DBHelperOledb dbhelperOledb, ModelCreaters modelCreaters) : base(dbhelperOledb, modelCreaters)
        {

        }
        public bool Create(Student model)
        {
            //string sql = @$"Insert into Student 
            //               (
            //                 StudentNickName, Password, StudentLastName, 
            //                 UnitId,StudentFirstName,CityId,StudentTelephone,
            //                 StudentAdrres,StudentImage
            //               )
            //               values
            //                 (
            //                      '{model.StudentNickName}','{model.Password}',
            //                      '{model.StudentLastName}',{model.UnitId},
            //                      '{model.StudentFirstName}',{model.CityId},'{model.StudentTelephone}',
            //                      '{model.StudentAdrres}','{model.StudentImage}'
            //                  )
            //                                         ";
            string sql = @$"Insert into Student 
                           (
                             StudentNickName, [Password], StudentLastName, 
                             UnitId,StudentFirstName,CityId,StudentTelephone,
                             StudentAdrres,StudentImage
                           )
                           values
                             (
                                 @StudentNickName,@Password,
                                 @StudentLastName,@UnitId,@StudentFirstName
                                @CityId,@StudentTelephone,@StudentAdrres,@StudentImage
                                 
                              )";
            this.helperOledb.AddParameter("@StudentNickName", model.StudentNickName);
            this.helperOledb.AddParameter("@Password", model.Password);
            this.helperOledb.AddParameter("@StudentLastName", model.StudentLastName);
            this.helperOledb.AddParameter("@UnitId", model.UnitId);
            this.helperOledb.AddParameter("@StudentFirstName", model.StudentFirstName);
            this.helperOledb.AddParameter("@CityId", model.CityId);
            this.helperOledb.AddParameter("@StudentTelephone", model.StudentTelephone);
            this.helperOledb.AddParameter("@StudentAdrres", model.StudentAdrres);
            this.helperOledb.AddParameter("@StudentImage", model.StudentImage);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Student where StudentId=@StudentId";
            this.helperOledb.AddParameter("@StudentId",id);
            return this.helperOledb.Delete(sql) > 0;
        }
        //מעביר את הטבלה למודל
        public List<Student> GetAll()
        {
            string sql = " Select * from Student";
          
            List<Student> students = new List<Student>();
            //אחרי שימוש ברידר למחוק אותו בזיכרון במחשב כדי שלא יהיה הרבה זבל
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    students.Add(this.modelCreaters.StudentCreator.CreateModel(reader));
                }
            }
          
            return students;
        }

        public Student GetById(string id)
        {
            string sql = " Select * from Student  where StudentId=@StudentId";
            this.helperOledb.AddParameter("@StudentId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
             return this.modelCreaters.StudentCreator.CreateModel(reader);
            }
        }

        public bool Update(Student model)
        {
            string sql = @"Update Student set StudentNickName=@StudentNickName,
                                             [Password]= @Password,StudentLastName=@StudentLastName,
                                             UnitId=@UnitId,StudentFirstName=@StudentFirstName,
                                             CityId=@CityId,StudentTelephone=@StudentTelephone,
                                             StudentAdrres=@StudentAdrres,StudentImage=@StudentImage
where StudentId=@StudentId";
            this.helperOledb.AddParameter("@StudentNickName", model.StudentNickName);
            this.helperOledb.AddParameter("@Password", model.Password);
            this.helperOledb.AddParameter("@StudentLastName", model.StudentLastName);
            this.helperOledb.AddParameter("@UnitId", model.UnitId);
            this.helperOledb.AddParameter("@StudentFirstName", model.StudentFirstName);
            this.helperOledb.AddParameter("@CityId", model.CityId);
            this.helperOledb.AddParameter("@StudentTelephone", model.StudentTelephone);
            this.helperOledb.AddParameter("@StudentAdrres", model.StudentAdrres);
            this.helperOledb.AddParameter("@StudentImage", model.StudentImage);
            this.helperOledb.AddParameter("@StudentId", model.StudentId);

            return this.helperOledb.Update(sql) > 0;
        }
        //הזדהות במערכת
        public string LogIn (string nickName, string password)
        {
            string sql = @"Select StudentId fron Student 
                 where StudentNickName =@StudentNickName
                 and [password]=@password";
            this.helperOledb.AddParameter("@StudentNickName",nickName);
            this.helperOledb.AddParameter("@password", password);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                if (reader.Read() == true)
                    return reader["ReaderId"].ToString();
                return null;
            }


        }
    }
}
