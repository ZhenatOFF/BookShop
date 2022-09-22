using Domain.Models;

namespace WebUI.ViewModels
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentGenre { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
