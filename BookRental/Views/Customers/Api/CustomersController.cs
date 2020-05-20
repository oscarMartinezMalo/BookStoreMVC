using BookRental.Models;
using System;
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
        public IEnumerable<CustomerDto> GetCustomers()
        {
            // Get /api/customers
            //return _context.Customers.ToList()  //Without using DTOs
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // Get /api/customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
                throw new HttpRequestException( HttpStatusCode.NotFound.ToString() );

            //return customer; //Without using DTOs
            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        // POST /api/customers
        [HttpPost]
        //public Customer CreateCustomer(Customer customer) //Without using DTOs
        public CustomerDto CreateCustomer(CustomerDto customerDto)

        {
            if (!ModelState.IsValid)
                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return customerDto;
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

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
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
