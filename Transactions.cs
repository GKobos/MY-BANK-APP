using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBankApp{
    public class Transactions{
        public void Deposit(List<BankAccount> userAccounts, DataManagement dataManagement,
                            List<BankAccount> allAccounts,
                            List<BankAccount> currentAccounts,
                            List<BankAccount> savingsAccounts){
            decimal DepositAmount;
            Console.WriteLine("\n--------DEPOSIT MENU--------");
            Console.WriteLine("0) Back to Previous Menu.");
            Console.WriteLine("1) Deposit to Current Account.");
            Console.WriteLine("2) Deposit to Savings Account.");
            int choice = Validations.IsIntNumber();

            switch (choice){
                case 0:
                    return;
                case 1:
                    bool status = false;
                    foreach (var acc in userAccounts){
                        if(acc is CurrentAccount){
                            while(true){
                                Console.WriteLine("\nEnter the Amount you want to Deposit.");
                                DepositAmount=Validations.IsDecimalNumber();
                                if(DepositAmount>=0){
                                    acc.Deposit(DepositAmount);
                                    dataManagement.SaveAllData(allAccounts, userAccounts, currentAccounts, savingsAccounts);
                                    status = true;
                                    break;
                                }
                                Console.WriteLine("\nThe Amount Cannot be Negative.");
                            }
                        }
                    }
                    if(!(status)){
                        Console.WriteLine("\nThis Account has not created a Current Account.");
                    }
                    break;
                case 2:
                    bool status1=false;
                    foreach (var acc in userAccounts){
                        if(acc is SavingsAccount){
                            while(true){
                                Console.WriteLine("\nEnter the Amount you want to Deposit.");
                                DepositAmount=Validations.IsDecimalNumber();
                                if(DepositAmount>=0){
                                    acc.Deposit(DepositAmount);
                                    dataManagement.SaveAllData(allAccounts, userAccounts, currentAccounts, savingsAccounts);
                                    status1 = true;
                                    break;
                                }
                                Console.WriteLine("\nThe Amount Cannot be Negative.");
                            }
                        }
                    }
                    if(!(status1)){
                        Console.WriteLine("\nThis Account has not created a Savings Account.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
        }
    

        public void Withdraw(List<BankAccount> userAccounts, DataManagement dataManagement,
                             List<BankAccount> allAccounts,
                             List<BankAccount> currentAccounts,
                             List<BankAccount> savingsAccounts){

            int choice;
            decimal WithdrawAmount;
            Console.WriteLine("\n--------WITHDRAW MENU----------");
            Console.WriteLine("0) Back to Previous Menu.");
            Console.WriteLine("1) Withdraw from Current Account.");
            Console.WriteLine("2) Withdraw from Savings Account.");
            choice=Validations.IsIntNumber();

            switch (choice){
                case 0:
                    return;
                case 1:
                    bool status=true;
                    foreach (var acc in userAccounts){
                        if(acc is CurrentAccount){
                            while(true){
                                Console.WriteLine("\nEnter the Amount you want to Withdraw.");
                                WithdrawAmount=Validations.IsDecimalNumber();
                                if(WithdrawAmount>=0){
                                    acc.Withdraw(WithdrawAmount);
                                    dataManagement.SaveAllData(allAccounts, userAccounts, currentAccounts, savingsAccounts);
                                    status = false;
                                    break;
                                }
                            }
                        }
                    }
                    if(!(status)){
                        Console.WriteLine("\nThis Account has not created a Current Account.");
                    }
                    break;
                case 2:
                    bool status1=true;
                    foreach (var acc in userAccounts){
                        if(acc is SavingsAccount){
                            while(true){
                                Console.WriteLine("\nEnter the Amount you want to Withdraw.");
                                WithdrawAmount=Validations.IsDecimalNumber();
                                if(WithdrawAmount>=0){
                                    acc.Withdraw(WithdrawAmount);
                                    dataManagement.SaveAllData(allAccounts, userAccounts, currentAccounts, savingsAccounts);
                                    status1 = false;
                                    break;
                                }
                            }
                        }
                    }
                    if(!(status1)){
                        Console.WriteLine("\nThis Account has not created a Savings Account.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
        }

        public void Transfer(CurrentAccount current,SavingsAccount savings,
                             List<BankAccount> userAccounts,DataManagement dataManagement,
                             List<BankAccount> allAccounts,List<BankAccount> currentAccounts,
                             List<BankAccount> savingsAccounts){
            decimal transferAmount;
            Console.WriteLine("\n---TRANSFER MENU---");
            Console.WriteLine("0) Back to Previous Menu.");
            Console.WriteLine("1) Transfer to Another Account.");
            Console.WriteLine("2) Transfer from Current Account to Savings Account.");
            Console.WriteLine("3) Transfer from Savings Account to Current Account.");
            int choice = Validations.IsIntNumber();

            switch (choice){
                case 0:
                    return;
                case 1:
                    while(true){    
                        Console.WriteLine("\nSelect the Amount that you want to Transfer.");
                        transferAmount = Validations.IsDecimalNumber();

                        if(transferAmount>=0){
                            break;
                        }
                        Console.WriteLine("\nInvalid Amount.");
                    }

                    string afm;
                    int count=0;
                    while(true){
                        Console.WriteLine($"\nTry Number:{count+1}/3.");
                        Console.WriteLine("Select the Afm of the Account that you want to Transfer the Amount.");
                        afm = Console.ReadLine();
                        if(afm.Length==9) break;
                        Console.WriteLine("\nInvalid Afm.");
                        count++;
                        if(count>3){
                            return;
                        }
                    }
                    
                    BankAccount receiver = allAccounts.FirstOrDefault(a => a.Afm == afm);

                    if(receiver == null){
                        Console.WriteLine("\nAccount not found.");
                        return;
                    }

                    current.Balance-=transferAmount;
                    receiver.Balance+=transferAmount;

                    current.trans.Add(new Transaction("Transfer Out", 
                                                            transferAmount, 
                                                            $"To {receiver.OwnerFirstName} {receiver.OwnerLastName}"));

                    receiver.trans.Add(new Transaction("Gain Transfer", 
                                                            transferAmount, 
                                                             $"From {current.OwnerFirstName} {current.OwnerLastName}"));

                    dataManagement.SaveAllData(userAccounts,allAccounts,
                                               currentAccounts,savingsAccounts);
                    break;
                case 2:
                    while(true){    
                        Console.WriteLine("\nSelect the Amount that you want to Transfer.");
                        transferAmount = Validations.IsDecimalNumber();

                        if(transferAmount>=0){
                            break;
                        }
                        Console.WriteLine("\nInvalid Amount.");
                    }

                    current.Balance-=transferAmount;
                    savings.Balance+=transferAmount;

                    current.trans.Add(new Transaction("Transfer In", 
                                                            transferAmount, 
                                                            "From Current Account To Savings"));

                    dataManagement.SaveAllData(userAccounts,allAccounts,
                                               currentAccounts,savingsAccounts);

                    break;
                case 3:
                    while(true){    
                        Console.WriteLine("\nSelect the Amount that you want to Transfer.");
                        transferAmount = Validations.IsDecimalNumber();

                        if(transferAmount>=0){
                            break;
                        }
                        Console.WriteLine("\nInvalid Amount.");
                    }

                    savings.Balance-=transferAmount;
                    current.Balance+=transferAmount;

                    current.trans.Add(new Transaction("Transfer In", 
                                                            transferAmount, 
                                                            "From Savings Account To Transfer"));

                    dataManagement.SaveAllData(userAccounts,allAccounts,
                                               currentAccounts,savingsAccounts);

                    break;
                default:
                    Console.WriteLine("\nInvalid Choice.");
                    break;
            }
        }
    }
}