using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Models
{
    public class Magazine : LibraryItem
    {
        public int IssueNumber { get; set; }
        public string Publisher { get; set; }
        public string Month { get; set; }

        public Magazine(string id, string title, int publishedYear, int issueNumber, string publisher, string month)
            : base(id, title, publishedYear)
        {
            IssueNumber = issueNumber;
            Publisher = publisher;
            Month = month;
        }

        public override bool Matches(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return false;

            searchTerm = searchTerm.ToLower();

            return base.Matches(searchTerm) ||
                   Publisher.ToLower().Contains(searchTerm) ||
                   Month.ToLower().Contains(searchTerm) ||
                   IssueNumber.ToString().Contains(searchTerm);
        }

        public override string GetInfo()
        {
            string status = IsAvailable ? "Tillgänglig" : $"Utlånad till {BorrowedBy}";
            return $"TIDNING - Titel: {Title}, Utgåva: #{IssueNumber}, Utgivare: {Publisher}, Månad: {Month}, Status: {status}";
        }
    }
}