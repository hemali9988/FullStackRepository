using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCApplication.Services
{
    public class EntityDataManager:IDataManager
    {
        public string GetMessage()
        {
            return "Hello Entity Data Manager!";
        }
    }
}
