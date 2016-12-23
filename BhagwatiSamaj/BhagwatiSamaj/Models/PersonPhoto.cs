using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class PersonPhoto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int CandidateFamilyId { get; set; }
        public string PhotoName { get; set; }

    }
}