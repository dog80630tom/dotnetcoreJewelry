using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry.Services
{
    public static class LoginCompare
    {
        public static int Compare(string Googlecode,string Linecode) {
            if (Googlecode != "") return 1;
            else if (Linecode != "") return 2;
            else return 0;
        

        }


    }
}
