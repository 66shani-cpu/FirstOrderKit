using FirstOrderKitModel;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;

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
                             StudentAdrres,StudentImage,StudentSalt
                           )
                           values
                             (
                                 @StudentNickName,@Password,
                                 @StudentLastName,@UnitId,@StudentFirstName
                                @CityId,@StudentTelephone,@StudentAdrres,@StudentImage,@StudentSalt
                                 
                              )";
            this.helperOledb.AddParameter("@StudentNickName", model.StudentNickName);
           
            this.helperOledb.AddParameter("@StudentLastName", model.StudentLastName);
            this.helperOledb.AddParameter("@UnitId", model.UnitId);
            this.helperOledb.AddParameter("@StudentFirstName", model.StudentFirstName);
            this.helperOledb.AddParameter("@CityId", model.CityId);
            this.helperOledb.AddParameter("@StudentTelephone", model.StudentTelephone);
            this.helperOledb.AddParameter("@StudentAdrres", model.StudentAdrres);
            this.helperOledb.AddParameter("@StudentImage", model.StudentImage);
            string salt = GetSalt(GetRandom());
            this.helperOledb.AddParameter("@Password", GetHash(model.Password, salt));
            this.helperOledb.AddParameter("@StudentSalt", salt);
            return this.helperOledb.Insert(sql) > 0;
        }
        private string GetHash(string passwoed, string salt)
        {
            string combine = passwoed + salt;
            byte[] bytes = System.Text.UTF8Encoding.UTF8.GetBytes(combine);
            using (SHA256  sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        private int GetRandom()
        {
            Random rnd = new Random();
            return rnd.Next(8,16);
        }
        private string GetSalt(int lenght)
        {
            byte[] bytes= new byte[lenght];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
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
            string sql = @"Select StudentSalt,StudentId,password fron Student 
                 where StudentNickName =@StudentNickName";
                 
            this.helperOledb.AddParameter("@StudentNickName",nickName);

            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                if (reader.Read() == true)
                {
                    string salt = reader["StudentSalt"].ToString();
                    string hash= reader["StudentSalt"].ToString();
                    string calculateHash =GetHash(password, salt);
                    if(hash==calculateHash)
                          return reader["ReaderId"].ToString();
                }
                    
                return null;
            }


        }
    }
}
