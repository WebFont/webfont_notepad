using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebFontNpp.Model;
using fastJSON;

namespace WebFontNpp.Util
{
    public static class FontLoader
    {
        private const string FONTS_URL = "http://webfonts.ru/api/list.json";

        public static List<Font> LoadFonts()
        {
            var fontsJson = new WebClient().DownloadString(FONTS_URL);
            var fonts = JSON.Instance.ToObject<List<Font>>(fontsJson);
            return fonts;
        } 

    }
}
