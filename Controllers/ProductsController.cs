using Microsoft.AspNetCore.Mvc;
using MonolithApp.Demo.Models;
using MonolithApp.Demo.Services;

namespace MonolithApp.Demo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsService _productsService;
        public ProductsController()
        {
            _productsService = new ProductsService();
        }

        public IActionResult Index()
        {
            ViewBag.ProductId = TempData["ProductId"]?.ToString();
            return View();
        }

        public IActionResult Details([FromQuery] int id)
        {
            try
            {
                Product product = _productsService.GetProductById(id);
                return View(product);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult SubmitProduct(Product product)
        {
            _productsService.AddProduct(product);
            TempData["ProductId"] = product.Id;
            return RedirectToAction("Index");
        }

		public IActionResult Edit([FromQuery] int id)
		{
			var product = _productsService.GetProductById(id);

			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}


		[HttpPost]
		public IActionResult Edit(int id, Product product)
		{
			if (!ModelState.IsValid)
			{
				return View(product);
			}

			_productsService.UpdateProduct(product);
            return Redirect($"/Products/Details?id={product.Id}");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _productsService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productsService.DeleteProduct(id);

            return RedirectToAction("Index");
        }
    }
}
