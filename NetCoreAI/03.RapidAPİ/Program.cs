using System.Net.Http.Headers;
using _03.RapidAPİ.ViewModels;
using Newtonsoft.Json;









var client = new HttpClient();
List<ApiSeriesViewModel> series = new List<ApiSeriesViewModel>();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
    Headers =
    {
        { "x-rapidapi-key", "ak" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    series = JsonConvert.DeserializeObject<List<ApiSeriesViewModel>>(body);
    foreach (var item in series)
    {
        Console.WriteLine($" {item.rank} - {item.title} Rating: {item.rating} Id: {item.id} Year: {item.year}");
    }

}


Console.ReadLine();