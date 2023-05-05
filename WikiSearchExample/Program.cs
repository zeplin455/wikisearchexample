// See https://aka.ms/new-console-template for more information
using System.Text;
using WikiSearchExample.Models;
using WikiSearchExample.Utils;

// We will query english and japanese wikipedia sites.
string[] urls = new string[] 
{
    "https://en.wikipedia.org/w/api.php",
    "https://jp.wikipedia.org/w/api.php"
};

string word = "絵文字";
int searchPageLimit = 100;
int resultLimit = 20;


// Set up query parameters
Dictionary<string, string> queryParams = new Dictionary<string, string>() 
{ 
    { "action", "query" },
    { "generator", "search" },
    { "gsrsearch", word },
    { "format", "json" },
    { "prop", "info" },
    { "gsrlimit", searchPageLimit.ToString() },
};

//Set initial array size because we know what it will be so we can be precise in mem usage.
List<SearchResult> results = new List<SearchResult>(urls.Length * searchPageLimit);

// Query sites for data
foreach(string url in urls)
{
    SearchQuery result = await HttpUtilities.Get<SearchQuery>(url, queryParams);

    // A bit of an awkward if to check that data is not null.
    if(result != null && result.Query != null && result.Query.Results != null)
    {
        results.AddRange(result.Query.Results.Select(q => q.Value));
    }
}

// If we don't have data then print a message
if(results.Count == 0)
{
    Console.WriteLine("No data was returned");
    return;
}

// Sort from largest to smallest - Let .net decide on sort algorithm (between quicksort, heapsort and insertion sort) or insert own sorting algorithm here
results.Sort((x, y) => y.Length.CompareTo(x.Length));

Console.OutputEncoding = Encoding.Unicode;

for(int i = 0; i < resultLimit; i++)
{
    Console.WriteLine($"{results[i].Title} | {results[i].Length} | {results[i].PageLanguage}");
}

Console.ReadKey();