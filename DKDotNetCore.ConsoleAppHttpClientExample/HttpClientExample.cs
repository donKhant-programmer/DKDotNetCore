using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKDotNetCore.ConsoleAppHttpClientExample
{
    internal class HttpClientExample
    {
        private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("http://localhost:5084") };
        private readonly string _blogEndpoint = "api/blog";
        public async Task RunAsync()
        {
            await ReadAsync();
        }

        private async Task ReadAsync()
        {
            var response = await _client.GetAsync(_blogEndpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
                foreach (var blog in lst)
                {
                    Console.WriteLine($"Title: {blog.BlogTitle}");
                    Console.WriteLine($"Author: {blog.BlogAuthor}");
                    Console.WriteLine($"Content: {blog.BlogContent}");
                }
            }

        }
    }
}
