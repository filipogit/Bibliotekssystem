using System;
using System.ComponentModel.DataAnnotations;
using Bibliotekssystem.Interfaces;

namespace Bibliotekssystem.Models
{
    public class LibraryItem : ISearchable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titel är obligatoriskt.")]
        [StringLength(200, ErrorMessage = "Titel får max vara 200 tecken.")]
        public string Title { get; set; } = string.Empty;

        [Range(1000, 2100, ErrorMessage = "Ange ett giltigt utgivningsår.")]
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? BorrowedBy { get; set; }

        public LibraryItem() { }

        public LibraryItem(string title, int publishedYear)
        {
            Title = title;
            PublishedYear = publishedYear;
        }

        public virtual void CheckOut(string borrowerName)
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                BorrowedBy = borrowerName;
                Console.WriteLine($"{Title} har lånats ut till {borrowerName}.");
            }
            else
            {
                Console.WriteLine($"{Title} är redan utlånad till {BorrowedBy}.");
            }
        }

        public virtual void Return()
        {
            if (!IsAvailable)
            {
                Console.WriteLine($"{Title} har returnerats av {BorrowedBy}.");
                IsAvailable = true;
                BorrowedBy = null;
            }
            else
            {
                Console.WriteLine($"{Title} är redan tillgänglig.");
            }
        }

        // Implementering av ISearchable
        public virtual bool Matches(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return false;

            searchTerm = searchTerm.ToLower();

            return Title.ToLower().Contains(searchTerm) ||
                   Id.ToString().Contains(searchTerm) ||
                   PublishedYear.ToString().Contains(searchTerm);
        }

        public virtual string GetInfo()
        {
            string status = IsAvailable ? "Tillgänglig" : $"Utlånad till {BorrowedBy}";
            return $"ID: {Id}, Titel: {Title}, Utgiven: {PublishedYear}, Status: {status}";
        }
    }
}