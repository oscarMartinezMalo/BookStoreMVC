using BookRental.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BookRental.Dtos;

namespace BookRental.Controllers.Api
{
    public class BooksController : ApiController
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<BookDto> GetBooks(string query = null)
        {
            var booksQuery = _context.Books
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                booksQuery = booksQuery.Where(m => m.Name.Contains(query));

            return booksQuery
                .ToList()
                .Select(Mapper.Map<Book, BookDto>);
        }
    }
}
