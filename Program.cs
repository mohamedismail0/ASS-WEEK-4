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
            IntersetRate = intersetRate;
        }
        public double  IntersetRate { get; set; }

        public override bool Deposit(double amount )
        {
            if (amount < 0)
                return false;
          
            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            if ((amount + amount * IntersetRate/100) > Balance ) return false;

            return base.Withdraw(amount);
        }

    }

    class CheckingAcc : Account
    {
        public CheckingAcc(string name = "Unnamed Account", double balance = 0 , double WithdrawFee =1.5) : base(name, balance)
        {
        }
         private const double WithdrawFee = 1.5;


        public override bool Withdraw(double amount)
        {
            if (amount+WithdrawFee < Balance) return false;            
        
            amount -= amount+ WithdrawFee;
            return base.Withdraw(amount);
        }
    }

    class TrustAcc : Account
    {
        public TrustAcc(string name = "Unnamed Account", double balance = 0, double intersetRate = 0) : base(name, balance)
        {
            IntersetRate = intersetRate;
        }
        double IntersetRate { get; set; }

        int WithdrawCounts = 0;
        private const double MaxWithdrawPercent = 0.2;
        private const double DepositBonus = 50.0;
        private const int MinimumAmount4Bonus = 5000;


        public override bool Deposit(double amount)
        {
            if (amount  < 0) return false;
            amount += amount * (IntersetRate / 100);
            if (amount >= MinimumAmount4Bonus) Balance += DepositBonus;
            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            if (amount > Balance * MaxWithdrawPercent || WithdrawCounts >= 3) return false;
            //if (amount > amount* MaxWithdrawPercent) return false;
            //if (WithdrawCounts > 3) return false;
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

            var s1 = new SavingsAcc("Ali", 1000, 5.0);
            var s2 = new SavingsAcc("Nada", 3000, 5.0);

            var c1 = new CheckingAcc("Mona", 2000);
            var c2 = new CheckingAcc("Osama", 1500);

            var t1 = new TrustAcc("Ahmed", 10000, 4.0);
            var t2 = new TrustAcc("Mohamed", 25000, 4.0);

            List<Account> accounts = new List<Account>() {s1,s2,c1,c2,t1,t2};

            List<SavingsAcc> savingList = new List<SavingsAcc>() { s1 ,s2 };

            List<TrustAcc> trustList = new List<TrustAcc>() { t1,t2 };

            List<CheckingAcc> checkingList = new List<CheckingAcc>() { c1,c2 };





            Console.WriteLine("\n--- Deposit 6000 to all accounts ---");
            foreach (var acc in accounts)
            {
                Console.WriteLine($"\nBefore deposit: {acc.Name} has balance {acc.Balance}");
                acc.Deposit(6000);
                Console.WriteLine($"After deposit: {acc.Name} has balance {acc.Balance}");
            }

            Console.WriteLine("\n--- Withdraw 500 from all accounts ---");
            foreach (var acc in accounts)
            {
                Console.WriteLine($"\nBefore withdraw: {acc.Name} has balance {acc.Balance}");
                acc.Withdraw(500);
                Console.WriteLine($"After withdraw: {acc.Name} has balance {acc.Balance}");
            }

            //=================================================================================

            Console.WriteLine("\n--- Deposit 6000 to all saving accounts ---");
            foreach (var acc in savingList)
            {
                Console.WriteLine($"\nBefore deposit: {acc.Name} has balance {acc.Balance}");
                acc.Deposit(6000);
                Console.WriteLine($"After deposit: {acc.Name} has balance {acc.Balance}");
            }

            //=================================================================================

            Console.WriteLine("\n--- Withdraw 500 from all saving accounts ---");
            foreach (var acc in savingList)
            {
                Console.WriteLine($"\nBefore withdraw: {acc.Name} has balance {acc.Balance}");
                acc.Withdraw(500);
                Console.WriteLine($"After withdraw: {acc.Name} has balance {acc.Balance}");
            }

            //=================================================================================

            Console.WriteLine("\n--- Deposit 6000 to all trust accounts ---");
            foreach (var acc in trustList)
            {
                Console.WriteLine($"\nBefore deposit: {acc.Name} has balance {acc.Balance}");
                acc.Deposit(6000);
                Console.WriteLine($"After deposit: {acc.Name} has balance {acc.Balance}");
            }

            Console.WriteLine("\n--- Withdraw 500 from all trust accounts ---");
            foreach (var acc in trustList)
            {
                Console.WriteLine($"\nBefore withdraw: {acc.Name} has balance {acc.Balance}");
                acc.Withdraw(500);
                Console.WriteLine($"After withdraw: {acc.Name} has balance {acc.Balance}");
            }

            //=================================================================================

            Console.WriteLine("\n--- Deposit 6000 to all checking accounts ---");
            foreach (var acc in checkingList)
            {
                Console.WriteLine($"\nBefore deposit: {acc.Name} has balance {acc.Balance}");
                acc.Deposit(6000);
                Console.WriteLine($"After deposit: {acc.Name} has balance {acc.Balance}");
            }

            Console.WriteLine("\n--- Withdraw 500 from all checking accounts ---");
            foreach (var acc in checkingList)
            {
                Console.WriteLine($"\nBefore withdraw: {acc.Name} has balance {acc.Balance}");
                acc.Withdraw(500);
                Console.WriteLine($"After withdraw: {acc.Name} has balance {acc.Balance}");
            }

            Console.WriteLine("\nDone.");
        }

    }
}
