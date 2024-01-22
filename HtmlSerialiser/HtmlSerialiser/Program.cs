

using System.Text.RegularExpressions;

var html = await Load("https://hebrewbooks.org/beis");

var cleanHtml=new Regex("\\s").Replace(html, "");
var htmlLines=new Regex("<(.*?)>").Split(cleanHtml).Where(s=>s.Length>0);

var htmlElement = "<div id=\"my-id\" class=\"my-class1 my-class2\" width=\"100%\">text</div>";
var attrebutes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlElement);
Console.ReadLine();

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

