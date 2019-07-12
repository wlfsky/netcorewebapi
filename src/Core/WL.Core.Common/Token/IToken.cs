using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Common.Token
{
    public interface IToken
    {
        string RequestToken();
        bool AuthenticationToken(string token);
    }
}
