using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MyBankApp
{
    public class BankData
    {
        public List<BankAccount> allAccounts { get; set; }
        public List<BankAccount> userAccounts { get; set; }
        public List<BankAccount> currentAccounts { get; set; }
        public List<BankAccount> savingsAccounts { get; set; }
    }

    public class DataManagement
    {
        public void SaveAllData(List<BankAccount> all,
                                List<BankAccount> user,
                                List<BankAccount> current,
                                List<BankAccount> savings)
        {
            BankData data = new BankData
            {
                allAccounts = all,
                userAccounts = user,
                currentAccounts = current,
                savingsAccounts = savings
            };

            string json = JsonConvert.SerializeObject(
                data,
                Formatting.Indented,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }
            );

            File.WriteAllText("accounts.json", json);
        }

        public BankData LoadAllData()
        {
            if (!File.Exists("accounts.json"))
            {
                return new BankData
                {
                    allAccounts = new List<BankAccount>(),
                    userAccounts = new List<BankAccount>(),
                    currentAccounts = new List<BankAccount>(),
                    savingsAccounts = new List<BankAccount>()
                };
            }

            string json = File.ReadAllText("accounts.json");

            BankData loadedData = JsonConvert.DeserializeObject<BankData>(
                json,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }
            );

            if (loadedData == null)
            {
                return new BankData
                {
                    allAccounts = new List<BankAccount>(),
                    userAccounts = new List<BankAccount>(),
                    currentAccounts = new List<BankAccount>(),
                    savingsAccounts = new List<BankAccount>()
                };
            }

            return loadedData;
        }

        public void DeleteAllData(List<BankAccount> allAccounts,
                                  List<BankAccount> userAccounts,
                                  List<BankAccount> currentAccounts,
                                  List<BankAccount> savingsAccounts)
        {
            while (true)
            {
                Console.WriteLine("\nAre you sure you want to Delete All Data? (y/n)");
                string answer = Console.ReadLine();

                if (answer.ToLower() == "y")
                {
                    allAccounts.Clear();
                    userAccounts.Clear();
                    currentAccounts.Clear();
                    savingsAccounts.Clear();

                    SaveAllData(allAccounts, userAccounts, currentAccounts, savingsAccounts);

                    Console.WriteLine("\nAll Data Deleted Successfully.");
                    return;
                }
                else if (answer.ToLower() == "n")
                {
                    Console.WriteLine("\nDelete Cancelled.");
                    return;
                }
                else
                {
                    Console.WriteLine("\nInvalid Answer.");
                }
            }
        }
    }
}