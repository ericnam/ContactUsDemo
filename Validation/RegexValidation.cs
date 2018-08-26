using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ContactUs.Validation
{
    public class RegexValidation
    {
        /// <summary>
        ///     Check through regex if email address is valid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(string input)
        {
            Regex emailAddress = new Regex("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");

            if (!String.IsNullOrEmpty(input) && emailAddress.IsMatch(input) == false)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Check if input is alphabetic
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAlphabetic(string input)
        {
            Regex alphabetic = new Regex(@"^[a-zA-Z]+$");
            if (!String.IsNullOrEmpty(input) && alphabetic.IsMatch(input) == false)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Check through regex is value contains only numbers
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ContainsOnlyNumbers(string value)
        {
            //This makes sure a string only contains numeric characters
            Regex validCharacters = new Regex("^[0-9]{1,45}$");

            if (validCharacters.IsMatch(value))
            {
                return true;
            }
            return false;
        }
    }
}