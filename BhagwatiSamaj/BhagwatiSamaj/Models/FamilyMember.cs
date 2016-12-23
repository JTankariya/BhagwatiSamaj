using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class FamilyMember
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string AnnualIncome { get; set; }
        public string BusinessTitle { get; set; }
        public string BusinessPosition { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessContactNo { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string OccupationType { get; set; }
        public string Relation { get; set; }
        public string Education { get; set; }
        public string OtherSkills { get; set; }
        public string PhotoPath { get; set; }
        public string MobileNo { get; set; }
    }
}