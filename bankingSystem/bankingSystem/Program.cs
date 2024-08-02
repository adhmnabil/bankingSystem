using System;
using System.Collections.Generic;

class Program
{
    public class Account
    {
        public string name;
        public double balance;

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.name = name;
            this.balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                balance += amount;
                return true;
            }
        }

        public virtual bool Withdraw(double amount)
        {
            if (balance - amount >= 0)
            {
                balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetBalance()
        {
            return balance;
        }

        public override string ToString()
        {
            return $"[Account: {name}: {balance}]";
        }

        public static double operator +(Account a1, Account a2)
        {
            return a1.balance + a2.balance;
        }
    }

    public class SavingsAccount : Account
    {
        public double interestRate;

        public SavingsAccount(string name = "Unnamed Savings Account", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance)
        {
            this.interestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            if (base.Deposit(amount))
            {
                balance += balance * interestRate / 100;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"[Savings Account: {name}: {balance}, Interest Rate: {interestRate}%]";
        }
    }

    public class CheckingAccount : Account
    {
        public double WithdrawalFee = 1.50;

        public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0)
            : base(name, balance)
        {
        }

        public override bool Withdraw(double amount)
        {
            amount += WithdrawalFee;
            return base.Withdraw(amount);
        }

        public override string ToString()
        {
            return $"[Checking Account: {name}: {balance}]";
        }
    }

    public class TrustAccount : Account
    {
        public double interestRate;
        public int withdrawalCount = 0;
        public const int MaxWithdrawals = 3;
        public const double MaxWithdrawalPercentage = 0.2;

        public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance)
        {
            this.interestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                amount += 50;
            }
            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            if (withdrawalCount >= MaxWithdrawals || amount > balance * MaxWithdrawalPercentage)
            {
                return false;
            }

            if (base.Withdraw(amount))
            {
                withdrawalCount++;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"[Trust Account: {name}: {balance}, Interest Rate: {interestRate}%]";
        }
    }

    public static class AccountUtil
    {
        public static void Display(List<Account> accounts)
        {
            if(accounts.Count == 0)
            {
                Console.WriteLine("no accounts has been made");
            }
            else
            {
                Console.WriteLine("\n Accounts ");
                foreach (var acc in accounts)
                {
                    Console.WriteLine(acc);
                }
            }
           
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n Depositing to Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n Withdrawing from Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }

    static void Main()
    {
        var accounts = new List<Account>();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Display Accounts");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount(accounts);
                    break;
                case "2":
                    MakeTransaction(accounts, "deposit");
                    break;
                case "3":
                    MakeTransaction(accounts, "withdraw");
                    break;
                case "4":
                    AccountUtil.Display(accounts);
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void CreateAccount(List<Account> accounts)
    {
        Console.Write("Enter account type (standard, savings, checking, trust): ");
        string type = Console.ReadLine().ToLower();

        Console.Write("Enter account name: ");
        string name = Console.ReadLine();

        Console.Write("Enter initial balance: ");
        double balance = double.Parse(Console.ReadLine());

        Account account = null;

        switch (type)
        {
            case "standard":
                account = new Account(name, balance);
                break;
            case "savings":
                Console.Write("Enter interest rate: ");
                double interestRate = double.Parse(Console.ReadLine());
                account = new SavingsAccount(name, balance, interestRate);
                break;
            case "checking":
                account = new CheckingAccount(name, balance);
                break;
            case "trust":
                Console.Write("Enter interest rate: ");
                double trustInterestRate = double.Parse(Console.ReadLine());
                account = new TrustAccount(name, balance, trustInterestRate);
                break;
            default:
                Console.WriteLine("Invalid account type.");
                return;
        }

        accounts.Add(account);
        Console.WriteLine("Account created successfully.");
    }

    static void MakeTransaction(List<Account> accounts, string transactionType)
    {
        Console.Write("Enter account name: ");
        string name = Console.ReadLine();

        var account = accounts.Find(acc => acc.name == name);

        if (account == null)
        {
            Console.WriteLine("Account not found.");
            return;
        }

        Console.Write($"Enter amount to {transactionType}: ");
        double amount = double.Parse(Console.ReadLine());

        bool success = false;

        if (transactionType == "deposit")
        {
            success = account.Deposit(amount);
        }
        else if (transactionType == "withdraw")
        {
            success = account.Withdraw(amount);
        }

        if (success)
        {
            Console.WriteLine($"{transactionType} of {amount} was successful.");
        }
        else
        {
            Console.WriteLine($"{transactionType} of {amount} failed.");
        }
    }
}
