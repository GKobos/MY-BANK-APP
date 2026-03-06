using System;
using System.Collections.Generic;

namespace MyBankApp{
    public class Connection{
        public string Login(List<BankAccount> allAccounts){
        
            string userName,password;
            for(int i=0; i<3; i++){
                Console.WriteLine("\n---LOGIN MENU---");
                Console.WriteLine($"Try Number:{i+1}/3.");
                Console.WriteLine("UserName:");
                userName = Console.ReadLine();
                Console.WriteLine("Password:");
                password = Console.ReadLine();
        
                foreach (var acc in allAccounts){
                    if(acc.Afm==userName && acc.Password==password){
                        Console.WriteLine($"\n{acc.OwnerFirstName} {acc.OwnerLastName} Connected Successfully.");
                        return(userName);
                    }
                }
                Console.WriteLine("\nWrong AFM or Password.");
            }
            return null;
        }

        public List<BankAccount> UserAccounts(List<BankAccount> allAccounts, string afm){

            List<BankAccount> userAccounts = new List<BankAccount>();

            foreach (var acc in allAccounts){
                if(acc.Afm==afm){
                    userAccounts.Add(acc);
                }
            }
            return(userAccounts);
        }

        public BankAccount loggedAccount(List<BankAccount> userAccounts){
            if (userAccounts.Count > 0){
                return userAccounts[0];
            }

            return null;
        }

        public CurrentAccount Current(List<BankAccount> userAccounts){
            foreach (var acc in userAccounts){
                if(acc is CurrentAccount c){
                    return c;
                }
            }
            return null;
        }

        public SavingsAccount Savings(List<BankAccount> userAccounts){
            foreach (var acc in userAccounts){
                if(acc is SavingsAccount s){
                    return s;
                }
            }
            return null;
        }
    }
}