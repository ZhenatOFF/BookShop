using Domain.Abstract;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebUI.Controllers;
using WebUI.ViewModels;
using Xunit;

namespace BookShop.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Items()
        {
            // Организация
            Book book1 = new Book { Id = 1, Name = "Book1" };
            Book book2 = new Book { Id = 2, Name = "Book2" };

            Cart cart = new Cart();

            // Действие
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            List<CartItem> results = cart.Items.ToList();

            // Утверждение
            Assert.Equal(results.Count(), 2);
            Assert.Equal(results[0].Book, book1);
            Assert.Equal(results[1].Book, book2);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Items()
        {
            // Организация
            Book book1 = new Book { Id = 1, Name = "Book1" };
            Book book2 = new Book { Id = 2, Name = "Book2" };

            Cart cart = new Cart();

            // Действие
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            List<CartItem> results = cart.Items.OrderBy(c => c.Book.Id).ToList();

            // Утверждение
            Assert.Equal(results.Count(), 2);
            Assert.Equal(results[0].Quantity, 6);
            Assert.Equal(results[1].Quantity, 1);
        }

        [Fact]
        public void Can_Remove_Item()
        {
            // Организация
            Book book1 = new Book { Id = 1, Name = "Book1" };
            Book book2 = new Book { Id = 2, Name = "Book2" };
            Book book3 = new Book { Id = 3, Name = "Book3" };

            Cart cart = new Cart();

            // Действие
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            cart.AddItem(book3, 2);
            cart.RemoveItem(book2);

            // Утверждение
            Assert.Equal(cart.Items.Where(c => c.Book == book2).Count(), 0);
            Assert.Equal(cart.Items.Count(), 2);
        }
        [Fact]
        public void Calculate_Cart_Total()
        {
            // Организация
            Book book1 = new Book { Id = 1, Name = "Book1", Price = 100 };
            Book book2 = new Book { Id = 2, Name = "Book2", Price = 55 };

            Cart cart = new Cart();

            // Действие
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            decimal result = cart.TotatValue();

            // Утверждение
            Assert.Equal(result, 655);
        }

        [Fact]
        public void Can_Clear_Cart()
        {
            // Организация
            Book book1 = new Book { Id = 1, Name = "Book1", Price = 100 };
            Book book2 = new Book { Id = 2, Name = "Book2", Price = 55 };

            Cart cart = new Cart();

            // Действие
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            cart.Clear();

            // Утвержение
            Assert.Equal(cart.Items.Count(), 0);
        }
    }
}
