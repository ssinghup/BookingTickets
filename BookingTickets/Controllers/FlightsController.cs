using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingTickets.Models;
using System.Net.Http;
using BookingTickets;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace BookingTickets.Controllers
{
    [RoutePrefix("api/Flights")]
    public class FlightsController : Controller
    {

        // GET: MVCCity
        // 

        [HttpGet]
        [Route("GetFlights")]
        public async Task<ActionResult> Index()
        {
            List<Flights> SpcInfo = new List<Flights>();
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("Flight/GetAll");
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var CityResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                SpcInfo = JsonConvert.DeserializeObject<List<Flights>>(CityResponse);
            }
            //returning the employee list to view
            return View(SpcInfo);
        }

        [NonAction]
        public void GetAirlineList(int CityID)
        {
            List<FLIGHT_COMPANY> CityInfo = new List<FLIGHT_COMPANY>();
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("FlightCompany/GetAll").Result;
            var CityResponse = response.Content.ReadAsStringAsync().Result;
            CityInfo = JsonConvert.DeserializeObject<List<FLIGHT_COMPANY>>(CityResponse);
            if (CityID == 0)
                ViewBag.COMPANY_ID = new SelectList(CityInfo, "CMPANYID", "COMPANY_NAME");

            else
            {
                ViewBag.COMPANY_ID = new SelectList(CityInfo, "CMPANYID", "COMPANY_NAME", CityID);

            }
        }

        [HttpGet]
        [Route("GetFlightsByID")]
        public async Task<ActionResult> GetFlightsByID(int id)
        {
            List<Flights> SpcInfo = new List<Flights>();
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("Flight/GetByID?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var CityResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                CityResponse = "[" + CityResponse + "]";
                SpcInfo = JsonConvert.DeserializeObject<List<Flights>>(CityResponse);
            }
            //returning the employee list to view
            return View("Index", SpcInfo);
        }


        public ActionResult AddEdit(int id = 0, int CityId = 0)
        {

            this.GetAirlineList(CityId);
            if (id == 0)
                return View(new Flights());
            else
            {

                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Flight/GetByID/" + id).Result;
                return View(response.Content.ReadAsAsync<Flights>().Result);

            }
        }
        [HttpPost]
        public ActionResult AddEdit(Flights city)
        {
            if (city.FLIGHTID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Flight/Insert", city).Result;
                TempData["Response"] = "Saved Successfully!";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Flight/Update", city).Result;
                TempData["Response"] = "Updated Successfully!";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Flight/Delete/" + id).Result;
            TempData["Response"] = "Deleted Successfully!";
            return RedirectToAction("Index");

        }
    }
}