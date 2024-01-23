
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
            while (q != null)
            {
                HtmlElement rootElement = q.First();
                yield return q.Dequeue();
                foreach (var item in rootElement.Children)
                    q.Enqueue(item);
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
        //לתקן
        public void FindSelectors(Selector selector, HtmlElement htmlElement)
        {

        }
        public List<HtmlElement> FindSelectors2(Selector selector)
        {
            bool flag = true;
            List<HtmlElement> list1 = new List<HtmlElement>();
            list1 = Descendants().ToList();
            List<HtmlElement> list2 = new List<HtmlElement>();
            foreach (var item in list1)
            {
                if (item.Id == selector.Id && item.Name == selector.TagName)
                {
                    if (item.Classes.Count() != selector.Classes.Count())
                        flag = false;
                    if (flag == true)
                    {
                        for (int i = 0; i < item.Classes.Count(); i++)
                        {
                            if (item.Classes[i] != selector.Classes[i])
                            {
                                flag = false;
                                break;
                            }
                        }

                    }
                }
                if (flag == true)
                    list2.Add(item);
            }
            return list2;
        }
    }
}