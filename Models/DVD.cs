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
        public int Duration { get; set; } // minuter
        public string Genre { get; set; }

        public DVD(string id, string title, int publishedYear, string director, int duration, string genre)
            : base(id, title, publishedYear)
        {
            Director = director;
            Duration = duration;
            Genre = genre;
        }

        public override string GetInfo()
        {
            return $"DVD - ID: {Id}, Titel: {Title}, Regissör: {Director}, Längd: {Duration} min, Genre: {Genre}, Utgiven: {PublishedYear}, Tillgänglig: {IsAvailable}";
        }

        public string GetDurationFormatted()
        {
            int hours = Duration / 60;
            int minutes = Duration % 60;
            return $"{hours}h {minutes}min";
        }
    }
}