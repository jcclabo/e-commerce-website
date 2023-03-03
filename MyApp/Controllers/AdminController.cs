using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyApp.Models;
using MyApp.App.Biz;

namespace MyApp.Controllers
{
    public class AdminController : Controller {
        [HttpGet, Route("/Admin/Product")]
        public IActionResult AdminProduct() {
            ProductModel model = new ProductModel();

            

            return View(model);
        }

        [HttpPost, Route("/Admin/Product/ajax-save")]
        public ActionResult Save(object data) {
            ProductModel model = new ProductModel();
            int index = 0;

            return View(model);


        }
    }
}
