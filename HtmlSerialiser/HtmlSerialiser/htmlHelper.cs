using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace HtmlSerialiser
{
    public class htmlHelper
    {
        private readonly static htmlHelper _instance = new htmlHelper();
        public static htmlHelper instance => _instance;
        public string[] AllHtmlTags { get; private set; }
        public string[] TagsWithoutClosing { get; private set; }

        //private htmlHelper(string allTagsJsonPath, string tagsWithoutClosingJsonPath)
        private htmlHelper()
        {
            string allTagsJson = File.ReadAllText("files/allTagsFilePath");
            AllHtmlTags = JsonSerializer.Deserialize<string[]>(allTagsJson);

            // Read and deserialize self-closing HTML tags
            string selfClosingTagsJson = File.ReadAllText("files/selfClosingTagsFilePath");
            TagsWithoutClosing = JsonSerializer.Deserialize<string[]>(selfClosingTagsJson);
            // קוד לטעינת נתונים מקובץ JSON
        }



        // נוסיף פה פונקציות נוספות לעזור בעבודה עם רשימת תגיות
    }
}
