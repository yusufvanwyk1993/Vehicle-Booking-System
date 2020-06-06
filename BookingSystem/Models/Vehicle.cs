using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.Models
{
    public class Vehicle : IVehicle
    {
        public int id { get; set; }
        public string vehiclemodel { get; set; }
    }
}
