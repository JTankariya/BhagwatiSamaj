using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventAddress { get; set; }

        public List<EventUpload> EventUploads { get; set; }

        public Event()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

    }
}