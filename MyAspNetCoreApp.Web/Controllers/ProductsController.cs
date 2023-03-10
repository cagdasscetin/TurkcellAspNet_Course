using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {

        private AppDbContext _context;

        private IHelper _helper;

        private readonly ProductRepository _productRepository;
        public ProductsController(AppDbContext context, IHelper helper)
        {

            _productRepository = new ProductRepository();
            _helper = helper;

            _context = context;

            

        }
        public IActionResult Index([FromServices]IHelper helper2)
        {
            var text = "Asp.Net";
            var upperText = _helper.Upper(text);

            var status = _helper.Equals(helper2);


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

        //[HttpGet]
        //public IActionResult SaveProduct(Product newProduct)
        //{
        //    _context.Products.Add(newProduct);
        //    _context.SaveChanges();

        //    return View();
        //}

        [HttpPost] // Datayi kaydedecek method budur. Post'larin sayfasi olmaz
        public IActionResult Add(Product newProduct)
        {
            //Request Header-Body

            //1. yontem
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            //2. yontem
            //Product newProduct = new Product() { Name = Name, Price = Price, Stock = Stock, Color = Color };

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla eklendi";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct, int productId, string type)
        {
            updateProduct.Id = productId;

            _context.Products.Update(updateProduct);
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla güncellendi.";

            return RedirectToAction("Index");
        }
    }
}
