using System;
using System.ComponentModel.DataAnnotations;

namespace Bibliotekssystem.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Namn är obligatoriskt.")]
        [StringLength(100, ErrorMessage = "Namn får max vara 100 tecken.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-post är obligatoriskt.")]
        [EmailAddress(ErrorMessage = "Ange en giltig e-postadress.")]
        public string Email { get; set; } = string.Empty;

        public DateTime MemberSince { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public Member() { }

        public Member(string name, string email, DateTime memberSince)
        {
            Name = name;
            Email = email;
            MemberSince = memberSince;
        }

        public Member(string name, string email)
            : this(name, email, DateTime.Now)
        {
        }
    }
}