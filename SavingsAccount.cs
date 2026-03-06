using System;

namespace MyBankApp{
    public class SavingsAccount : BankAccount{

        public SavingsAccount() { }

        public SavingsAccount(
            string Afm,
            string Password,
            string OwnerFirstName, 
            string OwnerLastName, 
            decimal InitialBalance
        ): base(Afm,Password,OwnerFirstName,OwnerLastName,InitialBalance){ }

        public override void Withdraw(decimal WithdrawAmount){
            if(WithdrawAmount<=Balance){
                Balance-=WithdrawAmount;
                trans.Add(new Transaction("Withdraw", WithdrawAmount, "ATM Withdraw"));
                Console.WriteLine($"\nYou Withdrawed {WithdrawAmount} Euros.");
                Console.WriteLine($"\nThe New Balance is {Balance}.");
            }
            else Console.WriteLine("\nYour Funds are Not Enough.");
        }

        public override string AccountType => "Savings Account";
    }
}