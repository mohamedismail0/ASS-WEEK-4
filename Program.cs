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
          
            amount += amount * (IntersetRate/100);
            return base.Deposit(amount);
        }

    }

    class CheckingAcc : Account
    {
        public CheckingAcc(string name = "Unnamed Account", double balance = 0 , double WithdrawFee =1.5) : base(name, balance)
        {
        }
        double WithdrawFee = 1.5;


        public override bool Withdraw(double amount)
        {
            if (Balance - amount < 0) return false;            
        
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
        const double MaxWithdrawPercent = 0.2;
        const double DepositBonus = 50.0;
        const int MinimumAmount4Bonus = 5000;


        public override bool Deposit(double amount)
        {
            if (amount  < 0) return false;
            Balance += amount;
            if (amount >= MinimumAmount4Bonus) Balance += DepositBonus;
            return base.Deposit(amount);

        }

        public override bool Withdraw(double amount)
        {
            if (Balance - amount  > 0 || amount > amount * MaxWithdrawPercent|| WithdrawCounts > 3) return false;
            //if (amount > amount* MaxWithdrawPercent) return false;
            //if (WithdrawCounts > 3) return false;
            Balance -= amount;
            if (base.Withdraw(amount))
            {
                WithdrawCounts++;
                return base.Withdraw(amount);
            }
            return false;

        }

    }

        internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
