using BookingSystem.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit_Testing.VehicleBooking
{
    [TestClass]
    public class VehicleBookings
    {
        const string connectionstring = "User ID=postgres;Password=password1234;Host=localhost;Port=5433;Database=VehicleBookingDB;Pooling=true;";
        readonly DataAccess da = new DataAccess(connectionstring);
        const string clientname = "JACK";
        [TestMethod]
        public void TEST_IF_DB_RETURNS_VEHICLE_BOOKINGS()
        {
            var bookings = da.VehicleGetList().Result;
            Assert.IsTrue(bookings.Count() > 0);
        }
        [TestMethod]
        public void TEST_IF_A_BOOKING_CAN_MADE()
        {
            int clientid = da.ClientGetList().Result.First().id;
            int vehicleid = da.VehicleGetList().Result.First().id;
            var IsSuccesfullyInserted = da.BookingsInsert(clientid,vehicleid, "23/09/2020", "Service").Result;
            Assert.IsTrue(IsSuccesfullyInserted);
        }
        [TestMethod]
        public void TEST_IF_BOOKING_EXISTS_BY_CLIENT()
        {
            int clientid = da.ClientGetList().Result.First().id;

            var bookings = da.BookingsGetList().Result;
            var booking = bookings.Where(x => x.clientid == clientid).First();
            Assert.IsTrue(!string.IsNullOrEmpty(booking.clientfirstname));
        }
        [TestMethod]
        public void TEST_IF_BOOKING_IS_DELETED()
        {
            var bookings = da.BookingsGetList().Result;
            int id = bookings.FirstOrDefault(x => x.notes == "Service").id;
            var IsSuccesfullyInserted = da.ClientDelete(id).Result;
            Assert.IsTrue(IsSuccesfullyInserted);
        }
    }
}
