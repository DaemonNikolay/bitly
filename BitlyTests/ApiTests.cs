using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bitly;
using System.Threading.Tasks;
using System.Threading;

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
            var expected = await api.LongLinkToSmall("http://nikulux.ru");

            Assert.AreEqual(expected: expected, actual: "https://bitly.com");
        }
    }
}