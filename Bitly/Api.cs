using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Bitly
{
    public class Api
    {
        public string Login { get; private set; }
        public string ApiKey { get; private set; }

        public Api(string login, string apiKey)
        {
            if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("Login or password not correct", nameof(login));
            }

            Login = login;
            ApiKey = apiKey;
        }

        public async Task<string> LongLinkToSmallAsync(string link)
        {
            if (CheckForInternetConnection() && !IsCorrectLink(link) && string.IsNullOrEmpty(link))
            {
                return null;
            }


            var responseJson = string.Empty;
            var apiUrl = $"http://api.bit.ly/v3/shorten?login={Login}&apiKey={ApiKey}&longUrl={link}&format=json";
            using (var webClient = new WebClient())
            {
                var response = await webClient.UploadValuesTaskAsync(new Uri(apiUrl), new NameValueCollection());
                responseJson = Encoding.UTF8.GetString(response);
            }

            var result = JObject.Parse(responseJson);
            if ((int)result["status_code"] == 200)
            {
                return result["data"]["url"].ToString(); ;
            }

            return null;
        }

        private static bool IsCorrectLink(string link)
        {
            return Uri.IsWellFormedUriString(link, UriKind.Absolute);
        }

        public static bool CheckForInternetConnection()
        {
            using (var ping = new Ping())
            {
                const string host = "google.com";
                var buffer = new byte[32];
                const int timeout = 1000;
                var options = new PingOptions();
                try
                {
                    var reply = ping.Send(host, timeout, buffer, options);

                    return reply.Status == IPStatus.Success;
                }
                catch (PingException)
                {
                    return false;
                }
            }
        }
    }
}