using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactUs.Validation;

namespace ContactUs.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            return View(EmptyContact());
        }

        /// <summary>
        ///     Contact us form submission
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection collection)
        {
            Contact contact = new Contact();
            TryUpdateModel(contact);
            contact.BestTimeToCall = ConvertTimeInput(collection["Hour"], collection["Minute"], collection["Meridiem"]);

            bool success = false;
            Dictionary<String, String> disclaimers = new Dictionary<string, string>();

            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool IsCaptchaValid = EncodedResponse != null ? (Recaptcha.Validate(EncodedResponse)) : false;

            Dictionary<String, List<String>> validationErrors = ContactValidation.Validate(contact);
            ViewData["ValidationErrors"] = validationErrors;

            if (validationErrors.Count == 0)
            {
                if (IsCaptchaValid)
                {
                    using (var db = new ContactUsEntities())
                    {
                        db.Contacts.Add(contact);
                        db.SaveChanges();
                        success = true;
                        disclaimers.Add("success", "Your form has been successfully submitted!");
                    }
                }
                else
                {
                    disclaimers.Add("warning", "ReCAPTCHA is invalid. Please try reCAPTCHA again!");
                }
            }

            ViewData["Disclaimers"] = disclaimers;
            return success ? View(EmptyContact()) : View(contact);
        }
        
        /// <summary>
        ///     Convert individual time inputs into one DateTime
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="min"></param>
        /// <param name="meridiem"></param>
        /// <returns></returns>
        public DateTime ConvertTimeInput(string hour, string min, string meridiem)
        {
            if (String.IsNullOrEmpty(hour))
                hour = "00";
            if (String.IsNullOrEmpty(min))
                min = "00";
            string time = hour + ":" + min + meridiem;
            return Convert.ToDateTime(time);
        }
        
        /// <summary>
        ///     Create empty contact for new form loads
        /// </summary>
        /// <returns></returns>
        public Contact EmptyContact()
        {
            Contact contact = new Contact();
            contact.BestTimeToCall = Convert.ToDateTime("9:00:00AM");
            return contact;
        } 
    }
}