using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class FamilyViewModel
    {
        public Family family { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; }

        public FamilyViewModel() {
            FamilyMembers = new List<FamilyMember>();
        }
    }
}