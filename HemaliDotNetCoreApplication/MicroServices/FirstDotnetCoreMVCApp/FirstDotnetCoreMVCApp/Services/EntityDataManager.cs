using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstDotnetCoreMVCApp.Services
{
    public class EntityDataManager:IDataManager
    {
        public string GetMessage()
        {
            return "Hi Hello Entity manager";
        }
    }
}