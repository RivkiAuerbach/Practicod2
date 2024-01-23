using practi2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace practi2
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; } = new Selector();


        public static Selector buildSelector(string str)
        {
            //string[] words = Regex.Split(str, @"[ ]");
            string[] words = Regex.Split(str, @"(?<= )");

            Selector root = new Selector();
            Selector currentSelector = root;


            foreach (string word in words)
            {
                currentSelector = ParseSelectorString(word);
                currentSelector.Child = new Selector();
                currentSelector.Child.Parent = currentSelector;
                currentSelector = currentSelector.Child;
            }
            return root;
        }
    public static Selector ParseSelectorString(string str)
    {
        // ביטוי רגולרי לפיצוץ המחרוזת למילים באמצעות הסימנים המיוחדים
        string[] words = Regex.Split(str, @"[.#]");

        // יצירת אובייקט סלקטור
        Selector selector = new Selector();

        // לולאה על כל מילה במערך
        foreach (string word in words)
        {
                Console.WriteLine(word);
            // בדיקה אם המילה לא ריקה
            if (!string.IsNullOrWhiteSpace(word))
            {
                // בדיקה אם המילה מתחילה בתו '#'
                if (word.StartsWith("#"))
                {
                    // שינוי של המשתנה Id בסלקטור
                    selector.Id = word.Substring(1);
                }
                // בדיקה אם המילה מתחילה בתו '.'
                else if (word.StartsWith("."))
                {
                    // הוספת המילה לרשימת המחלקות בסלקטור
                    selector.Classes.Add(word.Substring(1));
                }
                else
                {
                    // הגדרת המילה כשם התגית בסלקטור
                    selector.TagName = word;
                }
            }
        }

        return selector;
    }
}
}



//שילוב של 2 הפונקציות ביחד
//public static Selector ParseSelectorString(string str)
//{
//    // ביטוי רגולרי לפיצוץ המחרוזת למילים באמצעות הסימנים המיוחדים
//    string[] words = Regex.Split(str, @"(?=[.#])");

//    // יצירת אובייקט סלקטור
//    Selector rootSelector = new Selector();
//    Selector currentSelector = rootSelector;

//    // לולאה על כל מילה במערך
//    foreach (string word in words)
//    {
//        // בדיקה אם המילה לא ריקה
//        if (!string.IsNullOrWhiteSpace(word))
//        {
//            // בדיקה אם המילה מתחילה בתו '#'
//            if (word.StartsWith("#"))
//            {
//                // שינוי של המשתנה Id בסלקטור הנוכחי
//                currentSelector.Id = word.Substring(1);
//            }
//            // בדיקה אם המילה מתחילה בתו '.'
//            else if (word.StartsWith("."))
//            {
//                // הוספת המילה לרשימת המחלקות בסלקטור הנוכחי
//                currentSelector.Classes.Add(word.Substring(1));
//            }
//            else
//            {
//                // הגדרת המילה כשם התגית בסלקטור הנוכחי
//                currentSelector.TagName = word;
//            }

//            // בדיקה אם המילה מסתיימת בתו '>'
//            if (word.EndsWith(">"))
//            {
//                // יצירת סלקטור חדש ועדכון של הסלקטור הנוכחי
//                Selector newSelector = new Selector();
//                currentSelector.Child = newSelector;
//                newSelector.Parent = currentSelector;
//                currentSelector = newSelector;
//            }
//        }
//    }

//    return rootSelector;
}
