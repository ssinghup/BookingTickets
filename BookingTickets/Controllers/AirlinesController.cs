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
    [RoutePrefix("api/Airlines")]
    public class AirlinesController : Controller
    {

        // GET: MVCCity
        // 

        [HttpGet]
        [Route("GetAirlines")]
        public async Task<ActionResult> Index()
        {
            List<FLIGHT_COMPANY> SpcInfo = new List<FLIGHT_COMPANY>();
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("FlightCompany/GetAll");
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var CityResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                SpcInfo = JsonConvert.DeserializeObject<List<FLIGHT_COMPANY>>(CityResponse);
            }
            //returning the employee list to view
            return View(SpcInfo);
        }

        [HttpGet]
        [Route("GetAirlinesByID")]
        public async Task<ActionResult> GetAirlinesByID(int id)
        {
            List<FLIGHT_COMPANY> SpcInfo = new List<FLIGHT_COMPANY>();
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("FlightCompany/GetByID?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var CityResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                CityResponse = "[" + CityResponse + "]";
                SpcInfo = JsonConvert.DeserializeObject<List<FLIGHT_COMPANY>>(CityResponse);
            }
            //returning the employee list to view
            return View("Index", SpcInfo);
        }


        public ActionResult AddEdit(int id = 0)
        {
            if (id == 0)
                return View(new FLIGHT_COMPANY());
            else
            {

                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("FlightCompany/GetByID/" + id).Result;
                return View(response.Content.ReadAsAsync<FLIGHT_COMPANY>().Result);

            }
        }
        [HttpPost]
        public ActionResult AddEdit(FLIGHT_COMPANY city)
        {
            if (city.CMPANYID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("FlightCompany/Insert", city).Result;
                TempData["Response"] = "Saved Successfully!";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("FlightCompany/Update", city).Result;
                TempData["Response"] = "Updated Successfully!";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("FlightCompany/Delete/" + id).Result;
            TempData["Response"] = "Deleted Successfully!";
            return RedirectToAction("Index");

        }
    }
}