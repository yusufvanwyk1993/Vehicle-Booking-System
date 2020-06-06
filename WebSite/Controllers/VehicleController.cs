using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebSite.Controllers
{
    public class VehicleController : Controller
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        IConfiguration configuration;
        const string appsettingsjsonfile = "AppSettings.json";
        DataAccess da;
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
        public IActionResult Index()
        {
            var vehicles = da.VehicleGetList().Result;
            ViewData["Message"] = vehicles;
            return View();
        }
        public async Task<IActionResult> Insert(string addvehiclemodel)
        {
            await da.VehicleInsert(addvehiclemodel);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Vehicle", null);
            return redirectResult;
        }
        public async Task<IActionResult> Edit(string editvehicleid, string editvehiclemodelname)
        {
            await da.VehicleUpdate(int.Parse(editvehicleid), editvehiclemodelname);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Vehicle", null);
            return redirectResult;
        }
        public async Task<IActionResult> Delete(string deletevehicleid)
        {
            await da.VehicleDelete(int.Parse(deletevehicleid));
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Vehicle", null);
            return redirectResult;
        }
    }
}