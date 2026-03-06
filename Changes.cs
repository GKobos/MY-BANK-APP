using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBankApp{
    public class Changes{
        public void EditName(List<BankAccount> userAccounts, DataManagement dataManagement,
                            List<BankAccount> allAccounts,
                            List<BankAccount> currentAccounts,
                            List<BankAccount> savingsAccounts){
            string newFirstName, newLastName;
            
            Console.WriteLine("\nEnter the New First Name.");
            newFirstName=Validations.IsString();

            Console.WriteLine("\nEnter the New Last Name.");
            newLastName=Validations.IsString();

            foreach (var acc in userAccounts){
                acc.OwnerFirstName=newFirstName;
                acc.OwnerLastName=newLastName;
            }

            dataManagement.SaveAllData(allAccounts, userAccounts, currentAccounts, savingsAccounts);
            return;
        }

        public void EditAfm(List<BankAccount> userAccounts, DataManagement dataManagement,
                            List<BankAccount> allAccounts,
                            List<BankAccount> currentAccounts,
                            List<BankAccount> savingsAccounts){
            
            string newAfm;
            while(true){
                Console.WriteLine("\nEnter the New Afm.");
                newAfm=Console.ReadLine();
                if(newAfm.Length==9) break;
                Console.WriteLine("Invalid AFM");
            }

            foreach (var acc in userAccounts){
                acc.Afm=newAfm;
            }

            dataManagement.SaveAllData(userAccounts,allAccounts,currentAccounts,savingsAccounts);

            return;
        }

        public void EditPassword(List<BankAccount> userAccounts, DataManagement dataManagement,
                            List<BankAccount> allAccounts,
                            List<BankAccount> currentAccounts,
                            List<BankAccount> savingsAccounts){

            string newPassword;
            Console.WriteLine("Enter the New Password.");
            newPassword=Console.ReadLine();

            foreach (var acc in userAccounts){
                acc.Password=newPassword;
            }

            dataManagement.SaveAllData(userAccounts,allAccounts,currentAccounts,savingsAccounts);

            return;
        }

        public void DeleteSavingsAccount(List<BankAccount> userAccounts, DataManagement dataManagement,
                                         List<BankAccount> allAccounts,
                                         List<BankAccount> currentAccounts,
                                         List<BankAccount> savingsAccounts, 
                                         SavingsAccount savings, 
                                         CurrentAccount current){
            foreach (var acc in userAccounts){
                int savingsCount = userAccounts.Count(acc => acc is SavingsAccount);
                if(savingsCount==0){
                    Console.WriteLine("\nThis Account does not have a Savings Account.");
                    return;
                }
                if(acc is SavingsAccount){
                    string answer;
                    while(true){
                        Console.WriteLine("\nAre you sure you want to Delete Savings Account? (y/n)");
                        answer=Validations.IsString();
                        if(answer.ToLower()=="y"){
                            decimal amount = savings.Balance;
                            current.Deposit(amount);
                            savings.Withdraw(amount);
                            userAccounts.Remove(savings);
                            allAccounts.Remove(savings);
                            savingsAccounts.Remove(savings);
                            dataManagement.SaveAllData(userAccounts,allAccounts,
                                                       currentAccounts,savingsAccounts);
                            return;
                        }
                        else if(answer.ToLower()=="n"){
                            Console.WriteLine("\nDelete Cancelled.");
                            return;
                        }
                        Console.WriteLine("\nInvalid Answer.");
                    }
                }
            }
        }

        public void DeleteAccount(List<BankAccount> userAccounts, DataManagement dataManagement,
                                  List<BankAccount> allAccounts,
                                  List<BankAccount> currentAccounts,
                                  List<BankAccount> savingsAccounts, 
                                  SavingsAccount savings, 
                                  CurrentAccount current){
            string answer;
            while(true){
                Console.WriteLine("\nAre you Sure that you Want to Delete this Account? (y/n)");
                answer=Validations.IsString();

                if(answer.ToLower()=="y") break;
                else if(answer.ToLower()=="n") return;
                Console.WriteLine("\nInvalid Answer.");
            }
            
            decimal totalBalance=0;
            foreach (var acc in userAccounts){
                totalBalance+=acc.Balance;
            }

            if(totalBalance==0){
                foreach(var acc in userAccounts.ToList()){
                    allAccounts.Remove(acc);
                    currentAccounts.Remove(acc);
                    savingsAccounts.Remove(acc);
                }

                userAccounts.Clear();

                dataManagement.SaveAllData(userAccounts,allAccounts,
                                           currentAccounts,savingsAccounts);
                return;
            }
            Console.WriteLine("\nYou cannot Delete an Account with remaining or demanding funds.");
            return;
        }
    }
}