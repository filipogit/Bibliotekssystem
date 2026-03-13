using System;

namespace Bibliotekssystem.Models
{
    public class Loan
    {
        public int Id { get; set; }

        // Foreign keys
        public int BookId { get; set; }
        public int MemberId { get; set; }

        // Navigation properties
        public Book Book { get; set; } = null!;
        public Member Member { get; set; } = null!;

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Loan() { }

        public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            Book = book;
            Member = member;
            LoanDate = loanDate;
            DueDate = dueDate;
            ReturnDate = null;
        }

        // Beräknad egenskap: är lånet försenat?
        public bool IsOverdue
        {
            get
            {
                if (ReturnDate.HasValue)
                    return false; // Redan returnerad

                return DateTime.Now > DueDate;
            }
        }

        // Beräknad egenskap: är boken returnerad?
        public bool IsReturned => ReturnDate.HasValue;

        // Metod för att returnera boken
        public void ReturnBook()
        {
            if (!IsReturned)
            {
                ReturnDate = DateTime.Now;
                Book.Return();
            }
        }
    }
}