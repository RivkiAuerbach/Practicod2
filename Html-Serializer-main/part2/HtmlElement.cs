using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace part2
{
    internal class HtmlElement
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; } = new List<string>();
        public List<string> Attributes { get; set; } = new List<string>();
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> q = new Queue<HtmlElement>();
            
            q.Enqueue(this);
            while (q.Count > 0)
            {
                HtmlElement tmp = q.Dequeue();
                foreach (HtmlElement child in tmp.Children)
                {
                    q.Enqueue(child);
                }
                yield return tmp;
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement tmp = this;
            while(tmp.Parent != null)
            {
                yield return tmp.Parent;
                tmp = tmp.Parent;
            }
        }
        public static List<HtmlElement> Search(HtmlElement element, Selector selector, List<HtmlElement> s)
        {
            if (selector == null)
            {
                return null;
            }
            IEnumerable<HtmlElement> descendants = element.Descendants();
            foreach (HtmlElement d in descendants)
            {
                if (selector.TagName != null && selector.TagName != "")
                {
                    if (selector.TagName != d.Name)
                        continue;

                }
             
                if (selector.Id != null)
                {
                    if (selector.Id != d.Id)
                        continue;

                }
               
                if (selector.Classes != null)
                {
                    if (d.Classes != null)
                    {
                        if (!selector.Classes.All(c => d.Classes.Contains(c)))
                            continue;
                    }
                    else
                        continue;

                }

                if (selector.Child == null)
                {
                    s.Add(d);
                }
                else
                {
                    Search(d, selector.Child, s);
                }
            }
            var re = new HashSet<HtmlElement>(s);
            return re.ToList();
        }

    }
}
