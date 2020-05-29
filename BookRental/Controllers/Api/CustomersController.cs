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

namespace BookRental.Views.Customers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        // Get /api/customer
        //public IEnumerable<Customer> GetCustomers()   //Without using DTOs
        public IEnumerable<CustomerDto> GetCustomers(string  query = null)
        {
            // Get /api/customers
            //return _context.Customers.ToList()  //Without using DTOs

            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return customerDtos;
        }

        // Get /api/customers/1
        //public CustomerDto GetCustomer(int id)   // Return 200 status and you 201 status
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            //throw new HttpRequestException( HttpStatusCode.NotFound.ToString() );
            //return customer; //Without using DTOs
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        //public Customer CreateCustomer(Customer customer) //Without using DTOs
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)  // IHttpActionResult return status 201
        {
            if (!ModelState.IsValid)
                BadRequest();
                //throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
            //return customerDto;
        }

        // PUT /api/customers/1
        [HttpPut]
        //public Customer UpdateCustomer(int id,Customer customer)
        public CustomerDto UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customerInDb == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }

            Mapper.Map(customerDto, customerInDb);
            //customerInDb.Name = customer.Name;
            //customerInDb.BirthDate = customer.BirthDate;
            //customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            //customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
            return customerDto;
        }

        // DELETE  /api/customers/1 
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
