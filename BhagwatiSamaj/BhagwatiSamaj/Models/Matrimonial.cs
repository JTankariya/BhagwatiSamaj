using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class Matrimonial
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime BirthTime { get; set; }
        public string BirthPlace { get; set; }
        public string Hobby { get; set; }
        public string SkinColor { get; set; }
        public string Photo { get; set; }
        public string Education { get; set; }
        public string NativePlace { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string MosalName { get; set; }
        public string MosalVillage { get; set; }
        public string BusinessType { get; set; }
        public string AnnualIncome { get; set; }
        public string BusinessAddress { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string MobileNo { get; set; }
        public string OtherSkills { get; set; }
        public string BusinessTitle { get; set; }
        public string BusinessPosition { get; set; }
    }
}