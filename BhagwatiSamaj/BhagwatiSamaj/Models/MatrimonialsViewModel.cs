using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class MatrimonialsViewModel : Matrimonial
    {
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
    }
}