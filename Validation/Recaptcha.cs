using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ContactUs.Validation
{
    public class Recaptcha
    {
        readonly static string PRIVATE_KEY = "6LcsgWoUAAAAAFStzbGAdUwi3e97NxOS0xKiEJPe";

        public static bool Validate(string EncodedResponse)
        {
            var client = new WebClient();
            var result = client.DownloadString(string.Format(
                                                             "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}"
                                                             , PRIVATE_KEY
                                                             , EncodedResponse
                                                             ));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            return status ? true : false;
        }
    }
}