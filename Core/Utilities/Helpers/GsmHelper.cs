using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Utilities.Helpers
{
    public static class GsmHelper
    {
        public static void CorrectGsm(ref string gsm)
        {
            if (string.IsNullOrEmpty(gsm)) return;
            if (!gsm.StartsWith("+")) gsm = "+" + gsm;
            gsm = Regex.Replace(gsm, @"\s", "");
        }

        public static string CorrectGsm(string gsm)
        {
            if (string.IsNullOrEmpty(gsm)) return "";
            if (!gsm.StartsWith("+")) gsm = "+" + gsm;
            gsm = Regex.Replace(gsm, @"\s", "");
            return gsm;
        }

        public static string CorrectGsmWithCountryCode(string gsm, string defaultCountry)
        {
            if (string.IsNullOrEmpty(gsm)) return "";
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            gsm = gsm.Replace("(", "");
            gsm = gsm.Replace(")", "");
            gsm = gsm.Replace("-", "");
            gsm = Regex.Replace(gsm, @"\s", "");
            if (!gsm.StartsWith("+"))
            {
                try
                {
                    var phoneNumber = phoneNumberUtil.Parse(gsm, defaultCountry);
                    return "+" + phoneNumber.CountryCode + phoneNumber.NationalNumber;
                }
                catch (Exception)
                {
                    return "<null/>";
                }
            }
            else
            {
                if (gsm.StartsWith("+0"))
                {
                    gsm = gsm.Replace("+", "");
                    gsm = "+9" + gsm;
                    return gsm;
                }
                return gsm;
            }
        }
    }
}
