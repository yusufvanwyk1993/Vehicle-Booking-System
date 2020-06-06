using BookingSystem.Models;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Database
{
    public class DataAccess
    {
        /// <summary>
        /// stores current instance connectionstring
        /// </summary>
        private string connectionstring;
        /// <summary>
        /// Ensures user must have connectionstring in order to requests to database
        /// </summary>
        /// <param name="connectionstring"></param>
        public DataAccess(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        #region Clients
        /// <summary>
        /// Requests a list of clients from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Client>> ClientGetList()
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var clients = await connection.QueryAsync<Client>("Select id, firstname from Client;");
                return clients;
            }
        }
        /// <summary>
        /// Allows user to insert new client into database
        /// </summary>
        /// <param name="firstname"></param>
        /// <returns></returns>
        public async Task<bool> ClientInsert(string firstname)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var result = await connection.ExecuteAsync($"Call clientinsert('{firstname}')");
                return true;
            }
        }
        /// <summary>
        /// Allows user to update an existing client on the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstname"></param>
        /// <returns></returns>
        public async Task<bool> ClientUpdate(int id, string firstname)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var result = await connection.ExecuteAsync($"Call clientupdate({id},'{firstname}')");
                return true;
            }
        }
        /// <summary>
        /// Allows user to delete a specific client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ClientDelete(int id)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var result = await connection.ExecuteAsync($"Call clientdelete({id.ToString()})");
                return true;
            }
        }
        #endregion

        #region Vehicles
        /// <summary>
        /// Requests list of vehicles from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Vehicle>> VehicleGetList()
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var vehicles = await connection.QueryAsync<Vehicle>("Select id, vehiclemodel from Vehicle;");
                return vehicles;
            }
        }
        /// <summary>
        /// Allows user to insert a new vehicle into database
        /// </summary>
        /// <param name="vehiclemodel"></param>
        /// <returns></returns>
        public async Task<bool> VehicleInsert(string vehiclemodel)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var result = await connection.ExecuteAsync($"Call vehicleinsert('{vehiclemodel}')");
                return true;
            }
        }
        /// <summary>
        /// Allows user to update existing vehicle in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehiclemodels"></param>
        /// <returns></returns>
        public async Task<bool> VehicleUpdate(int id, string vehiclemodels)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var result = await connection.ExecuteAsync($"Call vehicleupdate({id},'{vehiclemodels}')");
                return true;
            }
        }
        /// <summary>
        /// Allows user to delete specific vehicle from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> VehicleDelete(int id)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var result = await connection.ExecuteAsync($"Call vehicledelete({id.ToString()})");
                return true;
            }
        }
        #endregion

        #region Vehicle Booking
        /// <summary>
        /// Requests list of vehicle bookings
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleBooking>> BookingsGetList()
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var vehiclebookings = await connection.QueryAsync<VehicleBooking>("select b.id, b.clientid, c.firstname as clientfirstname, b.vehicleid, v.vehiclemodel, b.bookedfor, b.notes from Bookings b inner join Client c on b.clientid = c.id inner join Vehicle v on b.vehicleid = v.id");
                return vehiclebookings;
            }
        }
        /// <summary>
        /// Allows user to book a vehicle by providing client, vehicle and notes if any
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="vehicleid"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public async Task<bool> BookingsInsert(int clientid, int vehicleid, string bookingfor, string notes)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var result = await connection.ExecuteAsync($"Call bookinginsert({clientid},{vehicleid},'{bookingfor}','{notes}')");
                return true;
            }
        }
        /// <summary>
        /// Allows user to update a specific booking by providing booking id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientid"></param>
        /// <param name="vehicleid"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public async Task<bool> BookingsUpdate(int id, int clientid, int vehicleid, string bookingfor, string notes)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var result = await connection.ExecuteAsync($"Call bookingupdate({id},{clientid},{vehicleid},{bookingfor},'{notes}')");
                return true;
            }
        }
        /// <summary>
        /// Allows user to delete booking from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> BookingsDelete(int id)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                var result = await connection.ExecuteAsync($"Call bookingdelete({id.ToString()})");
                return true;
            }
        }
        #endregion

    }
}
