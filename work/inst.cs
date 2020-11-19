using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using AngleSharp;
using Leaf.xNet;

namespace InstLiker
{
    class inst
    {
        static HttpRequest httpRequest = new HttpRequest();
        static RequestParams Params = new RequestParams();

        /// <summary>
        /// Получаем страницу авторизации
        /// </summary>
        /// <returns></returns>
        public static string getPageAuth()
        {
            httpRequest = new HttpRequest();
            string response = httpRequest.Get("https://www.instagram.com/accounts/login/?hl=ru").ToString(); // получить код элемента
            return response;
        }

        /// <summary>
        /// получить параметры
        /// </summary>
        /// <returns></returns>
        private static string[] ParsParams()
        {
            string response = getPageAuth();
            return new string[]{
                System.Text.RegularExpressions.Regex.Match(response,"rollout_hash\":\"(.*?)\"").Groups[1].Value,
                System.Text.RegularExpressions.Regex.Match(response,"csrf_token\":\"(.*?)\"").Groups[1].Value,
            };
        }

        /// <summary>
        /// авторизация инстаграм
        /// </summary>
        /// <returns></returns>
        public static string Auth(string Login, string Password)
        {
            string[] Data = ParsParams();

            httpRequest = new HttpRequest();
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";
            httpRequest.KeepAlive = true;
            httpRequest.Cookies = new CookieStorage(true);
            httpRequest.AddHeader(HttpHeader.Accept, "*/*");
            httpRequest.AddHeader("accept-encoding", "gzip, deflate");
            httpRequest.AddHeader(HttpHeader.AcceptLanguage, "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            httpRequest.AddHeader(HttpHeader.ContentLength, "314");
            httpRequest.AddHeader(HttpHeader.ContentType, "application/x-www-form-urlencoded");
            httpRequest.AddHeader(HttpHeader.Origin, "https://www.instagram.com");
            httpRequest.AddHeader(HttpHeader.Referer, "https://www.instagram.com/accounts/login/?hl=ru");
            httpRequest.AddHeader("Sec-Fetch-Dest", "empty");
            httpRequest.AddHeader("Sec-Fetch-Mode", "cors");
            httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
            httpRequest.AddHeader("X-IG-App-ID", "936619743392459");
            httpRequest.AddHeader("x-ig-www-claim", "hmac.AR1Clsr4IBaE5fbo0d97oMx8hqNCCJCgtmtHmD9_NTYEGDAA");
            httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            httpRequest.AddHeader("x-instagram-ajax", Data[0]);
            httpRequest.AddHeader("X-CSRFToken", Data[1]);


            Params = new RequestParams();
            Params["username"] = Login;
            Params["enc_password"] = "#PWD_INSTAGRAM_BROWSER:9:1605777807:AVdQAMNvt98bqnGmCXdqowRZ6WkkiPF3iBw6SATdawjZohEmJ1UXwhVPkjsBqBmH5nyCsrMQ2DkqS+Te86t2/z8dxVB7Gd9HB7QklH+bdC8eW8+QAAzFtLvfIRfiEFip0JCIRcvwWEt0NoJEgusDl6WM6uQuq4FqAiNBCWk8";
            Params["queryParams"] = "{\"hl\":\"ru\"}";
            Params["optIntoOneTap"] = "false";

            var response = httpRequest.Post("https://www.instagram.com/accounts/login/ajax/?hl=ru", Params);
            return response.ToString();
        }
    }

    public class Auth
    {
        public bool authenticated { get; set; }
        public bool user { get; set; }
        public string userId { get; set; }
        public bool oneTapPrompt { get; set; }
        public string status { get; set; }



    }


}
