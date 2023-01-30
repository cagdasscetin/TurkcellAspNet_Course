using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {

        private AppDbContext _context;

        private readonly ProductRepository _productRepository;
        public ProductsController(AppDbContext context)
        {

            _productRepository = new ProductRepository();

            _context = context;

            //if (!_context.Products.Any())
            //{
            //    _context.Products.Add(new Product() { Name = "Kalem 1", Price = 100, Stock = 100, Color = "Red" });
            //    _context.Products.Add(new Product() { Name = "Kalem 2", Price = 100, Stock = 200, Color = "Red" });
            //    _context.Products.Add(new Product() { Name = "Kalem 3", Price = 100, Stock = 300, Color = "Red" });

            //    _context.SaveChanges();
            //}
            

        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);

            _context.Products.Remove(product);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet] // Sayfayi gostermek icin kullaniyoruz. Sadece Get'lerin sayfasi olur
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost] // Datayi kaydedecek method budur. Post'larin sayfasi olmaz
        public IActionResult Add(String Name, decimal Price, int Stock, string Color)
        {
            //Request Header-Body

            //1. yontem
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            Product newProduct = new Product() { Name = Name, Price = Price, Stock = Stock, Color = Color };

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {

            return View();
        }
    }
}
