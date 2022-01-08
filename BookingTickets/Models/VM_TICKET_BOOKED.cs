using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BookingTickets.Models
{

    public class VM_TICKET_BOOKED
    {
        public int BOOKING_ID { get; set; }
        public int FLIGHT_ID { get; set; }

        [DisplayName("Flight Name")]
        public string FLIGHT_NAME { get; set; }

        [DisplayName("Flight Code")]
        public string FLIGHT_CODE { get; set; }

        [DisplayName("Airlines")]
        public string COMPANY_NAME { get; set; }
        public int FROMCITYID { get; set; }
        public int TOCITYID { get; set; }
        public int TIME_ID { get; set; }
        public System.DateTime BOOKING_DT { get; set; }

        [DisplayName("Booking Date")]

        public string BOOKING_DATE { get; set; }
        public int LOGIN_USRID { get; set; }
        public Nullable<System.DateTime> CURRENT_DT { get; set; }
        public Nullable<int> STATUS { get; set; }
        public Nullable<System.DateTime> CANCELLED_DT { get; set; }


        [DisplayName("Person Name")]
        public string PERSON_NAME { get; set; }

        [DisplayName("Person Age")]
        public Nullable<int> PERSON_AGE { get; set; }

        [DisplayName("Person Email")]
        public string PERSON_EMAIL { get; set; }

        [DisplayName("Person Mobile")]
        public string PERSON_MOBILE { get; set; }

        [DisplayName("Ticket Fare")]
        public Nullable<decimal> BOOKED_FARE { get; set; }

        [DisplayName("From Location")]
        public string FROM_CITYNAME { get; set; }

        [DisplayName("To Location")]
        public string TO_CITYNAME { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Duration { get; set; }
    }

}