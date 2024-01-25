//ציפי
using practi2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practi2
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        private static Selector CreateSelector(string selector)
        {
            Selector current = new Selector();
            bool flag = false;
            string[] tagAndId = null;
            if (selector.Contains("#"))
            {
                tagAndId = selector.Split('#');
                current.TagName = tagAndId[0];
                selector = tagAndId[1];
                flag = true;
            }
            if (selector.Contains("."))
            {
                current.Classes = new List<string>();
                string[] idAndClass = selector.Split('.');
                if (HtmlHelper.Instance.isHtmlTag(idAndClass[0]))
                {
                    current.TagName = idAndClass[0];
                }

                if (idAndClass.Length >= 2)
                {
                    int i = 0;
                    if (flag)
                    {
                        current.Id = idAndClass[0];
                        i = 1;
                    }
                    if (current.TagName != "")
                        i = 1;
                    for (; i < idAndClass.Length; i++)
                    {
                        current.Classes.Add(idAndClass[i]);
                    }
                }
            }
            else
            {
                if (tagAndId == null)
                    current.TagName = selector;
                else if (tagAndId.Length == 2)
                    current.Id = tagAndId[1];
            }

            return current;

        }

        public static Selector CreateTree(string selector)
        {
            Selector root = null;
            Selector current;
            Selector prev = null;
            if (!selector.Contains(" "))
                return CreateSelector(selector);
            string[] selectors = selector.Split(" ");

            foreach (string s in selectors)
            {
                current = CreateSelector(s);
                if (prev != null)
                {
                    current.Parent = prev;
                    prev.Child = current;
                }
                else
                {
                    root = current;
                }
                prev = current;

                current = current.Child;
            }
            return root;
        }
    }
}




//chatGPT
//using System;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;

//namespace practi2
//{
//    internal class Selector
//    {
//        public string TagName { get; set; }
//        public string Id { get; set; }
//        public List<string> Classes { get; set; } = new List<string>();
//        public Selector Parent { get; set; }
//        public Selector Child { get; set; }

//        public static Selector BuildSelector(string str)
//        {
//            string[] words = str.Split(' ');

//            Selector root = new Selector();
//            Selector currentSelector = root;
//            Selector prev = null;

//            foreach (string word in words)
//            {
//                if (!string.IsNullOrWhiteSpace(word))
//                {

//                    currentSelector = ParseSelectorString(word);
//                    if (prev == null)
//                        root = currentSelector;
//                    currentSelector.Parent = prev;
//                    if (prev != null)
//                        currentSelector.Parent.Child = currentSelector;
//                    prev = currentSelector;

//                }
//            }
//            return root;
//        }

//        public static Selector ParseSelectorString(string str)
//        {
//            string[] parts = str.Split('.', '#');

//            Selector selector = new Selector();

//            foreach (string part in parts)
//            {
//                if (!string.IsNullOrWhiteSpace(part))
//                {
//                    if (part.StartsWith("#"))
//                    {
//                        selector.Id = part.Substring(1);
//                    }
//                    else if (part.StartsWith("."))
//                    {
//                        selector.Classes.Add(part.Substring(1));
//                    }
//                    else
//                    {
//                        // Check if it's a valid HTML tag name before setting it
//                        if (IsValidTagName(part))
//                        {
//                            selector.TagName = part;
//                        }
//                    }
//                }
//            }

//            return selector;
//        }

//        private static bool IsValidTagName(string tagName)
//        {
//            // Add your validation logic here if needed
//            // For simplicity, let's assume any non-empty string is a valid tag name
//            return !string.IsNullOrWhiteSpace(tagName);
//        }
//    }
//}



//שלנו
//using practi2;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;


//namespace practi2
//{
//    internal class Selector
//    {
//        public string TagName { get; set; }
//        public string Id { get; set; }
//        public List<string> Classes { get; set; }
//        public Selector Parent { get; set; }
//        public Selector Child { get; set; }

//        public static Selector BuildSelector(string str)
//        {
//            string[] words = str.Split(' ');

//            Selector root = new Selector();
//            Selector currentSelector = root;
//            Selector prev = null;

//            foreach (string word in words)
//            {
//                if (!string.IsNullOrWhiteSpace(word))
//                {

//                    currentSelector = ParseSelectorString(word);
//                    if (prev == null)
//                        root = currentSelector;
//                    currentSelector.Parent = prev;
//                    if(prev!=null)
//                       currentSelector.Parent.Child = currentSelector;
//                    prev = currentSelector;

//                }
//            }
//            return root;
//        }
//        public static Selector ParseSelectorString(string str)
//        {
//            // ביטוי רגולרי לפיצוץ המחרוזת למילים באמצעות הסימנים המיוחדים
//            string[] words = Regex.Split(str, @"[.#]");

//            // יצירת אובייקט סלקטור
//            Selector selector = new Selector();

//            // לולאה על כל מילה במערך
//            foreach (string word in words)
//            {
//                Console.WriteLine(word);
//                // בדיקה אם המילה לא ריקה
//                if (!string.IsNullOrWhiteSpace(word))
//                {
//                    // בדיקה אם המילה מתחילה בתו '#'
//                    if (word.StartsWith("#"))
//                    {
//                        // שינוי של המשתנה Id בסלקטור
//                        selector.Id = word.Substring(1);
//                    }
//                    // בדיקה אם המילה מתחילה בתו '.'
//                    else if (word.StartsWith("."))
//                    {
//                        // הוספת המילה לרשימת המחלקות בסלקטור
//                        selector.Classes.Add(word.Substring(1));
//                    }
//                    else
//                    {
//                        // הגדרת המילה כשם התגית בסלקטור
//                        selector.TagName = word;
//                    }
//                }
//            }

//            return selector;
//        }
//    }
//}




