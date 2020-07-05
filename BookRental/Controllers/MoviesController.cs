using BookRental.Models;
using BookRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookRental.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = RoleName.CanManageBooks)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new BookFormViewModel
            {
                //Book = new Book(),  To avoid error in Id in the UI
                Genres = genres
            };

            return View(viewModel);
        }

        [Authorize(Roles = RoleName.CanManageBooks)]
        public ActionResult Edit(int id)
        {
            var books = _context.Books.SingleOrDefault(c => c.Id == id);

            if (books == null)
                return HttpNotFound();

            var viewModel = new BookFormViewModel(books)
            {
                Genres = _context.Genres.ToList()
            };

            return View("New", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageBooks)]
        public ActionResult Save(Book book)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BookFormViewModel(book)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("BookForm", viewModel);
            }

            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _context.Books.Add(book);
            }
            else
            {
                var bookInDb = _context.Books.Single(m => m.Id == book.Id);
                bookInDb.Name = book.Name;
                bookInDb.GenreId = book.GenreId;
                bookInDb.NumberInStock = book.NumberInStock;
                bookInDb.ReleaseDate = book.ReleaseDate;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Books");
        }

        public ViewResult Index()
        {
            var books = _context.Books.Include(m => m.Genre).ToList();
            //var books = GetBooks();
            if (User.IsInRole(RoleName.CanManageBooks))
                return View("List", books);


            return View("ReadOnlyList",books);
        }

        public ActionResult Details(int id)
        {
            var oneBook = _context.Books.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (oneBook == null)
                return HttpNotFound();

            return View(oneBook);
        }

        // GET: Books/Random
        //public ActionResult Random()
        //{
        //    var book = new Book() { Name = "Shrek!" };
        //    var customers = new List<Customer>
        //    {
        //        new Customer { Name = "Customer 1" },
        //        new Customer { Name = "Customer 2" }
        //    };
        //    var viewModel = new RandomBookViewModel
        //    {
        //        Book = book,
        //        Customers = customers
        //    };
        //    return View(viewModel);
        //}

        //private IEnumerable<Book> GetBooks()
        //{
        //    return new List<Book>
        //    {
        //        //new Book { Id = 1, Name = "Shrek" },
        //        //new Book { Id = 2, Name = "Wall-e" }
        //    };
        //}
    }
}