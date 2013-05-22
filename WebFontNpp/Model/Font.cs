using System;
using System.Collections.Generic;
using System.Text;

namespace WebFontNpp.Model
{
    [Serializable]
    public class Font
    {
        public string import { get; set; }
        public string name { get; set; }
        public string comments { get; set; }
        public string pack_url { get; set; }
    }
}
