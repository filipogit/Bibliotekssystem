using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Models
{
    public class LibraryItem
    {
        public string Id { get; init; }
        public string Title { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }

        public LibraryItem(string id, string title, int publishedYear)
        {
            Id = id;
            Title = title;
            PublishedYear = publishedYear;
            IsAvailable = true; // Standard: tillgänglig
        }

        public virtual void CheckOut()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                Console.WriteLine($"{Title} har lånats ut.");
            }
            else
            {
                Console.WriteLine($"{Title} är redan utlånad.");
            }
        }

        public virtual void Return()
        {
            if (!IsAvailable)
            {
                IsAvailable = true;
                Console.WriteLine($"{Title} har returnerats.");
            }
            else
            {
                Console.WriteLine($"{Title} är redan tillgänglig.");
            }
        }

        public virtual string GetInfo()
        {
            return $"ID: {Id}, Titel: {Title}, Utgiven: {PublishedYear}, Tillgänglig: {IsAvailable}";
        }
    }
}