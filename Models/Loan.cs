using System;

namespace Bibliotekssystem.Models
{
    public class Loan
    {
        public Loan(string loanId, Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            LoanId = loanId;
            Book = book ?? throw new ArgumentNullException(nameof(book));
            Member = member ?? throw new ArgumentNullException(nameof(member));
            LoanDate = loanDate;
            DueDate = dueDate;
            ReturnDate = null;
        }

        public string LoanId { get; init; }
        public Book Book { get; init; }
        public Member Member { get; init; }
        public DateTime LoanDate { get; init; }
        public DateTime DueDate { get; init; }
        public DateTime? ReturnDate { get; set; }

        // Beräknad property: är lånet försenat?
        public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;

        // Beräknad property: är boken återlämnad?
        public bool IsReturned => ReturnDate.HasValue;

        public string GetInfo()
        {
            return $"Loan ID: {LoanId}, Member: {Member.Name}, Book: {Book.Title}, Loan Date: {LoanDate:yyyy-MM-dd}, Due Date: {DueDate:yyyy-MM-dd}, Return Date: {(ReturnDate.HasValue ? ReturnDate.Value.ToString("yyyy-MM-dd") : "Not returned")}, Overdue: {IsOverdue}";
        }
    }
}