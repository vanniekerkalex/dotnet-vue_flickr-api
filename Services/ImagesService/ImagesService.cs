using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace planday.Services.ImagesService
{
  public class ImagesService
  {
    const string API_KEY = "48fecfd1d7eeddd1f9b856c618ad1c77";
    const int PER_PAGE = 30;
    const string METHOD_GET = "flickr.photos.getRecent";
    const string METHOD_SEARCH = "flickr.photos.search";
    const string FORMAT = "json";

    public async Task<string> Search(string tags)
    {
      var httpClient = HttpClientFactory.Create();
      var words = tags.Split(new char[] { ',', ';', ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
      var trimmed = String.Join(",", words);
      var data = "";
      var url = "";

      if (trimmed.Length > 0)
      {
        url = $"https://www.flickr.com/services/rest/?api_key={API_KEY}&method={METHOD_SEARCH}&format={FORMAT}&tags={trimmed}&per_page={PER_PAGE}";
      }
      else
      {
        url = $"https://www.flickr.com/services/rest/?api_key={API_KEY}&method={METHOD_GET}&format={FORMAT}&per_page={PER_PAGE}";
      }

      HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
      if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
      {
        var content = httpResponseMessage.Content;
        data = await content.ReadAsStringAsync();
      }
      else
      {
        Console.WriteLine($"Error: " + httpResponseMessage.StatusCode);
      }

      return data;
    }
  }
}