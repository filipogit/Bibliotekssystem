using Bibliotekssystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Models
{
    public class Loan
    {
        public Loan(string loanId, string memberId, string isbn, DateTime loanDate, DateTime? returnDate)
        {
            LoanId = loanId;
            MemberId = memberId;
            ISBN = isbn;
            LoanDate = loanDate;
            ReturnDate = returnDate;
        }
        public string LoanId { get; init; }
        public string MemberId { get; set; }
        public string ISBN { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string GetInfo()
        {
            return $"Loan ID: {LoanId}, Member ID: {MemberId}, ISBN: {ISBN}, Loan Date: {LoanDate}, Return Date: {(ReturnDate.HasValue ? ReturnDate.Value.ToString() : "Not returned")}";
        }
    }
}

/*
Properties: Book, Member, LoanDate, DueDate, ReturnDate?
Beräknad property IsOverdue som avgör om lånet är försenat
Beräknad property IsReturned
*/