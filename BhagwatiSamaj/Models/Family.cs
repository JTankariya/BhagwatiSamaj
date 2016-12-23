using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class Family
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string CurrentAddress { get; set; }
        public string PermenentAddress { get; set; }
        public string NativePlace { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string MerriageVillage { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherMiddleName { get; set; }
        public string FatherLastName { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherMiddleName { get; set; }
        public string MotherLastName { get; set; }
        public string Education { get; set; }
        public string OccupationType { get; set; }
        public string BusinessTitle { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPosition { get; set; }
        public string AnnualIncome { get; set; }
        public string BusinessContactNo { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public bool IsPublished { get; set; }
        public string PhotoPath { get; set; }
        public string MaritalStatus { get; set; }
        public string FatherOccupationType { get; set; }
        public string FatherBusinessTitle { get; set; }
        public string FatherBusinessAddress { get; set; }
        public string FatherBusinessPosition { get; set; }
        public string FatherAnnualIncome { get; set; }
        public string FatherBusinessContactNo { get; set; }
        public string FatherImagePath { get; set; }
        public string MotherOccupationType { get; set; }
        public string MotherBusinessTitle { get; set; }
        public string MotherBusinessAddress { get; set; }
        public string MotherBusinessPosition { get; set; }
        public string MotherAnnualIncome { get; set; }
        public string MotherBusinessContactNo { get; set; }
        public string MotherImagePath { get; set; }
        public string PinCode { get; set; }
        public string WebSite { get; set; }
        public string OtherSkills { get; set; }
        public int CreatedBy { get; set; }

        public List<FamilyMember> members { get; set; }
    }
}