using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class EventModel
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Please Enter EventName")]
        public string Event_Name { get; set; }

        [Required(ErrorMessage = "Please Enter StartDate")]
        [DataType(DataType.Date, ErrorMessage = "Please Enter Valid date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please Enter EndDate")]
        [DataType(DataType.Date,ErrorMessage ="Please Enter Valid date")]              
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please Enter Event Description")]
        public string Event_des { get; set; }

        [Required(ErrorMessage = "Please Enter Location")]
        public string Location { get; set; }
    }
}