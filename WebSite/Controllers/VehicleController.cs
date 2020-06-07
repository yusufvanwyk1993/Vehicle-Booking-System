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
    /// Vehicle Controller meant to handle vehicle requests 
    /// </summary>
    public class VehicleController : Controller
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
        public VehicleController()
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
        /// The full list of vehicles will be returned
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(string message)
        {
            var vehicles = da.VehicleGetList().Result;
            ViewData["Vehicles"] = vehicles;
            if (string.IsNullOrEmpty(message) | string.IsNullOrWhiteSpace(message))
            {
                ViewData["message"] = string.Empty;
            }
            else
            {
                ViewData["message"] = message;
            }
            return View();
        }
        /// <summary>
        /// Allows user to handle request for inserting a new vehicle
        /// </summary>
        /// <param name="addvehiclemodel"></param>
        /// <returns></returns>
        public async Task<IActionResult> Insert(string addvehiclemodel)
        {
            await da.VehicleInsert(addvehiclemodel);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Vehicle", null);
            return redirectResult;
        }
        /// <summary>
        /// Allows user to handle request for editing an existing vehicle
        /// </summary>
        /// <param name="editvehicleid"></param>
        /// <param name="editvehiclemodelname"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string editvehicleid, string editvehiclemodelname)
        {
            await da.VehicleUpdate(int.Parse(editvehicleid), editvehiclemodelname);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Vehicle", null);
            return redirectResult;
        }
        /// <summary>
        /// Allows user to handle request for deleting a vehicle
        /// </summary>
        /// <param name="deletevehicleid"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string deletevehicleid)
        {
            bool isdelted = await da.VehicleDelete(int.Parse(deletevehicleid));
            if (isdelted)
            {
                RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Vehicle", null);
                return redirectResult;
            }
            else
            {
                RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Vehicle", new { message = "Please remove booking linked to this vehicle before removing this vehicle." });
                return redirectResult;
            }
        }
    }
}