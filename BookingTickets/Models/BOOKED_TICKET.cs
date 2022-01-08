using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingTickets.Models
{
    public class BOOKED_TICKET
    {
        public int BOOKING_ID { get; set; }
        public int FLIGHT_ID { get; set; }
        public int FROMCITYID { get; set; }
        public int TOCITYID { get; set; }
        public int TIME_ID { get; set; }
        public System.DateTime BOOKING_DT { get; set; }
        public int LOGIN_USRID { get; set; }
        public string PERSON_NAME { get; set; }
        public Nullable<int> PERSON_AGE { get; set; }
        public string PERSON_EMAIL { get; set; }
        public string PERSON_MOBILE { get; set; }
        public Nullable<decimal> BOOKED_FARE { get; set; }
        public Nullable<System.DateTime> CURRENT_DT { get; set; }
        public Nullable<int> STATUS { get; set; }
        public Nullable<System.DateTime> CANCELLED_DT { get; set; }
    }
}