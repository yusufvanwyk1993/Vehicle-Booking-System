using System;

namespace BookingSystem.Models
{
    public interface IVehicleBooking
    {
        string bookedfor { get; set; }
        string clientfirstname { get; set; }
        int clientid { get; set; }
        int id { get; set; }
        string notes { get; set; }
        int vehicleid { get; set; }
        string vehiclemodel { get; set; }
    }
}