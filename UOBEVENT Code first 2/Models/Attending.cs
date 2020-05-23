using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace UOBEVENT_Code_first_2.Models
{
    public class Attending
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendID { get; set; }

        //[ForeignKey("Event")]
        [Key, Column(Order = 1)]
        public int EventID { get; set; }
        [Key, Column(Order = 2)]
        public string UserID { get; set; }
     
        public string UserCourse { get; set; }


        public Event Event { get; set; }
    }
}