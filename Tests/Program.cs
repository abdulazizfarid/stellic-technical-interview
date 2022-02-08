using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Tests
{
    /*
     * For example, if input integer array is [2, 6, 5, 4, 3, 9, 11]
     * and given target is 9, output should be [(6,3), (5,4)].

     */
    internal class Program
    {
        public static void Sum(int[] array, int target)
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < array.Length; i++)
            {
                var num2 = target - array[i];
                
                if (dict.ContainsKey(num2))
                    Console.WriteLine("({0},{1})", num2, array[i]);
                else
                {
                    try
                    {
                        dict.Add(array[i], i);
                    }
                    catch
                    {
                        
                    }
                }
                    
                
                
            }
        }

        /*
         * **Input:** [4,8,2,9,1,3]
         * [-2,-5,-1,3,5,2]
         * NegList.Add(currentElement)
         * NegList = [-2,-5]
         * PosList =
         * **Output:** 72
         */
        public static int MaxProduct(int[] array)
        {
            var NegList = new List<int>();
            var PosList = new List<int>();
            for (int i = 0; i < array.Length; i++)
            {
                
                if (array[i]<0 && NegList.Count<2)
                    NegList.Add(array[i]);
                else if (array[i]>=0 && PosList.Count<2)
                    PosList.Add(array[i]);


                if (array[i] < 0 && Math.Abs(array[i]) > Math.Abs(NegList.Max()))
                {
                    var index = NegList.IndexOf(NegList.Max());
                    NegList[index] = array[i];
                }
                
                else if (array[i] >= 0 && array[i] > PosList.Min())
                {
                    var index = PosList.IndexOf(PosList.Min());
                    PosList[index] = array[i];
                }

            }

            var negProduct = 0;
            var posProduct = 0;
            if(NegList.Count==2)
                negProduct = NegList[0] * NegList[1];
            if (PosList.Count==2)
                posProduct = PosList[0] * PosList[1];
            
            if (posProduct > negProduct)
                return posProduct;
            return negProduct;
        }
        
        //List of account transactions, what is the balance at the date of each month

        internal class Transaction
        {
            public DateTime datetime;
            public decimal amount;
            public bool type; //true is credit, false is debit
        }

        public static List<decimal> MonthlyTransactions(List<Transaction> transactions, decimal openingBalance)
        {
            var dict = new Dictionary<DateTime, decimal>();
            
            for (int j = 1; j <= 31; j++) //Initializes a dictionary with each date of month as key and 0 as closing balance
            {
                DateTime currentDate = new DateTime(transactions[0].datetime.Year, transactions[0].datetime.Month, j);
                dict.Add(new DateTime(transactions[0].datetime.Year, transactions[0].datetime.Month, j), 0);
            }
            
            for (int i = 0; i<transactions.Count; i++) //Adds the amount of balance changed to the date of the transaction
            {
                var transaction = transactions[i];
                DateTime currentDate = new DateTime(transaction.datetime.Year, transaction.datetime.Month,
                    transaction.datetime.Day);
                
                if (transaction.type) //credit
                    dict[currentDate] += transaction.amount;
                else //debit
                    dict[currentDate] -= transaction.amount;
            }

            for (int i = 1; i <= 31; i++) //Adds the amount of closing balance of previous day to the current day
            {
                var currentDay = new DateTime(transactions[0].datetime.Year, transactions[0].datetime.Month, i);
                
                if (i == 1) //if first day of month then add opening balance
                    dict[currentDay] += openingBalance;
                else //if any other day then add the amount of closing balance of the previous day to the current day
                {
                    var previousDay = new DateTime(transactions[0].datetime.Year, transactions[0].datetime.Month, (i - 1));
                    dict[currentDay] += dict[previousDay];
                }

            }

            return dict.Select(day => day.Value).ToList(); //Extract and return all the closing balances in the form of a list
        }
        public static void Main(string[] args)
        {
            var input = new List<Transaction>() //Sample input of transactions 
            {
                new Transaction()
                {
                    amount = 1200,
                    datetime = DateTime.Parse("01/01/2020"),
                    type = true //true means credit
                },
                new Transaction()
                {
                    amount = 1800,
                    datetime = DateTime.Parse("01/02/2020"),
                    type = true
                },
                new Transaction()
                {
                    amount = 200,
                    datetime = DateTime.Parse("01/03/2020"),
                    type = false //false means debit
                },
                new Transaction()
                {
                    amount = 500,
                    datetime = DateTime.Parse("01/07/2020"),
                    type = false
                },
                new Transaction()
                {
                    amount = 500,
                    datetime = DateTime.Parse("01/07/2020"),
                    type = false
                },
                new Transaction()
                {
                    amount = 300,
                    datetime = DateTime.Parse("01/07/2020"),
                    type = true
                },
                new Transaction()
                {
                    amount = 2000,
                    datetime = DateTime.Parse("01/30/2020"),
                    type = true
                }
                
            };
            var openingBalance = 200;
            var result = MonthlyTransactions(input, openingBalance);
            var count = 1;
            foreach (var closingBalance in result) //Output the list
            {
                Console.WriteLine("Day {0}:{1}",count++, closingBalance);
                
            }
        }
    }
}

//9 tennis balls, 8 have same weight while 1 has different

/*
 * 4 - 4
 * 2-2
 * 1-1
 * 
 * -----
 *
 * 2 - 2
 * 2 - 2
 * 1 - 1
 * 1 - 1
 * 1 - 1
 * -----
 * 
 * 3(a) - 3(b)  - 3(c)
 *
 * 3(a) - 3(b)
 * 1 - 1
 * 
 
*/