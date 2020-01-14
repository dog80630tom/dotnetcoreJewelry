using Jewelry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry.Respository
{
    public interface ISSO
    {
        GetTokenFromCodeResult GetTokenFromCodeResult(string code, string ClientId, string ClientSecret, string redirect_uri);
        UserInfoResult GetUserInfo(string token);
    }
}
