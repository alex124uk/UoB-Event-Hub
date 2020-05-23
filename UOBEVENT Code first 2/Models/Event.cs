using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace UOBEVENT_Code_first_2.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [Display(Name = "Name of Event")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Event Date ")]
        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }

        [Required]
        [Display(Name = "Event Time")]
        [DataType(DataType.Time)]
        public DateTime? EventTime { get; set; }

        [Required]
        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }

        [Required]
        [Display(Name = "Event Room")]
        public string EventRoom { get; set; }


        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }

        [Required]
        [Display(Name = "Event Category")]
        public string EventCategory { get; set; }

        [Display(Name = "Creation date and time")]
        public DateTime CreationDT { get; set; }

        
        



        public string EventUserID { get; set; }

        public ICollection<Attending> Attendings { get; set; }
    }

    public class EventDBContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Attending> Attendings { get; set; }
    }
}