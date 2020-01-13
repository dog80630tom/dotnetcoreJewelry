using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry.Respository.MemberResp
{
   public interface IEncryption
    {
        string EncryptionMethod(string password, string name);
    }
}
