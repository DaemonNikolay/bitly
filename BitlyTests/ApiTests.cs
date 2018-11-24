using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bitly;
using System.Threading.Tasks;
using System.Net;
using System;

namespace BitlyTests
{
    [TestClass]
    public class ApiTests
    {
        private const string login = "kinderny";
        private const string apiKey = "R_5bfe3e1a2f944de7b65a3c33f41546b9";
        private readonly Api api = new Api(login, apiKey);

        [TestMethod]
        public void CheckAuthDataTrueLogin()
        {
            const string login = "login";
            const string apiKey = "apiKey";

            var api = new Api(login: login, apiKey: apiKey);

            Assert.AreEqual(api.Login, login, message: "Login is correct");
        }

        [TestMethod]
        public void CheckAuthDataTrueApiKey()
        {
            const string login = "login";
            const string apiKey = "apiKey";

            var api = new Api(login: login, apiKey: apiKey);

            Assert.AreEqual(api.ApiKey, apiKey, message: "Api key is correct");
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

            Assert.ThrowsException<ArgumentException>(() => new Api(login: login, apiKey: apiKey), "Login or password is empty");
        }

        [TestMethod]
        public async Task LongLinkToSmallNikuluxAsync()
        {
            const string link = "http://nikulux.ru";
            var smallUrl = await api.LongLinkToSmallAsync(link);

            var req = (HttpWebRequest)WebRequest.Create(smallUrl);
            req.Method = "POST";

            var response = await req.GetResponseAsync();
            var expected = response.ResponseUri;

            var condition = expected.Host == "nikulux.ru";

            Assert.IsTrue(condition: condition, message: $"Link {link} correct, domen .ru");
        }

        [TestMethod]
        public async Task LongLinkToSmallGoogleAsync()
        {
            const string link = "https://google.com";
            var smallUrl = await api.LongLinkToSmallAsync(link);

            var req = (HttpWebRequest)WebRequest.Create(smallUrl);
            req.Method = "POST";

            var response = await req.GetResponseAsync();
            var expected = response.ResponseUri;

            var condition = expected.Host == "www.google.com";

            Assert.IsTrue(condition: condition, message: $"Link {link} correct and redirect, domen .com and prefix 'www'");
        }

        [TestMethod]
        public async Task LongLinkToSmallYandexAsync()
        {
            const string link = "https://yandex.ru";
            var smallUrl = await api.LongLinkToSmallAsync(link);

            var req = (HttpWebRequest)WebRequest.Create(smallUrl);
            req.Method = "POST";

            var response = await req.GetResponseAsync();
            var expected = response.ResponseUri;

            var condition = expected.Host == "yandex.ru";

            Assert.IsTrue(condition: condition, message: $"Link {link} correct, , domen .ru");
        }

        [TestMethod]
        public async Task LongLinkToSmallBitlyAsync()
        {
            const string link = "https://bitly.com/";
            var smallUrl = await api.LongLinkToSmallAsync(link);

            var req = (HttpWebRequest)WebRequest.Create(smallUrl);
            req.Method = "POST";

            var response = await req.GetResponseAsync();
            var expected = response.ResponseUri;

            var condition = expected.Host == "bitly.com";

            Assert.IsTrue(condition: condition, message: $"Link {link} correct, domen .com");
        }

        [TestMethod]
        public async Task LongLinkToSmallRedirectAsync()
        {
            const string link = "https://pinterest.ru/";
            var smallUrl = await api.LongLinkToSmallAsync(link);

            var req = (HttpWebRequest)WebRequest.Create(smallUrl);
            req.Method = "POST";

            var response = await req.GetResponseAsync();
            var expected = response.ResponseUri;

            var condition = expected.Host == "www.pinterest.ru";

            Assert.IsTrue(condition: condition, message: $"Link {link} correct and redirect, domen .ru and prefix 'www'");
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