using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebSite.Controllers
{
    /// <summary>
    /// Bookings Controller meant to handle vehicle bookings requests 
    /// </summary>
    public class BookingsController : Controller
    {
        /// <summary>
        /// Setting up variables and connectionstrings that comes from the appsettings.json file
        /// </summary>
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        IConfiguration configuration;
        const string appsettingsjsonfile = "AppSettings.json";
        DataAccess da;
        /// <summary>
        /// Setup connection to testVMGDB database
        /// </summary>
        public BookingsController()
        {
            configurationBuilder.AddJsonFile(appsettingsjsonfile);
            configuration = configurationBuilder.Build();
            string conString = Microsoft
               .Extensions
               .Configuration
               .ConfigurationExtensions
               .GetConnectionString(this.configuration, "testVMGDB");
            da = new DataAccess(conString);
        }
        /// <summary>
        /// If a date is specified, only bookings based on that date will be returned
        /// Else the full list of bookings will be returned
        /// </summary>
        /// <param name="filterbydate"></param>
        /// <returns></returns>
        public IActionResult Index(string filterbydate)
        {
            if (string.IsNullOrEmpty(filterbydate) | string.IsNullOrWhiteSpace(filterbydate))
            {
                var bookings = da.BookingsGetList().Result;
                var clients = da.ClientGetList().Result;
                var vehicles = da.VehicleGetList().Result;
                ViewData["Clients"] = clients;
                ViewData["Vehicles"] = vehicles;
                ViewData["Bookings"] = bookings;
                return View();
            }
            else
            {
                var parsedDate = DateTime.ParseExact(filterbydate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                var formattedDate = parsedDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var bookings = da.BookingsGetList(formattedDate).Result;
                var clients = da.ClientGetList().Result;
                var vehicles = da.VehicleGetList().Result;
                ViewData["Clients"] = clients;
                ViewData["Vehicles"] = vehicles;
                ViewData["Bookings"] = bookings;
                return View();
            }
            
        }
        /// <summary>
        /// Allows user to handle request for inserting a new vehicle booking
        /// </summary>
        /// <param name="addclientid"></param>
        /// <param name="addvehicleid"></param>
        /// <param name="addbookingfor"></param>
        /// <param name="addnotes"></param>
        /// <returns></returns>
        public async Task<IActionResult> Insert(string addclientid, string addvehicleid, string addbookingfor,string addnotes)
        {
            var parsedDate = DateTime.ParseExact(addbookingfor, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var formattedDate = parsedDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            await da.BookingsInsert(int.Parse(addclientid),int.Parse(addvehicleid), formattedDate, addnotes);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Bookings", null);
            return redirectResult;
        }
        /// <summary>
        /// Allows user to handle request for deleting a booking
        /// </summary>
        /// <param name="deletebookingid"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string deletebookingid)
        {
            await da.BookingsDelete(int.Parse(deletebookingid));
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Bookings", null);
            return redirectResult;
        }
    }
}