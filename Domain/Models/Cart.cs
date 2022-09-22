using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items { get { return items; } }
        public void AddItem(Book book, int quantity)
        {
            CartItem item = items
                .Where(b => b.Book.Id == book.Id)
                .FirstOrDefault();

            if(item == null)
            {
                items.Add(new CartItem { Book=book, Quantity=quantity});
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(Book book)
        {
            items.RemoveAll(b => b.Book.Id == book.Id); 
        }

        public decimal TotatValue()
        {
            return items.Sum(b => b.Book.Price*b.Quantity);
        }

        public void Clear()
        {
            items.Clear();
        }
    }

    public class CartItem
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
