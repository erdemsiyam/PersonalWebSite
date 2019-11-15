using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.App_Classes
{
    /*
        Blog başlıkları seoUrl haline getirilir
            {ingilizce hale gelmiş bloş başlığı}-{YILAYGÜN}
         */
    public static class MySeoUrl
    {
        public static string getUrl(string title, bool wantDate)
        {

            title = title.Replace("#", "sharp"); // SPECIAL OPTION. Because # char, not work at url. so we using this word.
            title = title.Replace("?", "q");
            title = title.Replace(".", "dot");
            title = title.Replace(' ', '-');
            title = title.ToLower();
            if (title.Length > 50)
            {
                title = title.Substring(0, 50);
                title = title.Substring(0, title.LastIndexOf('-')-1);
            }
            title = toEnglish(title);

            if(wantDate)
                title += getDate();

            return title;
        }
        private static string toEnglish(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
        private static string getDate()
        {
            return "-"+DateTime.Now.ToString("yyMMdd");
        }
    }
}