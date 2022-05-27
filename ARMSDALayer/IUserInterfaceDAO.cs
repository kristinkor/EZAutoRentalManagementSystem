using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSDALayer
{
    interface IUserInterfaceDAO<T>
    {
        List<T> GetAllRecords();
    }
}