﻿namespace Monopoly.Logics.PlayerFlyweight
{
    public class Wallet
    {
        #region Properties

        public int Balance { get; }

        #endregion

        #region Constructor

        public Wallet(int balance)
        {
            Balance = balance;
        }

        #endregion

        #region Overrides

        public static Wallet operator +(Wallet wallet, int value)
        {
            return new Wallet(wallet.Balance + value);
        }
        
        public static Wallet operator -(Wallet wallet, int value)
        {
            return new Wallet(wallet.Balance - value);
        }
        
        public override string ToString()
        {
            return "Balance: " + Balance;
        }
        
        // TODO: Operator overloads?

        #endregion
    }
}