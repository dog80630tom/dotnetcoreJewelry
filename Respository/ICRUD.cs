using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry.Respository
{
    public interface ICRUD<T>
    {
        bool Crate(T enity);
        bool Update(T enity);
        bool Delete(T enity);

    }
}
