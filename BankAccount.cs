using System;
using System.Collections.Generic;

namespace MyBankApp{
    public class BankAccount{
        public string Afm{get; set;}
        public string Password{get; set;}
        public string OwnerFirstName{get; set;}
        public string OwnerLastName{get; set;}
        public decimal Balance {get; set;}
    
        public virtual string AccountType {get;}

        public List<Transaction> trans { get; private set; } = new List<Transaction>();

        public BankAccount() { }

        public BankAccount(string afm, string password, string ownerFirstName, string ownerLastName, decimal InitialBalance){
            Afm=afm;
            Password=password;
            OwnerFirstName=ownerFirstName;
            OwnerLastName=ownerLastName;
            Balance=InitialBalance;
        }

        public virtual void Deposit(decimal DepositAmount){
            Balance+=DepositAmount;
            trans.Add(new Transaction("Deposit", DepositAmount, "ATM Deposit"));
            Console.WriteLine($"\nYou deposited {DepositAmount} Euros.");
            Console.WriteLine($"\nThe New Balance is {Balance}.");
        }

        public virtual void Withdraw(decimal WithdrawAmount){
            throw new NotImplementedException("Withdraw must be overridden in child classes");
        }
    }
}