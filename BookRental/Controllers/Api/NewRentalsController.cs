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
            if (newRental.MoviesIds.Count == 0)
                return BadRequest("No Movie Ids have been given.");

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            if (customer == null)
                return BadRequest("Invalid customer Id");

            var movies = _context.Movies.Where(m => newRental.MoviesIds.Contains(m.Id)).ToList();

            if (movies.Count != newRental.MoviesIds.Count)
                return BadRequest("One or more MovieIds are invalid");

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
