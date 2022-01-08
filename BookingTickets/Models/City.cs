using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookingTickets.Models
{
    public class City
    {
        [Key]
        public int CITYID { get; set; }
        public string CITYCODE { get; set; }

        [DisplayName("City Name")]
        [Required(ErrorMessage = "This filed is required!")]
        public string CITY_NAME { get; set; }
    }
}