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
    /// Client Controller meant to handle client requests 
    /// </summary>
    public class ClientController : Controller
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
        /// <summary>
        /// The full list of clients will be returned
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(string message)
        {
            var clients = da.ClientGetList().Result;
            ViewData["Clients"] = clients;
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
        /// Allows user to handle request for inserting a new client
        /// </summary>
        /// <param name="addclientfirstname"></param>
        /// <returns></returns>
        public async Task<IActionResult> Insert(string addclientfirstname)
        {
            await da.ClientInsert(addclientfirstname);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Client", null);
            return redirectResult;
        }
        /// <summary>
        /// Allows user to handle request for editing an existing client
        /// </summary>
        /// <param name="editclientid"></param>
        /// <param name="editclientfirstname"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string editclientid, string editclientfirstname)
        {
            await da.ClientUpdate(int.Parse(editclientid), editclientfirstname);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Client", null);
            return redirectResult;
        }
        /// <summary>
        /// Allows user to handle request for deleting a client
        /// </summary>
        /// <param name="deleteclientid"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string deleteclientid)
        {
            bool isdeleted = await da.ClientDelete(int.Parse(deleteclientid));
            if (isdeleted)
            {
                RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Client", null);
                return redirectResult;
            }
            else
            {
                RedirectToActionResult redirectResult = new RedirectToActionResult("Index", "Client", new { message = "Please remove booking linked to this client before removing this client." });
                return redirectResult;
            }
            
        }
    }
}