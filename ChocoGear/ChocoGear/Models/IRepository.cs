using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models
{
    internal interface IRepository<T>
    {
        int Create(T item);
        int Update(T item);
        int Delete(int id);
        List<T> Gets();
        T GetId(int id);
    }
}