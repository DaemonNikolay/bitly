using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bitly;
using System.Threading.Tasks;

namespace BitlyTests
{
    [TestClass]
    public class ApiTests
    {
        [TestMethod]
        public async Task LongLinkToSmallAsync()
        {
            var api = new Api("login", "password");
            var expected = await api.LongLinkToSmall("rew");

            Assert.AreEqual(expected: expected, actual: "https://bitly.com");
        }
    }
}