using BookingSystem.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit_Testing.Vehicle
{
    [TestClass]
    public class Vehicle
    {
        const string connectionstring = "User ID=postgres;Password=password1234;Host=localhost;Port=5433;Database=VehicleBookingDB;Pooling=true;";
        readonly DataAccess da = new DataAccess(connectionstring);
        const string vehiclemodel = "bmw";
        [TestMethod]
        public void TEST_IF_DB_RETURNS_VEHICLES()
        {
            var vehicles = da.VehicleGetList().Result;
            Assert.IsTrue(vehicles.Count() > 0);
        }
        [TestMethod]
        public void TEST_IF_VEHICLE_CAN_BE_INSERTED()
        {
            var IsSuccesfullyInserted = da.VehicleInsert(vehiclemodel).Result;
            Assert.IsTrue(IsSuccesfullyInserted);
        }
        [TestMethod]
        public void TEST_IF_VEHICLE_MODEL_EXISTS()
        {
            var vehicles = da.VehicleGetList().Result;
            string modelname = vehicles.Select(x => x.vehiclemodel).Where(x => x == vehiclemodel).First().ToString();
            Assert.IsTrue(vehiclemodel == modelname);
        }
        [TestMethod]
        public void TEST_IF_VEHICLE_IS_DELETED()
        {
            var vehicles = da.VehicleGetList().Result;
            int id = vehicles.FirstOrDefault(x => x.vehiclemodel == vehiclemodel).id;
            var IsSuccesfullyInserted = da.VehicleDelete(id).Result;
            Assert.IsTrue(IsSuccesfullyInserted);
        }
    }
}
