using BookingSystem.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit_Testing.Client
{
    [TestClass]
    public class Client
    {
        const string connectionstring = "User ID=postgres;Password=password1234;Host=localhost;Port=5433;Database=VehicleBookingDB;Pooling=true;";
        readonly DataAccess da = new DataAccess(connectionstring);
        const string clientname = "JACK";
        [TestMethod]
        public void TEST_IF_DB_RETURNS_CLIENTS()
        {
            var clients = da.ClientGetList().Result;
            Assert.IsTrue(clients.Count() > 0);
        }
        [TestMethod]
        public void TEST_IF_CLIENT_CAN_BE_INSERTED()
        {
            var IsSuccesfullyInserted = da.ClientInsert(clientname).Result;
            Assert.IsTrue(IsSuccesfullyInserted);
        }
        [TestMethod]
        public void TEST_IF_CLIENT_NAME_EXISTS()
        {
            var clients = da.ClientGetList().Result;
            string name = clients.Select(x => x.firstname).Where(x=>x== clientname).First().ToString();
            Assert.IsTrue(clientname == name);
        }
        [TestMethod]
        public void TEST_IF_CLIENT_IS_DELETED()
        {
            var clients = da.ClientGetList().Result;
            int id = clients.FirstOrDefault(x => x.firstname == clientname).id;
            var IsSuccesfullyInserted = da.ClientDelete(id).Result;
            Assert.IsTrue(IsSuccesfullyInserted);
        }
    }
}
