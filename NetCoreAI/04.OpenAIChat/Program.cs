

using System.Text;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var ak = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        Console.WriteLine("Merhaba, ben Chatbot. Size nasıl yardımcı olabilirim?");
        
        var promt= Console.ReadLine();

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ak}");
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new {role = "system", content = "You are a helpfull asistant." },
                new {role = "user",content = promt}
            },
            max_tokens = 100
        };
        var JsonRequestBody = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(JsonRequestBody, Encoding.UTF8, "application/json");

        try
        {
            var respons = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await respons.Content.ReadAsStringAsync();
            if(respons.IsSuccessStatusCode)//eğer bir hata oluşmazsa gelen cevabı yazdırmak için kullanılır.
            {
                var responseJson = JsonConvert.DeserializeObject<dynamic>(responseString);
                var chatResponse = responseJson("choices")[0].GetPropertyValue("message").GetPropertyValue("content").ToString();
                Console.WriteLine(chatResponse);
            } 
            else
            {
                Console.WriteLine($"Bir hata oluştu. Lütfen tekrar deneyin:  {respons.StatusCode}");
                Console.WriteLine(responseString);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu. Lütfen tekrar deneyin: {ex.Message}");

        }
    }
}