using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bitly
{
    public class Api
    {
        private string Login { get; set; }
        private string ApiKey { get; set; }

        public Api(string login, string apiKey)
        {
            if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("Login or password is empty", nameof(login));
            }

            Login = login;
            ApiKey = apiKey;
        }

        public async Task<string> LongLinkToSmall(string link)
        {
            var result = string.Empty;
            var url = $"http://api.bit.ly/v3/shorten?login={Login}&apiKey={ApiKey}&longUrl={link}&format=json";
            using (var webClient = new WebClient())
            {
                var uri = new Uri(url);
                var response = await webClient.UploadValuesTaskAsync(uri, new NameValueCollection());
                result = Encoding.UTF8.GetString(response);
            }

            return result;
        }
    }
}