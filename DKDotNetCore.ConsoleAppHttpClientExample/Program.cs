// See https://aka.ms/new-console-template for more information
using DKDotNetCore.ConsoleAppHttpClientExample;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");

//HttpClient client = new HttpClient();
//var response = await client.GetAsync("https://localhost:7214/api/blog");

//if (response.IsSuccessStatusCode)
//{
//    string jsonStr = await response.Content.ReadAsStringAsync();
//    Console.WriteLine(jsonStr);
//    List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
//    foreach (var blog in lst)
//    {
//        Console.WriteLine($"Title: {blog.BlogTitle}");
//        Console.WriteLine($"Author: {blog.BlogAuthor}");
//        Console.WriteLine($"Content: {blog.BlogContent}");
//    }
//}

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();