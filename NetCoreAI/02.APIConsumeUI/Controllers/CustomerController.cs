using System.Text;
using _02.APIConsumeUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _02.APIConsumeUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _clientFactory; //istemci oluşuturup üzerinden işlem yapmak için oluştuırulur 

        public CustomerController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> CustomerList()
        {
            var client = _clientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44378/api/Customers");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //gelen json veriyi stringe çevirir
                var values = JsonConvert.DeserializeObject<List<ResaultCustomerDTO>>(jsonData);//VERİLEN URLDEKİ VERİ İLE RESAULTCUSTOMERDTO YU EŞLEŞTİRİLDİĞİNİ KONTROL EDER
                return View(values);
            }
            return View();
        }


        [HttpGet]
        public IActionResult AddCustomer()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(CreateCustomerDTO model)
        {
            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model); //gelen stringi jsona çevirir
            StringContent Stringcontent = new StringContent(jsonData, Encoding.UTF8, "application/json"); 
            var responseMessage = await client.PostAsync("https://localhost:44378/api/Customers", Stringcontent);
            if (responseMessage.IsSuccessStatusCode)//kod hata veririse liste dönürür
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }

            
        
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var client = _clientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:44378/api/Customers?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var client = _clientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44378/api/Customers/GetCustomer?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetbyIdCustomerDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(GetbyIdCustomerDTO model)
        {
            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent Stringcontent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44378/api/Customers", Stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }
    }
}
