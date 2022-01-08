using BookingTickets.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;



namespace BookingTickets.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LOGIN_USER model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsync("Login/ValidateUser", content).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<LOGIN_USER>(result);
            }
            if (model.USERNAME != null)
            {
                FormsAuthentication.SetAuthCookie(model.USERNAME, true);
                Session["UserName"] = model.USERNAME;
                Session["UserId"] = model.USERID;
                return RedirectToAction("Index", "HOME");
            }
            ModelState.AddModelError("", "Invalid UserName and Password ");
            return View();
        }



        public ActionResult Logout()
        {
            Session["UserId"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }



        public async Task<ActionResult> BookingDetails()
        {
            if (Session["UserId"] != null)
            {
                int UserID = Convert.ToInt32(Session["UserId"]);
                List<VM_TICKET_BOOKED> data = new List<VM_TICKET_BOOKED>();
                HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("BookedTicket/GetBookedTicket/LoginUser=" + UserID);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<List<VM_TICKET_BOOKED>>(result);
                    return View(data);
                }
            }
            return View();
        }
    }
}