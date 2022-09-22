using Domain.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class BooksController: Controller
    {
        private IBookRepository repository;
        public int pageSize = 4;  //количество товаров отображаемых на одной странице
        public BooksController(IBookRepository repository)
        {
            this.repository = repository;
        }

        [Route("")]
        [Route("Books/List/")]
        [Route("Books/List/{genre}")]
        [Route("Books/List/{genre}/{page}")]
        public ViewResult List(string genre, string name="", int page = 1)
        {
            if (name == null) name = "";
            if (genre == "Все") genre = null;

            BooksListViewModel model = new BooksListViewModel
            {
                Books = repository.Books
                    .Where(book => genre == null|| book.Genre==genre)
                    .Where(book => book.Name.Contains(name))
                    .OrderBy(book => book.Id)
                    .Skip((page - 1) * pageSize)    //пропускаем объекты, расположенные до начала текущей страницы
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = genre==null ? 
                        repository.Books.Where(book => book.Name.Contains(name)).Count() : 
                        repository.Books.Where(book => book.Genre==genre).Where(book => book.Name.Contains(name)).Count()
                },
                CurrentGenre = genre, 
                FilterViewModel = new FilterViewModel(
                    genres: repository.Books
                    .Select(book => book.Genre)
                    .Distinct()
                    .OrderBy(x => x).ToList(), 
                    genre: genre, 
                    name: name)
            };

            return View(model);
        }
    }
}
