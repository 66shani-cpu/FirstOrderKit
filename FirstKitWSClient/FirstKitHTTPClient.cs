using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstKitWSClient
{
    public class FirstKitHTTPClient
    {
        private static HttpClient CreateClient()
        {
            //  שמוגבל בזמן קשר 
            SocketsHttpHandler handler = new SocketsHttpHandler();
            handler.PooledConnectionLifetime = TimeSpan.FromMinutes(10);
            handler.ConnectTimeout= TimeSpan.FromMinutes(15);
            return new HttpClient(handler);
        }
        //משתמשים כשאי אפשר ליצור אובייקטים מאותו טיפוס
        private static readonly HttpClient httpClient = CreateClient();
        //אי אפשר ליצור אובייקט
        private FirstKitHTTPClient() { }
        public static HttpClient Instance
        {
            get 
            {
                return httpClient;
            }
        }
    }
}
