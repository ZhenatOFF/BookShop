using Domain.Abstract;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        IBookRepository repository;
        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Books);
        }

        public ViewResult Edit(int id)
        {
            Book book = repository.Books.FirstOrDefault(b => b.Id == id);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                repository.SaveBook(book);
                TempData["message"] = string.Format($"Изменение информации о книге \"{book.Name}\" сохранены");
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Book());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Book deletedGame = repository.DeleteBook(id);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format($"Книга \"{deletedGame.Name}\" была удалена");
            }
            return RedirectToAction("Index");
        }
    }
}
