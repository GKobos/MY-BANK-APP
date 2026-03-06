using System;

namespace MyBankApp{
    public class CurrentAccount : BankAccount{
        decimal OverdraftLimit=1000;

        public CurrentAccount() { }

        public CurrentAccount(
            string Afm,
            string Password,
            string OwnerFirstName, 
            string OwnerLastName, 
            decimal InitialBalance
        ): base(Afm,Password,OwnerFirstName,OwnerLastName,InitialBalance){ }

        public override void Withdraw(decimal WithdrawAmount){
            if(WithdrawAmount<=Balance+OverdraftLimit){
                Balance-=WithdrawAmount;
                trans.Add(new Transaction("Withdraw", WithdrawAmount, "ATM Withdraw"));
                Console.WriteLine($"\nYou Withdrawed {WithdrawAmount} Euros.");
                Console.WriteLine($"\nThe New Balance is {Balance}.");
            }
            else Console.WriteLine("\nYour Funds are Not Enough.");
        }

        public override string AccountType => "Current Account";
    }
}