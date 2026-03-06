using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace MyBankApp{
    class Program{
        static void Main(string[] args){

            DataManagement dataManagement = new DataManagement();
            BankData data = dataManagement.LoadAllData();
            
            List<BankAccount> allAccounts = data.allAccounts;
            List<BankAccount> currentAccounts = data.currentAccounts;
            List<BankAccount> savingsAccounts = data.savingsAccounts;   
            List<BankAccount> userAccounts = data.userAccounts;
            Transactions transactions = new Transactions();
            Create create = new Create();
            Show show = new Show();
            Connection conn = new Connection();
            Changes changes = new Changes();
            BankAccount logged = new BankAccount();
            CurrentAccount current = new CurrentAccount();
            SavingsAccount savings = new SavingsAccount();

            int choice,choice1,choice2,choice3,choice4,choice5,choice6;
            string afm = null;
            
            Console.WriteLine("---WELCOME TO THE BANK APP---");
            while(true){
                Console.WriteLine("\n---MENU---");
                Console.WriteLine("0) Exit.");
                Console.WriteLine("1) Create Account.");
                Console.WriteLine("2) Log In.");
                Console.WriteLine("3) See Accounts.");  //FOR ADMIN
                Console.WriteLine("4) Delete Data."); //FOR ADMIN

                choice = Validations.IsIntNumber();

                switch (choice){
                    case 0:
                        dataManagement.SaveAllData(allAccounts, userAccounts, 
                                                   currentAccounts, savingsAccounts);
                        return;
                    case 1:
                        create.CreateAccount(currentAccounts, savingsAccounts, allAccounts);
                        break;
                    case 2:
                        afm = conn.Login(allAccounts);
                        if(afm==null){
                            Console.WriteLine("\nNo Accounts Created or Wrong Inputs.");
                            break;
                        }
                        userAccounts = conn.UserAccounts(allAccounts, afm);
                        
                        bool flag=true;
                        while(flag){
                            Console.WriteLine("\n----ACCOUNT MENU----");
                            Console.WriteLine("0) Log Out.");
                            Console.WriteLine("1) Create Savings Account.");
                            Console.WriteLine("2) Transactions.");
                            Console.WriteLine("3) See Details.");
                            Console.WriteLine("4) Options.");

                            choice1=Validations.IsIntNumber();

                            switch (choice1){ 
                                case 0:
                                    flag=false;
                                    break;
                                case 1:
                                    logged = conn.loggedAccount(userAccounts);
                                    create.CreateSavingsAccount(currentAccounts,savingsAccounts,
                                                                allAccounts,userAccounts,logged);
                                    break;
                                case 2: 
                                    Console.WriteLine("\n---TRANSACTIONS MENU---");
                                    Console.WriteLine("0) Back to Previous Menu.");
                                    Console.WriteLine("1) Deposit.");
                                    Console.WriteLine("2) Withdraw.");
                                    Console.WriteLine("3) Transfer Funds.");
                                    choice4 = Validations.IsIntNumber();

                                    switch (choice4){
                                        case 0:
                                            break;
                                        case 1:
                                            transactions.Deposit(userAccounts, dataManagement, 
                                                                 allAccounts, currentAccounts, 
                                                                 savingsAccounts);
                                            break;
                                        case 2:
                                            transactions.Withdraw(userAccounts, dataManagement, 
                                                                  allAccounts, currentAccounts, 
                                                                  savingsAccounts);
                                            break;
                                        case 3:
                                            savings = conn.Savings(userAccounts);
                                            current = conn.Current(userAccounts);
                                            transactions.Transfer(current,savings,
                                                                  userAccounts,dataManagement,
                                                                  allAccounts,currentAccounts,
                                                                  savingsAccounts);
                                            break;
                                        default:
                                            Console.WriteLine("\nWrong Choice.");
                                            break;
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("\n---SEE DETAILS---");
                                    Console.WriteLine("0) Back to Previous Menu.");
                                    Console.WriteLine("1) See Transactions.");
                                    Console.WriteLine("2) See your Balance.");
                                    Console.WriteLine("3) See My Profile.");
                                    choice5 = Validations.IsIntNumber();

                                    switch (choice5){
                                        case 0:
                                            break;
                                        case 1:
                                            show.ShowTransactions(userAccounts);
                                            break;
                                        case 2:
                                            Console.WriteLine("\n---SHOW MENU---");
                                            Console.WriteLine("0) Back to Previous Menu.");
                                            Console.WriteLine("1) Show My Current Account Balance.");
                                            Console.WriteLine("2) Show My Savings Account Balance.");

                                            choice2 = Validations.IsIntNumber();

                                            switch(choice2){
                                                case 0:
                                                    break;
                                                case 1:
                                                    bool status = false;
                                                    foreach (var acc in userAccounts){
                                                        if(acc is CurrentAccount){
                                                            show.ShowBalance(acc);
                                                            status = true;
                                                        }
                                                    }
                                                    if(!(status)){
                                                        Console.WriteLine("\nThis Account has not created a Current Account.");
                                                    }
                                                    break;
                                                case 2:
                                                    bool status1 = false;
                                                    foreach (var acc in userAccounts){
                                                        if(acc is SavingsAccount){
                                                            show.ShowBalance(acc);
                                                            status1 = true;
                                                        }
                                                    }
                                                    if(!(status1)){
                                                        Console.WriteLine("\nThis Account has not created a Savings Account.");
                                                    }
                                                    break;
                                                default:
                                                    Console.WriteLine("\nWrong Choice.");
                                                    break;
                                            }
                                            break;
                                        case 3:
                                            logged = conn.loggedAccount(userAccounts);
                                            show.ShowProfile(userAccounts,logged);
                                            break;
                                        default:
                                            Console.WriteLine("\nWrong Choice.");
                                            break;
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("\n---OPTIONS MENU---");
                                    Console.WriteLine("0) Back to Previous Menu.");
                                    Console.WriteLine("1) Edit My Profile.");
                                    Console.WriteLine("2) Delete Savings Account.");
                                    Console.WriteLine("3) Delete Account.");
                                    choice6 = Validations.IsIntNumber();

                                    switch (choice6){
                                        case 0:
                                            break;
                                        case 1:
                                            Console.WriteLine("\n--------EDIT MENU--------");
                                            Console.WriteLine("0) Back to Previous Menu.");
                                            Console.WriteLine("1) Edit Afm.");
                                            Console.WriteLine("2) Edit Password.");
                                            Console.WriteLine("3) Edit Name.");
                                            choice3=Validations.IsIntNumber();

                                            switch (choice3){
                                                case 0:
                                                    break;
                                                case 1:
                                                    changes.EditAfm(userAccounts,dataManagement,
                                                            allAccounts,currentAccounts,
                                                            savingsAccounts);
                                                    break;
                                                case 2:
                                                    changes.EditPassword(userAccounts,dataManagement,
                                                                 allAccounts,currentAccounts,
                                                                 savingsAccounts);
                                                    break;
                                                case 3:
                                                    changes.EditName(userAccounts,dataManagement,
                                                                     allAccounts,currentAccounts,
                                                                     savingsAccounts);
                                                    break;
                                                default:
                                                    Console.WriteLine("\nInvalid Choice.");
                                                    break;
                                            }
                                            break;
                                        case 2:
                                            savings = conn.Savings(userAccounts);
                                            current = conn.Current(userAccounts);
                                            changes.DeleteSavingsAccount(userAccounts,dataManagement,
                                                                         allAccounts,currentAccounts,
                                                                         savingsAccounts,savings,current);
                                            break;
                                        case 3:
                                            savings = conn.Savings(userAccounts);
                                            current = conn.Current(userAccounts);
                                            changes.DeleteAccount(userAccounts,dataManagement,
                                                                  allAccounts,currentAccounts,
                                                                  savingsAccounts,savings,current);
                                            flag=false;
                                            break;
                                        default:
                                            Console.WriteLine("\nWrong Choice.");
                                            break;
                                    }
                                    break;
                                default:
                                    Console.WriteLine("\nInvalid Choice.");
                                    break;
                            }
                        }
                        break;
                    case 3:
                        if(allAccounts.Count==0){
                            Console.WriteLine("\nThere are no Accounts Created.");
                            break;
                        }
                        Console.WriteLine("\n---ALL ACCOUNTS---");
                        show.ShowAccounts(allAccounts);
                        break;
                    case 4:
                        dataManagement.DeleteAllData(allAccounts, userAccounts, 
                                                     currentAccounts, savingsAccounts);
                        break;
                    default:
                        Console.WriteLine("\nInvalid Choice.");
                        break;
                }
            }
        }
    }
}
