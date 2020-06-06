using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.Models
{
    public class Client : IClient
    {
        public int id { get; set; }
        public string firstname { get; set; }
    }
}
