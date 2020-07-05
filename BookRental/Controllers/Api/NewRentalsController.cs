using BookRental.Dtos;
using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookRental.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public  IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (newRental.BooksIds.Count == 0)
                return BadRequest("No Books Ids have been given.");

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            if (customer == null)
                return BadRequest("Invalid customer Id");

            var books = _context.Books.Where(m => newRental.BooksIds.Contains(m.Id)).ToList();

            if (books.Count != newRental.BooksIds.Count)
                return BadRequest("One or more BookIds are invalid");

            foreach (var book in books)
            {
                if (book.NumberAvailable == 0)
                    return BadRequest("Book is not available.");

                book.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Book = book,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
