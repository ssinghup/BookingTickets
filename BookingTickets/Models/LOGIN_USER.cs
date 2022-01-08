using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BookingTickets.Models
{
    
public class LOGIN_USER
    {
        public int USERID { get; set; }

        [DisplayName("User Name")]
        public string USERNAME { get; set; }


        [DisplayName("Password")]
        public string USER_PASSWORD { get; set; }
        public string FNAME { get; set; }
        public string LNAME { get; set; }
        public string EMAIL { get; set; }
        public Nullable<int> ACTIVE { get; set; }
    }



}