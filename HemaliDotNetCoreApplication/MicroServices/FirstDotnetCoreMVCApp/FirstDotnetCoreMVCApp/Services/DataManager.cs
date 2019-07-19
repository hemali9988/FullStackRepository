using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstDotnetCoreMVCApp.Services
{
    public class DataManager:IDataManager
    {
        public string GetMessage()
        {
            return "Hello from DataManager";
        }
    }
}