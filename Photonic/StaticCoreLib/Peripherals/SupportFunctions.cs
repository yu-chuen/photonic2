using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class SupportFunctions
    {
        //constructor
        public SupportFunctions()
        {
        }
        
        /// <summary>
        /// Get the value with the number of decimal places without rounding off.
        /// </summary>
        /// <param name="value">Value to be used to process.</param>
        /// <param name="precision">Number of decimal places.</param>
        /// <returns>value = 10.867, precision = 2, ==> returns 10.86</returns>
        public double Get_DecimalPlaces(double value, int precision)
        {
            double db = 0;

            db = Math.Truncate((Math.Pow(10, precision)) * value) / (Math.Pow(10, precision));

            return db;
        }

        /// <summary>
        /// Calculate the percentage of the value.
        /// </summary>
        /// <param name="CompareValue">Value to be used to compare.</param>
        /// <param name="TotalValue">Total value of the target.</param>
        /// <param name="GetRemainderPercentage">Get the remaining percentage after calculation.</param>
        /// <returns>if (GetRemainderPercentage == false) percentage = 70, if (GetRemainderPercentage == true) percentage = 30</returns>
        public double Get_Percentage_From_Value(double CompareValue, double TotalValue, bool GetRemainderPercentage)
        {
            double percentage = 0;

            percentage = CompareValue / TotalValue * 100;

            if (GetRemainderPercentage == true)
            {
                percentage = 100 - percentage;
            }

            return percentage;
        }

        /// <summary>
        /// Calculate the value of the percentage.
        /// </summary>
        /// <param name="TotalValue">Total value of the target.</param>
        /// <param name="percentage">Percentage required to extract the value from TotalValue.</param>
        /// <returns>Actual value for the percentage from TotalValue.</returns>
        public double Get_Value_From_Percentage(double TotalValue, double percentage)
        {
            double value = 0;

            value = TotalValue * percentage / 100;

            return value;
        }
    }
}