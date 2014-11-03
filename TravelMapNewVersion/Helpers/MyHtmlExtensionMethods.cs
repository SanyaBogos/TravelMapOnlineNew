using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelMap.Helpers
{
    public static class MyHtmlExtensionMethods
    {
        public static IHtmlString Image(this HtmlHelper helper, string base64Image)
        {
            string htmlString = string.Format("<img src='data:image/jpg;base64,{0}' width='160' height='200' />", base64Image);
            return new HtmlString(htmlString);
        }
    }
}