using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace part2
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public string[] AllTags { get; private set; }
        public string[] SelfClosingTags { get; private set; }
        private HtmlHelper()
        {
            var s = File.ReadAllText("AllTags.json");
            this.AllTags = JsonSerializer.Deserialize<string[]>(s);

            var a = File.ReadAllText("SelfClosingTags.json");
            this.SelfClosingTags = JsonSerializer.Deserialize<string[]>(a);
        }

        public bool IsSelfClosingTag(string tag)
        {
            return this.SelfClosingTags.Contains(tag);
        }
       
        public bool IsHtmlTag(string tag)
        {
            return this.AllTags.Contains(tag);
        }
        

    }
}
