using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Ionic.Zip;
using WebFontNpp.Model;
using fastJSON;

namespace WebFontNpp.Util
{
    public static class FontLoader
    {
        private const string FONTS_URL = "http://webfonts.ru/api/list.json";

        public static List<Font> LoadFonts()
        {
            try
            {
                var fontsJson = new WebClient().DownloadString(FONTS_URL);
                var fonts = JSON.Instance.ToObject<List<Font>>(fontsJson);

                Main.FontsListCache = fontsJson;

                return fonts;
            }
            catch (WebException)
            {
            }

            var cachedJson = Main.FontsListCache;
            var cachedFonts = JSON.Instance.ToObject<List<Font>>(cachedJson);
            return cachedFonts;

        }

        public static void UnpackFontToFolder(string selectedFolder, Font font)
        {
            var fontUrl = font.pack_url;
            var zipFileName = Path.GetTempFileName();
            new WebClient().DownloadFile(fontUrl, zipFileName);

            using (var zipFile = ZipFile.Read(zipFileName))
            {
                foreach (var zipEntry in zipFile.Entries)
                {
                    zipEntry.Extract(selectedFolder, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            File.Delete(zipFileName);
        }
    }
}
