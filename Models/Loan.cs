using System;

namespace Bibliotekssystem.Models
{
    public class Loan
    {
        public Book Book { get; init; }
        public Member Member { get; init; }
        public DateTime LoanDate { get; init; }
        public DateTime DueDate { get; init; }
        public DateTime? ReturnDate { get; set; }

        // Konstruktor med alla parametrar
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