using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.Models
{
    public class VehicleBooking : IVehicleBooking
    {
        public int id { get; set; }
        public int clientid { get; set; }
        public string clientfirstname { get; set; }
        public int vehicleid { get; set; }
        public string vehiclemodel { get; set; }
        public string bookedfor { get; set; }
        public string notes { get; set; }
    }
}
