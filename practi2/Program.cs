
using practi2;
using System.Text.RegularExpressions;

var html = await Load("https://learn.malkabruk.co.il/");
HtmlElement rootElement = builtree(html);
PrintTree(rootElement, 0);

static HtmlElement builtree(string html)
{
    var clearHtml = new Regex(@"[\r\n]+").Replace(html, "");
    var htmlLines = new Regex("<(.*?)>").Split(clearHtml).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

    int keywordIndex = htmlLines.FindIndex(s => s.StartsWith("html"));
    htmlLines.RemoveRange(0, keywordIndex + 1);

    HtmlElement rootElement = new HtmlElement { Name = "html", Children = new List<HtmlElement>(), Parent = null };
    HtmlElement currentElement = rootElement;
    int currentIndex = 0;

    while (currentIndex < htmlLines.Count)
    {
        string line = htmlLines[currentIndex];
        var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line).Cast<Match>()
            .Select(match => match.Value).ToList();
        if (line.StartsWith("!--"))
        {
            while (!line.Contains("-->"))
            {
                currentIndex++;
                line = htmlLines[currentIndex];
            }
            continue;
        }

        int allLine = line.IndexOf(' ');
        string name, id = null, clas = null;
        List<string> classes = null;

        if (allLine == -1)
        {
            name = line;
        }
        else
            name = line.Substring(0, line.IndexOf(" "));

        if (attributes.Count > 0)
        {
            id = attributes.FirstOrDefault(attr => attr.StartsWith("id"));
            if (id != null)
            {
                attributes.RemoveAll(attr => attr == id);
                id = id.Substring(4);
            }

            clas = attributes.FirstOrDefault(attr => attr.StartsWith("class"));
            if (clas != null)
            {
                attributes.RemoveAll(attr => attr == clas);
                clas = clas.Substring(7);
                classes = clas.Split(" ").ToList();
            }
        }

        if (line == "/html")
        {
            break;
        }
        else if (line.StartsWith("/"))
        {
            if(currentElement!=null)
            currentElement = currentElement.Parent;
        }

        else if (!HtmlHelper.Instance.isHtmlTag(name))
        {
            currentElement.InnerHtml = (string)line;
        }
        else if (HtmlHelper.Instance.IsSelfClosingTag(name))
        {
            HtmlElement child = new HtmlElement
            {
                Name = name,
                Id = id,
                Classes = classes,
                Attributes = attributes,
                Parent = currentElement
            };
            currentElement.Children.Add(child);
        }
        else
        {
            HtmlElement child = new HtmlElement
            {
                Name = name,
                Id = id,
                Classes = classes,
                Attributes = attributes,
                Parent = currentElement,
                Children = new List<HtmlElement>()
            };


            currentElement.Children.Add(child);
            currentElement = child;
        }

        currentIndex++;
    }
    return rootElement;
}


static void PrintTree(HtmlElement element, int depth)
{
    Console.WriteLine(new string(' ', depth * 2) + element.Name);

    foreach (var child in element.Children)
    {
        PrintTree(child, depth + 1);
    }
}

static async Task<string> Load(string url)
{
    using HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();

    return html;
}