using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime MemberSince { get; set; }

        // Navigation properties
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

/*
Properties: MemberId(string), Name(string), Email(string), MemberSince(DateTime)
Properties för att hålla reda på lånade böcker
Metod för att visa medlemsinformation
*/