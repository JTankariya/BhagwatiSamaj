using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class Advertize
    {
        public int Id { get; set; }
        public string FirmName { get; set; }
        public DateTime StartFrom { get; set; }
        public DateTime EndFrom { get; set; }
        public string AdvertizePath { get; set; }

        public string Description { get; set; }
        public Advertize()
        {
            StartFrom = DateTime.Now;
            EndFrom = DateTime.Now;
        }

    }
}