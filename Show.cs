using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBankApp{
    public class Show{
        public void ShowAccounts(List<BankAccount> accounts){
            int count=1;
            foreach (var acc in accounts){

                var SortedAccs = accounts.OrderByDescending(t => t.OwnerLastName).ToList();

                foreach (var sa in SortedAccs){
                    Console.WriteLine($"\nAccount {count}: First Name: {sa.OwnerFirstName} |"
                                      +$" Last Name: {sa.OwnerLastName} | Type: {sa.AccountType} |"
                                      +$" Balance: {sa.Balance} Euros.");
                    count++;
                }
                return;
            }
        }

        public void ShowBalance(BankAccount logged){
            
            Console.WriteLine($"\nThe Current Funds in your {logged.AccountType} is {logged.Balance} Euros.");
        }

        public void ShowProfile(List<BankAccount> userAccounts, BankAccount logged){
            
            Console.WriteLine("\n---------PROFILE---------");
            Console.WriteLine($"Afm: {logged.Afm}.");
            Console.WriteLine($"First Name: {logged.OwnerFirstName}.");
            Console.WriteLine($"Last Name: {logged.OwnerLastName}.");
            foreach (var acc in userAccounts){
                if(acc.Afm==logged.Afm){
                    if(acc is CurrentAccount)
                        Console.WriteLine($"Current Account Balance: {acc.Balance} Euros.");
                    if(acc is SavingsAccount)
                        Console.WriteLine($"Savings Account Balance: {acc.Balance} Euros.");
                }
            }
        }

        public void ShowTransactions(List<BankAccount> userAccounts){
            bool anyTransactions = userAccounts.Any(acc => acc.trans.Count > 0);
            

            if(!anyTransactions){
                Console.WriteLine("\nNo Transactions happened yet.");
                return; 
            }
            foreach (var acc in userAccounts){
        
            var sortedTrans = acc.trans.OrderByDescending(t => t.Date).ToList();

            foreach(var tr in sortedTrans){
                Console.WriteLine("\n---SHOW TRANSACTIONS---");
                Console.WriteLine($"Type: {tr.Type}."
                                  +$"\nAmount: {tr.Amount} Euros."
                                  +$"\nDetails: {tr.Details}."
                                  +$"\nDate: {tr.Date:dd-MM-yyyy HH:mm}"); 
                }
            }
            return;
        }
    }
}