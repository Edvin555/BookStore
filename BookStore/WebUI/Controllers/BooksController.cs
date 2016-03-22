using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository repository;
        private EFDbContext db = new EFDbContext();
        public int pageSize = 4 ;
        public BooksController(IBookRepository repo)
        {
            repository = repo;
        }
        //
        // GET: /Books/
        public ViewResult List(string genre, int page = 1)
        {
            BooksListViewModel model = new BooksListViewModel
            {
                Books = repository.Books.Where(b => genre == null || b.Genre == genre).OrderBy(b => b.BookId).Skip((page - 1) * pageSize).Take(pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    TotalItems = genre == null ?  repository.Books.Count() : 
                                          repository.Books.Where(b => b.Genre == genre).Count(),
                    ItemsPerPage = pageSize
                },
                CurrentGenre = genre


            };
            return View(model);
        }

        // GET: /Create/
        public ActionResult Create()
        {
            return View();
        }

        // Post: /Create/
        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("List");

            }
            return View(book);
        }
	}
}