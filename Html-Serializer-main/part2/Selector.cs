using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace part2
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
                if (HtmlHelper.Instance.IsHtmlTag(idAndClass[0]))
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
            Selector current ;
            Selector prev=null;
            if (!selector.Contains(" "))
                return CreateSelector(selector);
            string[] selectors= selector.Split(" ");

            foreach (string s in selectors)
            {
                current=CreateSelector(s);
                if (prev != null)
                {
                    current.Parent = prev;
                    prev.Child = current;
                }
                else
                {
                    root=current;
                }
                prev = current;

                current = current.Child;
            }
            return root;
        }
    }
}
