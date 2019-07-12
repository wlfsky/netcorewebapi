using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Common.Token
{
    public class DefaultInsideNetApiToken : IToken
    {
        private readonly string token = "fuckoff";
        public bool AuthenticationToken(string token)
        {
            return token.ToLower() == token;
        }

        public string RequestToken()
        {
            return token;
        }
    }
}
