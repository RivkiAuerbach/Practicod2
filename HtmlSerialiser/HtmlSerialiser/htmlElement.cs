using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlSerialiser
{
    public class htmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public htmlElement Parent { get; set; }
        public List<htmlElement> Children { get; set; }

        public htmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            Children = new List<htmlElement>();
        }

        public static htmlElement ParseHtmlString(string htmlString)
        {
            htmlElement rootElement = new htmlElement();
            htmlElement currentElement = rootElement;

            Regex tagRegex = new Regex(@"<(\w+)([^>]*)>(.*?)<\/\1>");
            MatchCollection matches = tagRegex.Matches(htmlString);

            foreach (Match match in matches)
            {
                string tagName = match.Groups[1].Value;
                string attributesString = match.Groups[2].Value;
                string innerHtml = match.Groups[3].Value;

                htmlElement newElement = new htmlElement
                {
                    Name = tagName,
                    InnerHtml = innerHtml
                };

                // Parse attributes
                Regex attributeRegex = new Regex(@"(\w+)=""([^""]*)""");
                MatchCollection attributeMatches = attributeRegex.Matches(attributesString);
                foreach (Match attributeMatch in attributeMatches)
                {
                    string attributeName = attributeMatch.Groups[1].Value;
                    string attributeValue = attributeMatch.Groups[2].Value;

                    if (attributeName.ToLower() == "id")
                    {
                        newElement.Id = attributeValue;
                    }
                    else if (attributeName.ToLower() == "class")
                    {
                        newElement.Classes.AddRange(attributeValue.Split(' '));
                    }
                    else
                    {
                        newElement.Attributes.Add($"{attributeName}=\"{attributeValue}\"");
                    }
                }

                currentElement.Children.Add(newElement);
                newElement.Parent = currentElement;

                currentElement = newElement;
            }

            return rootElement;
        }
    }
}