using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Models
{
    public class DVD : LibraryItem
    {
        public string Director { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }

        public DVD(string id, string title, int publishedYear, string director, int duration, string genre)
            : base(id, title, publishedYear)
        {
            Director = director;
            Duration = duration;
            Genre = genre;
        }

        public override bool Matches(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return false;

            searchTerm = searchTerm.ToLower();

            return base.Matches(searchTerm) ||
                   Director.ToLower().Contains(searchTerm) ||
                   Genre.ToLower().Contains(searchTerm);
        }

        public override string GetInfo()
        {
            string status = IsAvailable ? "Tillgänglig" : $"Utlånad till {BorrowedBy}";
            return $"DVD - Titel: {Title}, Regissör: {Director}, Längd: {Duration} min, Genre: {Genre}, Status: {status}";
        }

        public string GetDurationFormatted()
        {
            int hours = Duration / 60;
            int minutes = Duration % 60;
            return $"{hours}h {minutes}min";
        }
    }
}