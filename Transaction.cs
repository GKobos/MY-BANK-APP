using System;

namespace MyBankApp{
    public class Transaction{
        public string Type{get; set;}
        public decimal Amount{get; set;}
        public string Details{get; set;}
        public DateTime Date { get; set; }

        public Transaction(string type,decimal amount,string details){
            Type=type;
            Amount=amount;
            Details=details;
            Date = DateTime.Now;
        }
    }
}