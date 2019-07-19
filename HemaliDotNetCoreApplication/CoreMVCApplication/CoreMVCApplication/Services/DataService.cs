using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCApplication.Services
{
    public class DataService
    {
        private string message;

        public void SetMessage(string msg)
        {
            this.message = msg;
        }

        public string GetMessage()
        {
            return this.message;
        }
    }
}
