using System.Collections.Generic;

namespace ASS_WEEK_4
{

    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                Balance += amount;
                return true;
            }
        }

        public virtual bool Withdraw(double amount)
        {
            if (amount <= 0 || amount > Balance) return false;
            Balance -= amount;
            return true;

        }
    }
    class SavingsAcc : Account
    {
        public SavingsAcc(string name = "Unnamed Account", double balance = 0, double intersetRate = 0) : base(name, balance)
        {
            InterestRate = intersetRate;
        }
        public double InterestRate { get; set; }

        public override bool Deposit(double amount)
        {
            if (amount < 0) return false;
            double total = amount + amount * InterestRate/100;

            return base.Deposit(total);
        }
    }

    class CheckingAcc : Account
    {
        public CheckingAcc(string name = "Unnamed Account", double balance = 0) : base(name, balance)
        {
        }
        private const double WithdrawFee = 1.5;


        public override bool Withdraw(double amount)
        {
            double total = amount + WithdrawFee;
            if (total > Balance) return false;

            return base.Withdraw(total);
        }
    }

    class TrustAcc : Account
    {
        public TrustAcc(string name = "Unnamed Account", double balance = 0, double interestRate = 0) : base(name, balance)
        {
            InterestRate = interestRate;
        }
        public double InterestRate { get; set; }

        int WithdrawCounts = 0;
        private const double MaxWithdrawPercent = 0.2;
        private const double DepositBonus = 50.0;
        private const int MinimumAmount4Bonus = 5000;
        private const int MAxWIthdrawTranaction = 3;


        public override bool Deposit(double amount)
        {
            if (amount < 0) return false;
            double total = amount;
            if (total >= MinimumAmount4Bonus) total += DepositBonus;
            return base.Deposit(total);
        }

        public override bool Withdraw(double amount)
        {
            if (amount > Balance * MaxWithdrawPercent || WithdrawCounts >= MAxWIthdrawTranaction) return false;
            if (base.Withdraw(amount))
            {
                WithdrawCounts++;
                return true;
            }
            return false;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Account s1 = new SavingsAcc("Ali", 1000, 5.0);
            Account s2 = new SavingsAcc("Nada", 3000, 5.0);

            Account c1 = new CheckingAcc("Mona", 2000);
            Account c2 = new CheckingAcc("Osama", 1500);

            Account t1 = new TrustAcc("Ahmed", 10000, 4.0);
            Account t2 = new TrustAcc("Mohamed", 25000, 4.0);

            List<Account> accounts = new List<Account>() { s1, s2, c1, c2, t1, t2 };

            AccountUtil.Deposit(accounts, 5000);
            AccountUtil.Withdraw(accounts, 1000);
            AccountUtil.PrintDetails(accounts);
        }
    }

    public static class AccountUtil
    {
        // Utility helper functions for Account class
        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc.Name}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc.Name}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc.Name}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc.Name}");
            }
        }

        public static void PrintDetails(List<Account> accounts)
        {
            Console.WriteLine("\n=== Account Info ==============================");
            foreach (var acc in accounts)
            {
                Console.WriteLine($"{acc.Name} , has balance {acc.Balance}");
            }
        }
    }
}
