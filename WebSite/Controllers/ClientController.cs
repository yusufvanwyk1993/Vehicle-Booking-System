using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebSite.Controllers
{
    public class ClientController : Controller
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        IConfiguration configuration;
        const string appsettingsjsonfile = "AppSettings.json";
        DataAccess da;
        public ClientController()
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
            var clients = da.ClientGetList().Result;
            ViewData["Message"] = clients;
            return View();
        }
        public async Task<IActionResult> Insert(string addclientfirstname)
        {
            await da.ClientInsert(addclientfirstname);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Client", null);
            return redirectResult;
        }
        public async Task<IActionResult> Edit(string editclientid, string editclientfirstname)
        {
            await da.ClientUpdate(int.Parse(editclientid), editclientfirstname);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Client", null);
            return redirectResult;
        }
        public async Task<IActionResult> Delete(string deleteclientid)
        {
            await da.ClientDelete(int.Parse(deleteclientid));
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Client", null);
            return redirectResult;
        }
    }
}