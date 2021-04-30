using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class Transaction
    {
        public int TransactionId {get;set;}
        [Required]
        public Double Amount {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        //foreign key
        public int UserId {get;set;}
        // Navigation Property for related User object
        public User BankCustomer {get;set;}
    }
}