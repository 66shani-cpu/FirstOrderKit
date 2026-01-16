using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FirstKitWSClient
{
    public class ApiClient<T>
    {
        HttpClient httpClient = FirstKitHTTPClient.Instance;
        //פרטי קשר בלי טעויות בונה את הקשר לווב סרוויס
        UriBuilder uriBuilder = new UriBuilder();

        public string Schema
        {
            set
            {
                this.uriBuilder.Scheme= value;
            }
        }
        public string Host
        {
            set
            {
                this.uriBuilder.Host = value;
            }
        }
        public int Port
        {
            set
            {
                this.uriBuilder.Port = value;
            }
        }

        //לאיזה קבצים לנווט בקשה
        public string Path
        {
            set
            {
                this.uriBuilder.Path = value;
            }
        }
        public void AddParameter(string key, string value)
        {
            if (this.uriBuilder.Query==string.Empty)
            {
                this.uriBuilder.Query += "?";
            }
            else
            {
                this.uriBuilder.Query += "&";
            }
            this.uriBuilder.Query += $"{key}={value}";
        }
        //רוצה לקבל נתונים מWS
        public async Task<T> GetAsync()
        {
            //כדי שיימחק אחרי בזיכרון
            using (HttpRequestMessage httpRequest = new HttpRequestMessage())
            {
                //בונה בקשה ומוכן להישלח לשרת
                httpRequest.Method = HttpMethod.Get;
                httpRequest.RequestUri= this.uriBuilder.Uri;
                //שולחים בקשה לפי הכתובת שיצרנו
                using (HttpResponseMessage httpResponse = await this.httpClient.SendAsync(httpRequest))
                {
                    //קוד 200
                    if(httpResponse.IsSuccessStatusCode==true)
                    {
                        //תוכן מגיע כמחרוזת
                        string result = await httpResponse.Content.ReadAsStringAsync();
                        //שימוש בפירוש ולא איך שכתבו
                        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                        jsonSerializerOptions.PropertyNameCaseInsensitive= true;
                        jsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;

                        //דסרליזציה
                        T model =JsonSerializer.Deserialize<T>(result, jsonSerializerOptions);
                        return model;
                    }
                    //ברירת מחדל NULL
                    return default(T);
                }

            }

        }
        public async Task <bool> PostAsync (T model)
        {
            using (HttpRequestMessage httpRequest = new HttpRequestMessage())
            {
                httpRequest.Method = HttpMethod.Post;
                httpRequest.RequestUri = this.uriBuilder.Uri;
                string json = JsonSerializer.Serialize<T>(model);
                StringContent content = new StringContent(json);
                httpRequest.Content = content;
                using(HttpResponseMessage responseMessage= await this.httpClient.SendAsync(httpRequest))
                {
                   return responseMessage.IsSuccessStatusCode == true;

                }

            }
        }
        public async Task<bool> PostAsync(T model,Stream file)
        {
            using (HttpRequestMessage httpRequest = new HttpRequestMessage())
            {
                httpRequest.Method = HttpMethod.Post;
                httpRequest.RequestUri = this.uriBuilder.Uri;
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                string json = JsonSerializer.Serialize<T>(model);
                StringContent modelcontent = new StringContent(json);
                multipartFormDataContent.Add(modelcontent, "model");
                StreamContent streamContent = new StreamContent(file);
                multipartFormDataContent.Add(modelcontent, "file", "file");
                httpRequest.Content = multipartFormDataContent;
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(httpRequest))
                {
                    return responseMessage.IsSuccessStatusCode == true;

                }

            }
        }

        public async Task<bool> PostAsync(T model, List<Stream> files)
        {
            using (HttpRequestMessage httpRequest = new HttpRequestMessage())
            {
                httpRequest.Method = HttpMethod.Post;
                httpRequest.RequestUri = this.uriBuilder.Uri;
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                string json = JsonSerializer.Serialize<T>(model);
                StringContent modelcontent = new StringContent(json);
                multipartFormDataContent.Add(modelcontent, "model");
                foreach (FileStream fileStream in files)
                {
                    StreamContent streamContent = new StreamContent(fileStream);
                    multipartFormDataContent.Add(modelcontent, "file", "file");
                }
                httpRequest.Content = multipartFormDataContent;
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(httpRequest))
                {
                    return responseMessage.IsSuccessStatusCode == true;

                }

            }
        }
    }
}
