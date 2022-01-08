using BookingTickets.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;



namespace BookingTickets.Controllers
{
    public class BookFlightController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> SearchFlight()
        {
            List<City> cities = new List<City>();
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("City/GetAll");
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var CityResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                cities = JsonConvert.DeserializeObject<List<City>>(CityResponse);
            }
            ViewBag.From_Id = new SelectList(cities, "CityId", "City_Name");
            ViewBag.To_Id = new SelectList(cities, "CityId", "City_Name");
            return View();
        }



        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchFlight(Flights model)
        {
            try
            {
                List<Flights> flights = new List<Flights>();

                 HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("Booking/GetFlightTime/" + model.From_Id + "/" + model.To_Id + "/" + model.SelectedDate.ToString("dd/MMM/yyyy"));

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    flights = JsonConvert.DeserializeObject<List<Flights>>(result);
                }
                TempData["SelectedFromId"] = model.From_Id;
                TempData["SelectedToId"] = model.To_Id;
                Session["SelectedFromId"] = model.From_Id;
                Session["SelectedToId"] = model.To_Id;
                Session["SelectedDate"] = model.SelectedDate;
                Session["FlightList"] = flights;
                return RedirectToAction("FlightList");
            }
            catch (Exception e)
            {
                TempData["Failure"] = "Invalid Data";
            }
            return RedirectToAction("Index", "Home");
        }



        public async Task<ActionResult> FlightList()
        {
            List<City> cities = new List<City>();
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("City/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var CityResponse = response.Content.ReadAsStringAsync().Result;
                cities = JsonConvert.DeserializeObject<List<City>>(CityResponse);
            }
            ViewBag.From_Id = new SelectList(cities, "CityId", "City_Name", TempData["SelectedFromId"]);
            ViewBag.To_Id = new SelectList(cities, "CityId", "City_Name", TempData["SelectedToId"]);
            if (Session["FlightList"] != null)
            {
                var result = Session["FlightList"];
                return View(result);
            }
            return View();
        }



        [HttpGet]
        public async Task<ActionResult> BookFlight(int flightId, int timeId, int fare)
        {
            if (Session["UserId"] != null)
            {
                BOOKED_TICKET details = new BOOKED_TICKET();
                details.FLIGHT_ID = flightId;
                details.FROMCITYID = Convert.ToInt32(Session["SelectedFromId"]);
                details.TOCITYID = Convert.ToInt32(Session["SelectedToId"]);
                details.TIME_ID = timeId;
                details.BOOKED_FARE = fare;
                details.LOGIN_USRID = Convert.ToInt32(Session["UserId"]);
                details.BOOKING_DT = Convert.ToDateTime(Session["SelectedDate"]);
                return View(details);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookFlight(BOOKED_TICKET model)
        {
            BOOKED_TICKET details = new BOOKED_TICKET();
            details.FLIGHT_ID = model.FLIGHT_ID;
            details.FROMCITYID = model.FROMCITYID;
            details.TOCITYID = model.TOCITYID;
            details.TIME_ID = model.TIME_ID;
            details.BOOKED_FARE = model.BOOKED_FARE;
            details.BOOKING_DT = model.BOOKING_DT;
            details.PERSON_NAME = model.PERSON_NAME;
            details.PERSON_AGE = model.PERSON_AGE;
            details.PERSON_EMAIL = model.PERSON_EMAIL;
            details.PERSON_MOBILE = model.PERSON_MOBILE;
            details.LOGIN_USRID = Convert.ToInt32(Session["UserId"]);
            details.CURRENT_DT = DateTime.Now;
            details.STATUS = 1;



            string data = JsonConvert.SerializeObject(details);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsync("BookedTicket/Insert", content).Result;
            if (response.IsSuccessStatusCode)
            {
                Task<string> result = response.Content.ReadAsStringAsync();
            }
            ViewBag.Message = "Ticket Booked Successfully !!!";
            return RedirectToAction("BookingDetails", "User");
        }
    }
}