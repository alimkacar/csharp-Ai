using _01.APIDemo.Context;
using _01.APIDemo.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _01.APIDemo.Controllers
{
    [Route("api/[controller]")] // Doğru RouteAttribute kullanılmalı
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly APIContext _context;

        public CustomersController(APIContext context)
        {
            _context = context;
        }

        [HttpGet] // GET isteği için route tanımlaması
        public IActionResult CustomerList()
        {
            var values = _context.Customers.ToList();
            return Ok(values); // HTTP 200 yanıtı döndürür
        }


        [HttpPost]
        public IActionResult AddCustomer(Customer c)
        {
            _context.Customers.Add(c);
            _context.SaveChanges();
            return Ok("Müşteri Ekleme İşlemi Başarılı");
        }
        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok("Müşteri Silme İşlemi Başarılı");
        }
        [HttpPut]
        public IActionResult UpdateCustomer(Customer c)
        {
            _context.Customers.Update(c);
            _context.SaveChanges();
            return Ok("Müşteri Güncelleme İşlemi Başarılı");
        }

        [HttpGet("GetCustomer")] // GET isteği için route tanımlaması birden fazla hhtpget kullanınca diğerine isimlendirme vermen gerekiyor
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            return Ok(customer);
        }
    }
}