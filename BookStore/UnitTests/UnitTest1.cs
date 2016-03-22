using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Abstract;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book {BookId = 1, Name = "b1"},
                new Book {BookId = 2, Name = "b2"},
                new Book {BookId = 3, Name = "b3"},
                new Book {BookId = 4, Name = "b4"},
                new Book {BookId = 5, Name = "b5"},
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;
            BooksListViewModel result = (BooksListViewModel)controller.List(null,2).Model;

            List<Book> books = result.Books.ToList();
            Assert.IsTrue(books.Count == 2);
            Assert.AreEqual(books[0].Name, "b4");
            Assert.AreEqual(books[1].Name, "b5");

        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book {BookId = 1, Name = "b1"},
                new Book {BookId = 2, Name = "b2"},
                new Book {BookId = 3, Name = "b3"},
                new Book {BookId = 4, Name = "b4"},
                new Book {BookId = 5, Name = "b5"},
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            BooksListViewModel result = (BooksListViewModel)controller.List(null,2).Model;
            PageInfo pagingInfo = result.PageInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
        }
    }
}
