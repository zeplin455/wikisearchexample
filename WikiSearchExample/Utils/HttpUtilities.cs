using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiSearchExample.Utils
{
    public static class HttpUtilities
    {
        // Generic utility method to make a GET request and try to deserialize to given type
        public static async Task<T> Get<T>(string url, Dictionary<string, string> parameters)
        {
            UriBuilder builder = new UriBuilder(url);

            if(parameters != null && parameters.Count > 0)
            {
                builder.Query = string.Join("&", parameters.Select(q => $"{q.Key}={q.Value}"));
            }

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(builder.Uri);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string objStr = await response.Content.ReadAsStringAsync();
                    if (objStr != null)
                    {
                        T resultObj = JsonConvert.DeserializeObject<T>(objStr);
                        return resultObj;
                    }
                    // Return and exceptions can be handled based on what fits the project the best.
                    return default;
                }
                else
                {
                    Console.WriteLine($"Request failed : {response.StatusCode}");
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return default;
        }
    }
}
