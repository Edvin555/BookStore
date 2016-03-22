using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Book book1 = new Book { BookId = 1, Name = "b1" };
            Book book2 = new Book { BookId = 2, Name = "b2" };

            Cart cart = new Cart();
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);

            List<CartLine> results = cart.Lines.ToList();
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Book, book1);

            Assert.AreEqual(results[1].Book, book2);
        }

        [TestMethod]
        public void Can_Add_Quantity_for_Existing_Lines()
        {
            Book book1 = new Book { BookId = 1, Name = "b1" };
            Book book2 = new Book { BookId = 2, Name = "b2" };

            Cart cart = new Cart();
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);

            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);

            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Lines()
        {
            Book book1 = new Book { BookId = 1, Name = "b1" };
            Book book2 = new Book { BookId = 2, Name = "b2" };

            Cart cart = new Cart();
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 6);
            cart.RemoveLine(book2);
            
            //Assert.AreEqual(cart.Count(), 1);
            Assert.AreEqual(cart.Lines.Where(l => l.Book == book2).Count(), 0);
        }
    }
}
