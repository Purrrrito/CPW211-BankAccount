﻿using Microsoft.VisualStudio.TestTools.UnitTesting; // I had to add this using manually. Not sure why
using BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Tests
{
    [TestClass()]
    public class AccountTests
    {
        private Account acc;

        [TestInitialize]
        public void CreateDefaultAccount()
        {
            acc = new Account("J Doe");
        }

        [TestMethod()]
        [DataRow(100)]
        [DataRow(.01)]
        [DataRow(1.99)]
        [DataRow(9_999.99)]
        [TestCategory("Deposit")]
        public void Deposit_APositiveAmount_AddToBalance(double depositAmount)
        {
            acc.Deposit(depositAmount);

            Assert.AreEqual(depositAmount, acc.Balance);
        }

        [TestMethod]
        [TestCategory("Deposit")]
        public void Deposit_APositiveAmount_ReturnsUpdatedBalance()
        {
            // AAA - Arrange Act Assert
            //Arrange
            double depositAmount = 100;
            double expectedReturn = 100;

            //Act
            double returnValue = acc.Deposit(depositAmount);

            // Assert
            Assert.AreEqual(expectedReturn, returnValue);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [TestCategory("Deposit")]
        public void Deposit_ZeroOrLess_ThrowsArgumentException(double invalidDepositAmount)
        {
            // Arrange
            // Nothing to add

            // Assert => Act
            Assert.ThrowsException<ArgumentOutOfRangeException>
                (() => acc.Deposit(invalidDepositAmount));

        }


        [TestMethod]
        [TestCategory("Withdraw")]
        public void Withdraw_PositiveAmount_DecreasesBalance()
        {
            // Arrange
            double initialDeposit = 100;
            double withdrawalAmount = 50;
            double expectedBalance = initialDeposit - withdrawalAmount;

            // Act
            acc.Deposit(initialDeposit);
            acc.Withdraw(withdrawalAmount);

            double actualBalance = acc.Balance;

            // Assert
            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [TestMethod]
        [DataRow(100, 50)]
        [DataRow(100, .99)]
        [TestCategory("Withdraw")]
        public void Withdraw_PositiveAmount_ReturnsUpdatedBalance(double initialDeposit, double withdrawAmount)
        {
            // Arrange
            double expectedBalance = initialDeposit - withdrawAmount;
            acc.Deposit(initialDeposit);

            // Act
            double returnedBalance = acc.Withdraw(withdrawAmount);

            // Assert
            Assert.AreEqual(expectedBalance, returnedBalance);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-.01)]
        [DataRow(-1000)]
        [TestCategory("Withdraw")]
        public void Withdraw_ZeroOrLess_ThrowsArugmentOutOfRangeException(double withdrawAmount)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => acc.Withdraw(withdrawAmount));
        }

        [TestMethod]
        [TestCategory("Withdraw")]
        public void Withdraw_MoreThanAvailableBalance_ThrowsArgumentException()
        {
            double withdrawAmount = 1000;

            Assert.ThrowsException<ArgumentException>(() => acc.Withdraw(withdrawAmount));
        }

        [TestMethod]
        public void Owner_SetAsNull_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => acc.Owner = null);
        }

        [TestMethod]
        public void Owner_SetAsWhitespaceOrEmptyString_ThrowsArugmentException()
        {
            Assert.ThrowsException<ArgumentException>(() => acc.Owner = String.Empty);
            Assert.ThrowsException<ArgumentException>(() => acc.Owner = "   "); 
        }

        [TestMethod]
        [DataRow("Wyatt")]
        [DataRow("Wyatt Smith")]
        [DataRow("Wyatt Carters Smiths")]
        public void Owner_SetAsUpTo20Characters_SetsSuccessfully(string ownerName)
        {
            acc.Owner = ownerName;
            Assert.AreEqual(ownerName, acc.Owner);
        }

        [TestMethod]
        [DataRow("Wyatt 3rd")]
        [DataRow("Wyatts Carters Smiths")]
        [DataRow("#$%$")]
        public void Owner_InvalidOwnerName_ThrowsArugmentException(string ownerName)
        {
            Assert.ThrowsException<ArgumentException>(() => acc.Owner = ownerName);
        }
    }
}