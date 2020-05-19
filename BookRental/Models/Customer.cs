using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class Customer
    {
        public int Id { get; set; }
        //[Required(ErrorMessage ="Please enter customer's name")] to change UI error message
        [Required]      // Data Anotation other examples [Range(1,10)],  [Compare("OtherProperty")],...
        [StringLength(255)]
        public string Name { get; set; }

        //[Display(Name = "Date of Birth")]  to display the text in the UI
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}