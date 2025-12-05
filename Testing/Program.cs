using FirstOrderKitModel;
using FirstOrderKitWS;
using FirstOrderKitWS.ORM.Repositories;
using System.Data;
using System.Security.Cryptography;
using System.Text.Json;

namespace Testing
{
    internal class Program

    {
        static void CheckCreater()
        {
            string sql = "Select * from Student where StudentId='2'";
            DBHelperOledb dBHelperOledb = new DBHelperOledb();
            dBHelperOledb.OpenConnection();
            IDataReader dataReader= dBHelperOledb.Select(sql);
            dataReader.Read();
            ModelCreaters moodelCreators = new ModelCreaters();
            Student student = moodelCreators.StudentCreator.CreateModel(dataReader);
            dBHelperOledb.CloseConnection();
            Console.WriteLine($"{student.StudentFirstName} {student.StudentLastName}");
        }
        static void Main(string[] args)
        {
            for (int i =1; i<=10; i++)
            {
                Console.WriteLine("insert password");
                string password = Console.ReadLine();
                string salt = GetSalt(8);
                string hash= GetHash(password, salt);
                Console.WriteLine(salt);
                Console.WriteLine( hash);
            }
           

            
            Console.ReadLine();

        }
        static string GetHash(string passwoed, string salt)
        {
            string combine = passwoed + salt;
            byte[] bytes = System.Text.UTF8Encoding.UTF8.GetBytes(combine);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        static string GetSalt(int lenght)
        {
            byte[] bytes = new byte[lenght];
            RandomNumberGenerator.Fill(bytes);
            string s = Convert.ToBase64String(bytes);
            return s;
        }
            
        //static void Test
        static void TestRepositorAdddQuiestion()
        {
            RepositoryUOF repositoryUOF = new RepositoryUOF();
            repositoryUOF.DBHelperOledb.OpenConnection();
            Question question = new Question();
            question.LevelQuestions = "2";
            question.QuestionText = "Ndwdnek";
            bool ok = repositoryUOF.QuestionRepository.Create(question);
            repositoryUOF.DBHelperOledb.CloseConnection();
            Console.WriteLine($"{ok}");

        }
        static void TestRepositorAddQuestion()
        {
            RepositoryUOF repositoryUOF = new RepositoryUOF();
            repositoryUOF.DBHelperOledb.OpenConnection();
            List<Question> questions= repositoryUOF.QuestionRepository.GetLevelQuestion("1");
            repositoryUOF.DBHelperOledb.CloseConnection();
            foreach (Question Question in questions)
            {
                Console.WriteLine($"{Question.QuestionText}");
            }

        }
        //static void TestRepositorFilterFirst()
        //{
        //    RepositoryUOF repositoryUOF = new RepositoryUOF();
        //    repositoryUOF.DBHelperOledb.OpenConnection();
        //    List<Subject> subjects= repositoryUOF.SubjectRepository.SubjectFilter("A");
        //    repositoryUOF.DBHelperOledb.CloseConnection();
        //    foreach (Subject Subject in subjects)
        //    {
        //        Console.WriteLine($"{Subject.SubjectName}");
        //    }

        //}
        static void TestRepositorDelete()
        {
            RepositoryUOF repositoryUOF = new RepositoryUOF();
            repositoryUOF.DBHelperOledb.OpenConnection();
            bool ok = repositoryUOF.AnswerRepository.Delete("1");
            repositoryUOF.DBHelperOledb.CloseConnection();
            Console.WriteLine($"{ok}");

        }
        static void TestRepositoryUpDate()
        {
            RepositoryUOF repositoryUOF = new RepositoryUOF();
            repositoryUOF.DBHelperOledb.OpenConnection();
            Student student = new Student();
            student.StudentId = "1";
            student.StudentTelephone = "1";
            student.StudentLastName = "Sfs";
            student.StudentFirstName = "Sdhbj";
            student.StudentNickName = "Asys";
            student.StudentImage = "1.jpg";
            student.Password = "234";
            student.StudentAdrres = "Dghh";
            student.CityId = 1;
            student.UnitId = 1;
            bool ok = repositoryUOF.StudentRepository.Update(student);
            repositoryUOF.DBHelperOledb.CloseConnection();
            Console.WriteLine($"{ok}");

        }

        static void TestRepositoryGetId()
        {
            RepositoryUOF repositoryUOF = new RepositoryUOF();
            repositoryUOF.DBHelperOledb.OpenConnection();
            Answer answers = repositoryUOF.AnswerRepository.GetById("3");
            repositoryUOF.DBHelperOledb.CloseConnection();
            Console.WriteLine($"{answers.AnswerText}");
            
        }
        static void TestRepositoryGetAll()
        {
            RepositoryUOF repositoryUOF = new RepositoryUOF();
            repositoryUOF.DBHelperOledb.OpenConnection();
            List<Answer> answers = repositoryUOF.AnswerRepository.GetAll();
            repositoryUOF.DBHelperOledb.CloseConnection();
            foreach (Answer answer in answers)
            {
                Console.WriteLine($"{answer.AnswerText}");
            }
        }

        static void TestSubject() 
        {
            Subject subject = new Subject();
            subject.SubjectId = "1";
            subject.SubjectName = "Test";
            if(subject.HasErrors==true)
            {
                foreach(KeyValuePair<string,List<string>> keyValuePair in subject.AllErrors())
                {
                    Console.WriteLine(keyValuePair.Key);
                    foreach(string str in keyValuePair.Value)
                    {
                        Console.WriteLine($"     {str}");
                    }
                    Console.WriteLine("==========================");
                }
            }
            else Console.WriteLine("There was no error");
        }

        static async Task Horskope()
        {
            Console.WriteLine("Enter week start");
            string start = Console.ReadLine();
            
            Console.WriteLine("Enter sign");
            string sign = Console.ReadLine();

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://horoscope-api-by-apirobots.p.rapidapi.com/v1/horoscopes/aries/weekly?week_start={start   }"),
                Headers =
    {
        { "x-rapidapi-key", "5c7fb314b8msh555f6b6e488e2fbp1880f2jsnf6c5176b4258" },
        { "x-rapidapi-host", "horoscope-api-by-apirobots.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Horskope horskope = JsonSerializer.Deserialize<Horskope>(body); 

                Console.WriteLine(horskope.overview);
            }
        }
        static void CheckInsert()
        {
            DBHelperOledb dBHelperOledb = new DBHelperOledb();
            Console.WriteLine("Insert subject");
            string Subject = Console.ReadLine();
            string sql = $"Insert into Subjects(SubjectName) values('{Subject}')";
            dBHelperOledb.OpenConnection();
           int count=  dBHelperOledb.Insert(sql);
            dBHelperOledb.CloseConnection();

            if (count > 0)
            {
                Console.WriteLine("ok");
            }
            else 
            {
                Console.WriteLine("Not ok");
            }
            dBHelperOledb.CloseConnection();
        }

    }
}