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
    public class PageTests
    {
        [Fact]
        public void Can_Paginate()      //проверяем на правильное разбиение на страницы
        {
            // Организация (arrange)
            Mock<IBookRepository> mock = new Mock<IBookRepository>();      //создаем имитированную реализацию IBookRepository
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{Id = 1, Name = "Book1"},
                new Book{Id = 2, Name = "Book2"},
                new Book{Id = 3, Name = "Book3"},
                new Book{Id = 4, Name = "Book4"},
                new Book{Id = 5, Name = "Book5"}
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            //BooksListViewModel result = (BooksListViewModel)controller.List(null, 2).Model;
            BooksListViewModel result = (BooksListViewModel)controller.List(null, null, 2).Model;

            // Утверждение (assert)
            List<Book> books = result.Books.ToList();      //преобразуем полученную последовательность с пом-ю LINQ-метода ToList()

            Assert.True(books.Count == 2);
            Assert.Equal(books[0].Name, "Book4");
            Assert.Equal(books[1].Name, "Book5");
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{Id = 1, Name = "Book1"},
                new Book{Id = 2, Name = "Book2"},
                new Book{Id = 3, Name = "Book3"},
                new Book{Id = 4, Name = "Book4"},
                new Book{Id = 5, Name = "Book5"}
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            BooksListViewModel result = (BooksListViewModel)controller.List(null, null, 2).Model;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(pagingInfo.CurrentPage, 2);
            Assert.Equal(pagingInfo.ItemsPerPage, 3);
            Assert.Equal(pagingInfo.TotalItems, 5);
            Assert.Equal(pagingInfo.TotalPages, 2);
        }

        [Fact]
        public void Can_Filter_Books()
        {
            // Организация (arrange)
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{Id = 1, Name = "Book1", Genre="Genre1"},
                new Book{Id = 2, Name = "Book2", Genre="Genre2"},
                new Book{Id = 3, Name = "Book3", Genre="Genre1"},
                new Book{Id = 4, Name = "Book4", Genre="Genre3"},
                new Book{Id = 5, Name = "Book5", Genre="Genre2"}
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            List<Book> result = ((BooksListViewModel)controller.List("Genre2", null, 1).Model).Books.ToList();

            Assert.Equal(result.Count(), 2);
            Assert.True(result[0].Name == "Book2" && result[0].Genre == "Genre2");
            Assert.True(result[1].Name == "Book5" && result[1].Genre == "Genre2");
        }
    }
}