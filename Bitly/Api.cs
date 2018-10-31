using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitly
{
    public class Api
    {
        private string Login { get; set; }
        private string Password { get; set; }

        public Api(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public async Task<string> LongLinkToSmall(string link)
        {
            return string.Empty;
        }
    }
}