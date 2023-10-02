﻿using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BookingRepository : IBookingRepository

    {
        private readonly BookingManagementDBContext _context;

        public BookingRepository(BookingManagementDBContext context)
        {
            _context = context;
        }
        public Booking? Create(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Add(booking);
                _context.SaveChanges();
                return booking;
            }catch
            {
                return null;
            }
        }

        public bool Delete(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Remove(booking);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Set<Booking>().ToList();
        }

        public Booking? GetByGuid(Guid guid)
        {
            return _context.Set<Booking>().Find(guid);
        }

        public bool Update(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Update(booking);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}