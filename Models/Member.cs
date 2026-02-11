using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Models
{
    public class Member
    {
        public Member(string memberId, string name, string email, DateTime memberSince)
        {
            MemberId = memberId;
            Name = name;
            Email = email;
            MemberSince = memberSince;
        }
        public string MemberId { get; init; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime MemberSince { get; set; }

        public string GetInfo()
        {
            return $"Member ID: {MemberId}, Name: {Name}, Email: {Email}, Member Since: {MemberSince}";
        }   
    }
}

/*
Properties: MemberId(string), Name(string), Email(string), MemberSince(DateTime)
Properties för att hålla reda på lånade böcker
Metod för att visa medlemsinformation
*/