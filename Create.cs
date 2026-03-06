using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBankApp{
    public class Create{
        public void CreateAccount(List<BankAccount> currentAccounts, 
                                  List<BankAccount> savingsAccounts,
                                  List<BankAccount> allAccounts){
            string fName,lName,afm,password;
            int count=0;

            while(true){
                Console.WriteLine($"\nTry Number:{count+1}/3.");
                Console.WriteLine("Enter your Afm.");
                afm=Console.ReadLine();
                if(allAccounts.Any(a => a.Afm == afm)){
                    Console.WriteLine("\nThere is Already an Account with this AFM.");
                    count++;
                    if(count==3){
                        return;
                    }
                    continue;
                }
                if(afm.Length==9){
                    break;
                }
                count++;
                Console.WriteLine("\nInvalid AFM.");
                if(count==3){
                    return;
                }
            }

            Console.WriteLine("\nEnter your Password.");
            password=Console.ReadLine();

            Console.WriteLine("\nEnter your First Name.");
            fName = Validations.IsString();

            Console.WriteLine("\nEnter your Last Name.");
            lName = Validations.IsString();

            BankAccount currentAccount = new CurrentAccount(afm, password, fName, lName, 0);
            currentAccounts.Add(currentAccount);
            allAccounts.Add(currentAccount);

            Console.WriteLine("\nCurrent Account Created!");
            return;
        }

        public void CreateSavingsAccount(List<BankAccount> currentAccounts, 
                                         List<BankAccount> savingsAccounts,
                                         List<BankAccount> allAccounts,
                                         List<BankAccount> userAccounts,
                                         BankAccount logged){
            string fName,lName,afm,password;

            Console.WriteLine("\nEnter your Afm.");
            afm=Console.ReadLine();
            if(savingsAccounts.Any(a => a.Afm == afm)){
                Console.WriteLine("\nThere is Already an Account with this AFM");
                return;
            }
            if(afm.Length!=9){
                Console.WriteLine("\nInvalid AFM.");
            }  
              
            Console.WriteLine("\nEnter your Password.");
            password=Console.ReadLine();

            if(afm==logged.Afm && password==logged.Password){
                afm = logged.Afm;
                password = logged.Password;
                fName = logged.OwnerFirstName;
                lName = logged.OwnerLastName;

                BankAccount savingsAccount = new SavingsAccount(afm, password, fName, lName, 0);
                savingsAccounts.Add(savingsAccount);
                allAccounts.Add(savingsAccount);
                userAccounts.Add(savingsAccount);

                Console.WriteLine("\nSavings Account Created!");
            }
            else{
                Console.WriteLine("\nInvalid Afm or Password.");
            }
            return;
        }
    }
}