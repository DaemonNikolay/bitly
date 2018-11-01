﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public string SmallUrl { get; private set; }

        public Api(string login, string apiKey)
        {
            if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("Login or password is empty", nameof(login));
            }

            Login = login;
            ApiKey = apiKey;
        }

        public async Task<string> LongLinkToSmallAsync(string link)
        {
            var responseJson = string.Empty;
            var apiUrl = $"http://api.bit.ly/v3/shorten?login={Login}&apiKey={ApiKey}&longUrl={link}&format=json";
            using (var webClient = new WebClient())
            {
                var uri = new Uri(apiUrl);
                var response = await webClient.UploadValuesTaskAsync(uri, new NameValueCollection());
                responseJson = Encoding.UTF8.GetString(response);
            }

            //var result = JsonConvert.DeserializeObject<BitlyJson>(responseJson);

            var result = JObject.Parse(responseJson);
            var statusCode = (int)result["status_code"];

            if (statusCode == 200)
            {
                SmallUrl = result["data"]["url"].ToString();
                return SmallUrl;
            }

            return $"Status code: {statusCode}";
        }

        private class BitlyJson
        {
            public int status_code;
            public Data data;

            public class Data
            {
                public string url;
            }
        }
    }
}