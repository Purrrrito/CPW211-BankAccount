﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    /// <summary>
    /// Represents a single customers bank account
    /// </summary>
    public class Account
    {
        private string owner;

        /// <summary>
        /// Creates an account with a specific owner
        /// </summary>
        /// <param name="accOwner">The customer full name that owns the account</param>
        public Account(string accOwner)
        {
            Owner = accOwner;
        }

        /// <summary>
        /// The account holders full name (first and last)
        /// </summary>
        public string Owner 
        { 
            get { return owner;  }
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException($"{nameof(Owner)} cannot be null");
                }

                if (value.Trim() == string.Empty)
                {
                    throw new ArgumentException($"{nameof(Owner)} must have some text");
                }

                if (IsOwnerNameValid(value))
                {
                    owner = value;
                }
                else
                {
                    throw new ArgumentException($"{nameof(Owner)} cane be up to 20 characters, A-Z/spaces only");
                }
            } 
        }

        /// <summary>
        /// Checks if Owner name is less than or equal to 20 characters, A-Z,
        /// and whitespace characters are allowed
        /// </summary>
        /// <returns></returns>
        private bool IsOwnerNameValid(string ownerName)
        {
            char[] validCharacters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y' };

            ownerName = ownerName.ToLower(); // Only need to compare to one casing

            const int MaxLengthOwnerName = 20;

            if (ownerName.Length > MaxLengthOwnerName)
            {
                return false;
            }

            foreach (char letter in ownerName)
            {
                if (letter != ' ' && !validCharacters.Contains(letter))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The amount of money currently in the account
        /// </summary>
        public double Balance { get; private set; }

        /// <summary>
        /// Add a specified amount of money to the account.
        /// Returns the new balance
        /// </summary>
        /// <param name="amt">The positive amount to deposit to the balance</param>
        /// <returns>The new balance after the deposit</returns>
        public double Deposit(double amt)
        {
            if (amt <= 0)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(amt)} must be more than 0");
            }

            Balance += amt;
            return Balance;
        }

        /// <summary>
        /// Withdraws an amount of money from the balance
        /// </summary>
        /// <param name="amt">The positive amount of money to withdraw from the balance</param>
        /// <returns>Returns updated balance after withdrawal</returns>
        public double Withdraw(double amt)
        {
            if (amt > Balance)
            {
                throw new ArgumentException($"{nameof(amt)} cannot be greater than balance");
            }

            if (amt <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(amt)} must be greater than 0");
            }
            Balance -= amt;
            return Balance;
        }
    }
}
