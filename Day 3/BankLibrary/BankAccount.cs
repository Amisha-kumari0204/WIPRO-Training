using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_bankAccountManagementSysytemIN_CSharp
{
    public class BankAccount
    //core banking assembly
    {
        // Account PIN must be hidden
        private string accountPIN;
        // Account number accessible everywhere
        public string accountNumber;
        //accessible within derived classes or same assembly
        protected internal decimal interestRate;
        // accessible within same assembly
        internal string bankBranchCode;
        // accessible within derived classes
        protected decimal balance;

        // Constructor : To initialize bank account memebers with default values
        public BankAccount(string accNumber, string pin, decimal interest, string branchCode)
        {
            accountNumber = accNumber;
            accountPIN = pin;
            interestRate = interest;
            bankBranchCode = branchCode;
        }

        //deposit money method

        public void Deposit(decimal amount)
        {
            balance += amount;// add amount to balance
        }

        // Method to withdraw money (requires correct PIN)
        public bool Withdraw(decimal amount, string pin)
        {
            if (pin != accountPIN) return false;
            if (amount <= 0 || amount > balance) return false;
            balance -= amount;
            return true;
        }

        // Method to get balance (requires correct PIN)
        public decimal GetBalance(string pin)
        {
            if (pin != accountPIN) throw new UnauthorizedAccessException("Invalid PIN");
            return balance;
        }

        // Display account details (can be overridden by derived classes)
        public virtual void DisplayAccountDetails()
        {
            Console.WriteLine($"Account: {accountNumber}, Balance: {balance}, Branch: {bankBranchCode}");
        }

     public void ApplyCorporateInterest()// Method to apply interest for corporate account
        {
            balance += balance * interestRate / 100;
        }

        //pincode is private in base class hence not accessible here
        //branchCode is internal in base class hence not accessible here



    }
}