﻿using AutoMapper;
using BookRental.Models;
using BookRental.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            Mapper.CreateMap<Book, BookDto>();
            Mapper.CreateMap<BookDto, Book>();
            Mapper.CreateMap<Genre , GenreDto>();

        }
    }
}