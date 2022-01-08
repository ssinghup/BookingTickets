using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookingTickets.Models
{
    public class FLIGHT_COMPANY
    {
        [Key]
        public int CMPANYID { get; set; }
        [DisplayName("Airlines Name")]
        [Required(ErrorMessage = "This filed is required!")]
        public string COMPANY_NAME { get; set; }
    }
}