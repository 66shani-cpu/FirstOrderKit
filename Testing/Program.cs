using FirstOrderKitModel;
using System.Text.Json;

namespace Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TestSubject();
            Weather();
            Console.ReadLine();
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

        static async Task Weather()
        {
            Console.Write("Insert latitude width ");
            string latitude = Console.ReadLine();
            Console.Write("Insert longitude length ");
            string longitude =Console.ReadLine();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://easy-weather1.p.rapidapi.com/daily/5?latitude=31.93&longitude=34.87"),
                Headers =
    {
        { "x-rapidapi-key", "5c7fb314b8msh555f6b6e488e2fbp1880f2jsnf6c5176b4258" },
        { "x-rapidapi-host", "easy-weather1.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Weather weather = JsonSerializer.Deserialize<Weather>(body);
                Console.WriteLine(weather.forecastDaily.Days[0].temperatureMin);
                Console.WriteLine(weather.forecastDaily.Days[0].temperatureMax);
                Console.WriteLine(weather.forecastDaily.Days[0].precipitationType);
                Console.WriteLine(body);
            }
        }
    }
}