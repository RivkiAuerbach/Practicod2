
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace practi2
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
                HtmlElement rootElement = q.Dequeue();
                foreach (HtmlElement item in rootElement.Children)
                {
                    q.Enqueue(item);
                }
                yield return rootElement;
            }
        }
        public List<HtmlElement> Ancestors()
        {
            HtmlElement rootElement = this;
            List<HtmlElement> list = new List<HtmlElement>();
            list.Add(rootElement);
            while (rootElement.Parent != null)
            {
                list.Add(this.Parent);
                rootElement = rootElement.Parent;
            }
            return list;
        }

        public static void FindSelectors(Selector selector, HtmlElement htmlElement, List<HtmlElement> result)
        {
            List<HtmlElement> list1 = htmlElement.Descendants().ToList();


            foreach (HtmlElement item in list1)
            {

                if (item.Id != null && selector.Id != null)
                    if (item.Id != selector.Id)
                        continue;
                if (!(item.Id == null && selector.Id == null))
                    continue;
                if (item.Name != null && selector.TagName != null)
                    if (item.Name != selector.TagName)
                        continue;
                if (!(item.Name == null && selector.TagName == null))
                    continue;
                if (selector.Classes != null && item.Classes != null)
                {

                    if (!selector.Classes.All(c => item.Classes.Contains(c)))
                        continue;
                }
                else
                {
                    if (!(selector.Classes == null && item.Classes == null))
                        continue;
                }
                if (selector.Child == null)
                    result.Add(item);
                else
                {
                    foreach (HtmlElement htmlElementChild in item.Children)
                        FindSelectors(selector.Child, htmlElementChild, result);
                }


            }
        }


    }
}










