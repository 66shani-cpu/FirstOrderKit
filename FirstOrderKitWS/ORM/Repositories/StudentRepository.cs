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
            //    //    string sql = @$"Insert into Student 
            //    //                   (
            //    //                     StudentId,StudentNickName, [Password], StudentLastName, 
            //    //                     UnitId,StudentFirstName,CityId,StudentTelephone,
            //    //                     StudentAdrres,StudentImage,StudentActive,StudentIsManager
            //    //                   )
            //    //                   values
            //    //                     (
            //    //                          '{model.StudentId}','{model.StudentNickName}','{model.Password}',
            //    //                          '{model.StudentLastName}',{model.UnitId},
            //    //                          '{model.StudentFirstName}',{model.CityId},'{model.StudentTelephone}',
            //    //                          '{model.StudentAdrres}','{model.StudentImage}',True,False
            //    //                      )
            //    //                                             ";

            //    string sql = @$"Insert into Student 
            //                   (
            //                     StudentId,StudentNickName, [Password], StudentLastName, 
            //                     UnitId,StudentFirstName,CityId,StudentTelephone,
            //                     StudentAdrres,StudentImage,StudentSalt,StudentActive,StudentIsManager
            //                   )
            //                   values
            //                     (
            //                         @StudentId,@StudentNickName,@Password,
            //                         @StudentLastName,@UnitId,@StudentFirstName,
            //                         @CityId,@StudentTelephone,@StudentAdrres,@StudentImage,@StudentSalt,True,False)";
            //    string salt = GetSalt(GetRandom());
            //    string hashedPassword = GetHash(model.Password, salt);
            //    model.Password = hashedPassword;
            //    model.StudentSalt = salt;
            //    this.helperOledb.AddParameter("@StudentId", model.StudentId);
            //    this.helperOledb.AddParameter("@StudentNickName", model.StudentNickName);
            //    this.helperOledb.AddParameter("@Password",model.Password);
            //    this.helperOledb.AddParameter("@StudentLastName", model.StudentLastName);
            //    this.helperOledb.AddParameter("@UnitId", model.UnitId);
            //    this.helperOledb.AddParameter("@StudentFirstName", model.StudentFirstName);
            //    this.helperOledb.AddParameter("@CityId", model.CityId);
            //    this.helperOledb.AddParameter("@StudentTelephone", model.StudentTelephone);
            //    this.helperOledb.AddParameter("@StudentAdrres", model.StudentAdrres);
            //    this.helperOledb.AddParameter("@StudentImage", model.StudentImage);
            //    this.helperOledb.AddParameter("@StudentSalt", model.StudentSalt);
            //    this.helperOledb.AddParameter("@StudentActive", true);
            //    this.helperOledb.AddParameter("@StudentIsManager", false);
            //    return this.helperOledb.Insert(sql) > 0;
            string sql = @"Insert into Student 
               (
                 [StudentId],[UnitId], [StudentNickName], [Password],[StudentFirstName],
                 [StudentLastName],[CityId], [StudentTelephone],
                 [StudentAdrres], [StudentImage], [StudentSalt], [StudentNum], [StudentActive], [StudentIsManager]
               )
               values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            string salt = GetSalt(GetRandom());
            string hashedPassword = GetHash(model.Password, salt);
            model.Password = hashedPassword;
            model.StudentSalt = salt;

            this.helperOledb.AddParameter("@StudentId", model.StudentId);
            this.helperOledb.AddParameter("@UnitId", model.UnitId);
            this.helperOledb.AddParameter("@StudentNickName", model.StudentNickName);
            this.helperOledb.AddParameter("@Password", model.Password);
            this.helperOledb.AddParameter("@StudentFirstName", model.StudentFirstName);
            this.helperOledb.AddParameter("@StudentLastName", model.StudentLastName);            
            this.helperOledb.AddParameter("@CityId", model.CityId);
            this.helperOledb.AddParameter("@StudentTelephone", model.StudentTelephone);
            this.helperOledb.AddParameter("@StudentAdrres", model.StudentAdrres);
            this.helperOledb.AddParameter("@StudentImage", model.StudentImage);
            this.helperOledb.AddParameter("@StudentSalt", model.StudentSalt);
            this.helperOledb.AddParameter("@StudentNum", 0);
            this.helperOledb.AddParameter("@StudentActive", -1);
            this.helperOledb.AddParameter("@StudentIsManager", 0);
            return this.helperOledb.Insert(sql) > 0;
        }

        private string GetHash(string password, string salt)
        {
            string combine = password + salt;
            byte[] bytes = System.Text.UTF8Encoding.UTF8.GetBytes(combine);
            using (SHA256  sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        private string GetSalt(int lenght)
        {
            byte[] bytes= new byte[lenght];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }
        private int GetRandom()
        {
            Random rnd = new Random();
            return rnd.Next(8, 16);
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
            string sql = " Select * from Student where StudentActive=true";
          
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
        public string GetUnitNameByStudentId(string studentId)
        {
            string sql = @"SELECT Units.UnitName 
                    FROM Student 
                    INNER JOIN Units ON Student.UnitId = Units.UnitId
                    WHERE Student.StudentId = ?";

            this.helperOledb.AddParameter("StudentId", studentId);

            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                if (reader.Read())
                {
                    return reader["UnitName"].ToString();
                }
                return "";
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
        public bool Active(string studentId)
        {
            string sql = $@"UPDATE Student 
                SET StudentActive = False 
                WHERE StudentId = @StudentId";
            this.helperOledb.AddParameter("@StudentId", studentId);
            return this.helperOledb.Update(sql) > 0;

        }
        //הזדהות במערכת
        public string LogIn (string nickName, string password)
        {
            string sql = @"Select StudentSalt,StudentId,password from Student 
                 where StudentNickName =@StudentNickName";
                 
            this.helperOledb.AddParameter("@StudentNickName",nickName);

            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                if (reader.Read() == true)
                {
                    string salt = reader["StudentSalt"].ToString();
                    string hash= reader["Password"].ToString();
                    string calculateHash =GetHash(password, salt);
                    if(hash==calculateHash)
                          return reader["StudentId"].ToString();
                }
                    
                return null;
            }
        }
        public string LogInManager(string nickName, string password)
        {
            string sql = @$"Select StudentSalt,StudentId,[password] from Student 
                            where StudentNickName='{nickName}' AND 
                            StudentIsManager=true";

            this.helperOledb.AddParameter("@StudentNickName", nickName);

            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                if (reader.Read() == true)
                {
                    string salt = reader["StudentSalt"].ToString();
                    string hash = reader["Password"].ToString();
                    string calculateHash = GetHash(password, salt);
                    if (hash == calculateHash)
                        return reader["StudentId"].ToString();
                }

                return null;
            }
        }
        public bool UpdateImageName(string studentId, string fileName)
        {
            string sql = $@"Update Student set
                              StudentImage=@StudentImage 
                              where StudentId=@StudentId";
            this.helperOledb.AddParameter("@StudentImage", fileName);
            this.helperOledb.AddParameter("@StudentId", studentId);
            return this.helperOledb.Update(sql) > 0;
        }

    }
}
