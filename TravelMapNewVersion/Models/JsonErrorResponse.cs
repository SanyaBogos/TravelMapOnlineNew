using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMap.Models
{
    public class JsonErrorResponse
    {
        public string Error { get; set; }

        public JsonErrorResponse(string msg)
        {
            Error = msg;
        }
    }
}