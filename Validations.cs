using System;
using System.Linq;

namespace MyBankApp{
    public class Validations{

        public static int IsIntNumber(){
            while(true){
                int number;
                if(!int.TryParse(Console.ReadLine(), out number)){
                    Console.WriteLine("\nPlease Enter Only Numbers.");
                }
                else{
                    return(number);
                }
            }
        }

        public static decimal IsDecimalNumber(){
            while(true){
                decimal number;
                if(!decimal.TryParse(Console.ReadLine(), out number)){
                    Console.WriteLine("\nPlease Enter Only Numbers.");
                }
                else{ 
                    return(number);
                }
            }
        }

        public static string IsString(){
            while(true){
                string input = Console.ReadLine();
                if(input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrWhiteSpace(input)){
                    return(input);
                }
                else{
                    Console.WriteLine("\nPlease enter only Letters!\n");
                }
            }
        }
    }

}