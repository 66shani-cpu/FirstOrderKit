using FirstOrderKitModel;
using FirstOrderKitWS;
using System.Text.Json;

namespace Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TestSubject();
            //Horskope();
            //Console.ReadLine();
            CheckInsert();
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
        static async Task CurrenctList()
        {
            //await Console.Out.WriteLineAsync();
            Console.WriteLine("insert...");
            string from = Console.ReadLine();
            string to = Console.ReadLine();
            string amount = Console.ReadLine();

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://countries59.p.rapidapi.com/list_countries" ),
                Headers =
    {
        { "x-rapidapi-key", "5c7fb314b8msh555f6b6e488e2fbp1880f2jsnf6c5176b4258" },
        { "x-rapidapi-host", "countries59.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Currency carr = JsonSerializer.Deserialize<Currency>(body);

                //Console.WriteLine($"{carr.query.amount}{carr.query.from}") ={carr.result}{carr.query.to;
            }
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