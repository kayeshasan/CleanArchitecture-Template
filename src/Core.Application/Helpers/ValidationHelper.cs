using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Application.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress mail = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
        }

        public static bool validMobileNo(string telNo)
        {
            return Regex.Match(telNo, @"^\+\d{1,12}$").Success;
        }

        public static bool IsValidURL(string URL)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(URL);
        }

        public static bool IsValidFileType(IFormFile file, string[] validExtensions)
        {
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!validExtensions.Contains(extension.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

