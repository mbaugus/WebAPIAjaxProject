using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebAPIAjaxProject.Utility
{
    public class JsonMessage
    {
        public string Result { get; set; }
        public string Message { get; set; }

        public JsonMessage(string Result, string Message)
        {
            this.Result = Result;
            this.Message = Message;
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}