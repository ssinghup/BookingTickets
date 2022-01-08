using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookingTickets.Models
{
    public class Flights
    {
        [Key]
        public int FLIGHTID { get; set; }



        [DisplayName("Flight Code")]
        [Required(ErrorMessage = "This filed is required!")]
        public string FLIGHT_CODE { get; set; }



        [DisplayName("Flight Name")]
        [Required(ErrorMessage = "This filed is required!")]
        public string FLIGHT_NAME { get; set; }



        [DisplayName("Airlines Name")]
        [Required(ErrorMessage = "This filed is required!")]
        public string COMPANY_NAME { get; set; }
        public Nullable<int> COMPANY_ID { get; set; }
        public Nullable<int> Time_ID { get; set; }
        public int From_Id { get; set; }
        public int To_Id { get; set; }
        public DateTime SelectedDate { get; set; }
        public string FROM_CITYNAME { get; set; }
        public string TO_CITYNAME { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Duration { get; set; }
        public int Fare { get; set; }



        public Nullable<int> ACTIVE { get; set; }

      public virtual  FLIGHT_COMPANY FLIGHT_COMPANY { get; set; }
    }
}