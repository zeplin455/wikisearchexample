using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiSearchExample.Models
{
    // Some container objects to facilitate smooth deserialization in newtonsoft json
    // Only included fields we care about for now
    public class SearchQuery
    {
        [JsonProperty("query")]
        public SearchContainer Query { get; set; }
    }
    public class SearchContainer
    {

        [JsonProperty("pages")]
        public Dictionary<int, SearchResult> Results { get; set; }
    }
    public record SearchResult
    {
        [JsonProperty("pageid")]
        public int PageId { get; set; }
        [JsonProperty("ns")]
        public int Namespace { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("index")]
        public int Index { get; set; }
        [JsonProperty("contentmodel")]
        public string? ContentModel { get; set; }
        [JsonProperty("pagelanguage")]
        public string? PageLanguage { get; set; }
        [JsonProperty("pagelanguagehtmlcode")]
        public string? PageLanguageHtmlCode { get; set; }
        [JsonProperty("pagelanguagedir")]
        public string? PageLanguageDir { get; set; }
        [JsonProperty("touched")]
        public DateTime Touched { get; set; }
        [JsonProperty("lastrevid")]
        public int Lastrevid { get; set; }
        [JsonProperty("length")]
        public int Length { get; set; }
           
    }
}
