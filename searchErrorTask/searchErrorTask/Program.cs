using System;
using System.Collections.Generic;

namespace searchErrorTask
{
    internal class Program
    {
        class CheckVols
        {
            public string Mine { get; set; }

            public void Check(string word)
            {
                bool hasVowels = false;

                foreach (var item in word)
                {
                    if (item == 'e' || item == 'i' || item == 'o' || item == 'u' || item == 'a')
                    {
                        hasVowels = true;
                        break;
                    }
                }

                if (!hasVowels)
                {
                    Console.WriteLine("Invalid word");
                }
                else
                {
                    Console.WriteLine("Valid word");
                }
            }
        }

        class NumberChecker
        {
            private List<int> numbers = new List<int>();

            public bool CheckAndAddNumber(int number)
            {
                if (numbers.Contains(number))
                {
                    Console.WriteLine("Number already exists.");
                    return false;
                }
                else
                {
                    numbers.Add(number);
                    Console.WriteLine("Number added.");
                    return true;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            CheckVols vowelChecker = new CheckVols();
            string input = Console.ReadLine();
            vowelChecker.Check(input);
            NumberChecker numberChecker = new NumberChecker();
            while (true)
            {
                Console.WriteLine("Enter a number :");
                string numberInput = Console.ReadLine();
                if (numberInput.ToLower() == "exit")
                {
                    break;
                }
                int number = int.Parse(numberInput);
                numberChecker.CheckAndAddNumber(number);
            }
        }
    }
}
