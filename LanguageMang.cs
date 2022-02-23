using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
namespace marble
{
    public class LanguageMang
    {
        public static List<Languages> AvailableLanguages = new List<Languages> {
             new Languages {
                LanguageFullName = "Ar", LanguageCultureName = "ar"
            }, new Languages {
                LanguageFullName = "En", LanguageCultureName = "en"
            }
          
        };
        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }
        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0].LanguageCultureName;
        }
        public void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture", lang);
                //langCookie.Expires = DateTime.Parse("2024-02-07T22:45:45.000Z");

                langCookie.Expires = Convert.ToDateTime(DateTime.Now.AddYears(1));
                HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
    public class Languages
    {
        public string LanguageFullName
        {
            get;
            set;
        }
        public string LanguageCultureName
        {
            get;
            set;
        }
    }
}