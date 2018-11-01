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
        private readonly Api api = new Api(login, apiKey);

        [TestMethod]
        public void CheckAuthDataTrueApiKey()
        {
            const string login = "login";
            const string apiKey = "apiKey";

            var api = new Api(login: login, apiKey: apiKey);

            Assert.AreEqual(api.ApiKey, apiKey, message:"Api key is correct");
        }

        [TestMethod]
        public void CheckAuthDataTrueLogin()
        {
            const string login = "login";
            const string apiKey = "apiKey";

            var api = new Api(login: login, apiKey: apiKey);

            Assert.AreEqual(api.Login, login, message: "Login is correct");
        }

        [TestMethod]
        public void CheckAuthDataNull()
        {
            const string login = null;
            const string apiKey = null;

            Assert.ThrowsException<ArgumentException>(() => new Api(login: login, apiKey: apiKey), "Login or password is null");
        }

        [TestMethod]
        public void CheckAuthDataEmpty()
        {
            var login = string.Empty;
            var apiKey = string.Empty;

            Assert.ThrowsException<ArgumentException>(() => new Api(login: login, apiKey: apiKey), "Login or password not correct");
        }

        [TestMethod]
        public async Task LongLinkToSmallTrueAsync()
        {
            var smallUrl = await api.LongLinkToSmallAsync("http://nikulux.ru");

            var finalUrl = string.Empty;
            using (var webClient = new WebClient())
            {
                var uri = new Uri(smallUrl);
                var response = await webClient.UploadValuesTaskAsync(uri, new NameValueCollection());
                var result = Encoding.UTF8.GetString(response);

                finalUrl = webClient.ResponseHeaders["link"];
            }

            var condition = finalUrl.Contains("http://nikulux.ru/");

            Assert.IsTrue(condition: condition, message: "Small link is correct");
        }

        [TestMethod]
        public async Task LongLinkToSmallNullAsync()
        {
            var expected = await api.LongLinkToSmallAsync(null);

            Assert.IsNull(value: expected, message: "Small link is null");
        }

        [TestMethod]
        public async Task LongLinkToSmallEmptyAsync()
        {
            var expected = await api.LongLinkToSmallAsync(string.Empty);

            Assert.IsNull(value: expected, message: "Small link is string empty");
        }

        [TestMethod]
        public async Task LongLinkToSmallFalseAsync()
        {
            var expected = await api.LongLinkToSmallAsync("fdsfds");

            Assert.IsNull(value: expected);
        }
    }
}