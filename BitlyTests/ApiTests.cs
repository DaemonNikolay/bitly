using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bitly;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Net.Http;

namespace BitlyTests
{
    [TestClass]
    public class ApiTests
    {
        private const string login = "kinderny";
        private const string apiKey = "R_5bfe3e1a2f944de7b65a3c33f41546b9";

        [TestMethod]
        public async Task LongLinkToSmallAsync()
        {
            var api = new Api(login, apiKey);
            var expected = await api.LongLinkToSmallAsync("http://nikulux.ru");

            Assert.AreEqual(expected: expected, actual: "https://bitly.com");
        }

        [TestMethod]
        public async Task LongLinkToSmallAsyncTrueAsync()
        {
            var api = new Api(login, apiKey);
            var expected = await api.LongLinkToSmallAsync("http://nikulux.ru");

            Assert.AreEqual(expected: expected, actual: "http://nikulux.ru/");
        }

        [TestMethod]
        public async Task LongLinkToSmallRequestAsync()
        {
            var api = new Api(login, apiKey);
            var smallUrl = await api.LongLinkToSmallAsync("http://nikulux.ru");

            var expected = string.Empty;
            using (var webClient = new WebClient())
            {
                var uri = new Uri(smallUrl);
                var response = await webClient.UploadValuesTaskAsync(uri, new NameValueCollection());
                var result = Encoding.UTF8.GetString(response);

                expected = webClient.ResponseHeaders["link"];
            }

            Assert.IsTrue(expected != null && expected.Contains("http://nikulux.ru/"));
        }
    }
}