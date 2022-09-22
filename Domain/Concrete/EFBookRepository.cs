using Domain.Abstract;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        EFDbContext dbContext;

        public EFBookRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Book> Books
        {
            get { return dbContext.Books; }
        }


        public void SaveBook(Book book)
        {
            if (book.Id == 0)
            {
                dbContext.Books.Add(book);
            }
            else
            {
                Book dbEntry = dbContext.Books.Find(book.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = book.Name;
                    dbEntry.Author = book.Author;
                    dbEntry.Description = book.Description;
                    dbEntry.Genre = book.Genre;
                    dbEntry.Price = book.Price;
                }
            }
            dbContext.SaveChanges();
        }
        public Book DeleteBook(int id)
        {
            Book book = dbContext.Books.Find(id);
            if(book != null)
            {
                dbContext.Books.Remove(book);
                dbContext.SaveChanges();
            }
            return book;
        }
    }
}
