using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbschlussprojektWPF
{
    class Browser
    {
        private string browserDetails = string.Empty;

        public Browser()
        {


        }
        public static string GetBrowserDetails()
        {
            string browserDetails = string.Empty;

            browserDetails = "test";
            //System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            //browserDetails =
            //"Name = " + browser.Browser + "," +
            //"Type = " + browser.Type + ","
            //+ "Version = " + browser.Version + ",";
            return browserDetails;

        }
    }
}
