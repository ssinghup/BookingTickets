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
    [RoutePrefix("api/City")]
    public class CityController : Controller
    {

        // GET: MVCCity
        // 

        [HttpGet]
        [Route("GetCity")]
        public async Task<ActionResult> Index()
        {
            List<City> SpcInfo = new List<City>();
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("City/GetAll");
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var CityResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                SpcInfo = JsonConvert.DeserializeObject<List<City>>(CityResponse);
            }
            //returning the employee list to view
            return View(SpcInfo);
        }

        [HttpGet]
        [Route("GetCityID")]
        public async Task<ActionResult> GetCityByID(int id)
        {
            List<City> SpcInfo = new List<City>();
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("City/GetByID?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var CityResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                CityResponse = "[" + CityResponse + "]";
                SpcInfo = JsonConvert.DeserializeObject<List<City>>(CityResponse);
            }
            //returning the employee list to view
            return View("Index", SpcInfo);
        }


        public ActionResult AddEdit(int id = 0)
        {
            if (id == 0)
                return View(new City());
            else
            {

                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("City/GetByID/" + id).Result;
                return View(response.Content.ReadAsAsync<City>().Result);

            }
        }
        [HttpPost]
        public ActionResult AddEdit(City city)
        {
            if (city.CITYID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("City/Insert", city).Result;
                TempData["Response"] = "Saved Successfully!";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("City/Update", city).Result;
                TempData["Response"] = "Updated Successfully!";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("City/Delete/" + id).Result;
            TempData["Response"] = "Deleted Successfully!";
            return RedirectToAction("Index");

        }
    }
}