using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ContactUs.Validation
{
    public class ContactValidation
    {
        public static Dictionary<String, List<String>> Validate(Contact contact)
        {
            Dictionary<String, List<String>> validationErrors = new Dictionary<string, List<string>>();
            ValidateName(contact, ref validationErrors);
            ValidateEmail(contact, ref validationErrors);
            ValidatePhoneNumber(contact, ref validationErrors);
            ValidateTime(contact, ref validationErrors);
            return validationErrors;
        }

        public static void ValidateName(Contact contact, ref Dictionary<String, List<String>> validationErrors)
        {
            List<String> firstNameErrors = new List<string>();
            List<String> lastNameErrors = new List<string>();

            if (String.IsNullOrEmpty(contact.FirstName))
            {
                firstNameErrors.Add("First name cannot be blank");
            }
            if (!RegexValidation.IsAlphabetic(contact.FirstName))
            {
                firstNameErrors.Add("Please input a valid first name");
            }
            if (!String.IsNullOrEmpty(contact.FirstName) && contact.FirstName.Length > 40)
            {
                firstNameErrors.Add("First name cannot be over 40 characters");
            }
            if (String.IsNullOrEmpty(contact.LastName))
            {
                lastNameErrors.Add("Last name cannot be blank");
            }
            if (!RegexValidation.IsAlphabetic(contact.LastName))
            {
                firstNameErrors.Add("Please input a valid last name");
            }
            if (!String.IsNullOrEmpty(contact.LastName) && contact.LastName.Length > 40)
            {
                firstNameErrors.Add("Last name cannot be over 40 characterss");
            }

            if (firstNameErrors.Count > 0) validationErrors.Add("FirstName", firstNameErrors);
            if (lastNameErrors.Count > 0) validationErrors.Add("LastName", lastNameErrors);
        }

        public static void ValidateEmail(Contact contact, ref Dictionary<String, List<String>> validationErrors)
        {
            List<String> emailErrors = new List<string>();

            if (String.IsNullOrEmpty(contact.EmailAddress))
            {
                emailErrors.Add("Email cannot be blank");
            }
            if (!RegexValidation.IsValidEmailAddress(contact.EmailAddress))
            {
                emailErrors.Add("Please enter a correctly formatted email address");
            }
            if (!String.IsNullOrEmpty(contact.EmailAddress) && contact.EmailAddress.Length > 80)
            {
                emailErrors.Add("Email length cannot exceed 80 characters");
            }

            if (emailErrors.Count > 0) validationErrors.Add("Email", emailErrors);
        }

        public static void ValidatePhoneNumber(Contact contact, ref Dictionary<String, List<String>> validationErrors)
        {
            List<String> phoneNumberErrors = new List<string>();
            if (!String.IsNullOrEmpty(contact.Telephone) && (contact.Telephone.Length < 9 || contact.Telephone.Length > 16))
            {
                phoneNumberErrors.Add("Please input a valid phone number");
            }
            if (phoneNumberErrors.Count > 0) validationErrors.Add("PhoneNumber", phoneNumberErrors);
        }

        public static void ValidateTime(Contact contact, ref Dictionary<String, List<String>> validationErrors)
        {
            List<String> timeErrors = new List<string>();

            string mm = contact.BestTimeToCall.Value.ToString("mm", CultureInfo.InvariantCulture);
            string[] acceptedMinutes = new string[] { "00", "15", "30", "45" };

            if (!acceptedMinutes.Contains(mm))
            {
                timeErrors.Add("Please select a time that falls in 15 minute increments.");
            }
            if (TimeSpan.Compare(contact.BestTimeToCall.Value.TimeOfDay, Convert.ToDateTime("9:00:00AM").TimeOfDay) == -1 ||
                TimeSpan.Compare(contact.BestTimeToCall.Value.TimeOfDay, Convert.ToDateTime("6:00:00PM").TimeOfDay) == 1)
            {
                timeErrors.Add("Please select a time that falls within our business hours!");
            }
            if (timeErrors.Count > 0) validationErrors.Add("Time", timeErrors);
        }
    }
}