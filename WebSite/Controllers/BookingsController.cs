using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebSite.Controllers
{
    public class BookingsController : Controller
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        IConfiguration configuration;
        const string appsettingsjsonfile = "AppSettings.json";
        DataAccess da;
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
        public IActionResult Index()
        {
            var bookings = da.BookingsGetList().Result;
            var clients = da.ClientGetList().Result;
            var vehicles = da.VehicleGetList().Result;
            ViewData["Clients"] = clients;
            ViewData["Vehicles"] = vehicles;
            ViewData["Bookings"] = bookings;
            return View();
        }
        public async Task<IActionResult> Insert(string addclientid, string addvehicleid, string addbookingfor,string addnotes)
        {
            var parsedDate = DateTime.ParseExact(addbookingfor, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var formattedDate = parsedDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            await da.BookingsInsert(int.Parse(addclientid),int.Parse(addvehicleid), formattedDate, addnotes);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Bookings", null);
            return redirectResult;
        }
        public async Task<IActionResult> Edit(string editid,string editclientid, string editvehicleid, string editbookingfor, string editnotes)
        {
            await da.BookingsUpdate(int.Parse(editid), int.Parse(editclientid), int.Parse(editvehicleid), editbookingfor, editnotes);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Bookings", null);
            return redirectResult;
        }
        public async Task<IActionResult> Delete(string deletebookingid)
        {
            await da.VehicleDelete(int.Parse(deletebookingid));
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Bookings", null);
            return redirectResult;
        }
    }
}