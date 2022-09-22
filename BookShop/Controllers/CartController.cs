using Domain.Abstract;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Helpers;
using WebUI.ViewModels;


namespace WebUI.Controllers
{
    public class CartController: Controller
    {
        private IBookRepository repository;
        private Cart cart;

        public CartController(IBookRepository repository)
        {
            this.repository = repository;
            cart = new Cart();
        }

        public ViewResult Index()
        {
            return View(new CartIndexViewModel { 
                Cart = GetCart()
            });
        }

        public Cart GetCart()
        {
            if (HttpContext.Session.Keys.Contains("cart"))
                Console.WriteLine("w");
            //Cart cart = SessionHelper.GetFromJson<Cart>(HttpContext.Session, "cart");
            Cart cart = (Cart)HttpContext.Session.GetFromJson<Cart>("cart");

            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.SetAsJson("cart", cart);
            }
                //SessionHelper.SetAsJson<Cart>(HttpContext.Session, "cart", cart);
                 
            return cart;
        }

        public IActionResult AddToCart(int id)
        {
            Cart cart = GetCart();
            Book book = repository.Books
                .FirstOrDefault(x => x.Id == id);

            if (book != null)
            {
                cart.AddItem(book, 1);
            }

            //Cart cart1 = HttpContext.Session.GetFromJson<Cart>("cart");
            HttpContext.Session.SetAsJson("cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            Cart cart = GetCart();
            Book book = repository.Books
                .FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                cart.RemoveItem(book);
            }

            HttpContext.Session.SetAsJson("cart", cart);

            return RedirectToAction("Index");
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        public ViewResult Checkout()
        {
            if(GetCart().Items.Count() == 0)
            {
                ModelState.AddModelError("cart", "Корзина пуста!");
            }
            return View(new ShippingDetails());
        }
    }
}
