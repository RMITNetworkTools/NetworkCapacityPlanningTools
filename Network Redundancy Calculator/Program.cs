
using System;

namespace NetworkCapacityPlanningTool
{
    public class RedundancyCalculator
    {
        // ... (rest of the class)

        public static void Main(string[] args)
        {
            RedundancyCalculator calculator = new RedundancyCalculator();

            Console.WriteLine("Enter desired uptime percentage (e.g., 99.99):");
            double desiredUptimePercentage = double.Parse(Console.ReadLine());
            Console.WriteLine($"You entered: {desiredUptimePercentage}%");

            Console.WriteLine("Enter average downtime of a single link or device in hours:");
            double averageDowntime = double.Parse(Console.ReadLine());
            Console.WriteLine($"You entered: {averageDowntime} hours");


            int requiredRedundancy = calculator.CalculateRedundancy(desiredUptimePercentage, averageDowntime);

            Console.WriteLine($"Required redundancy: {requiredRedundancy}");
            Console.ReadLine();

        }
        /// <summary>
        /// Calculates the required redundancy to achieve a specified uptime.
        /// </summary>
        /// <param name="desiredUptimePercentage">Desired uptime percentage (e.g., 99.99 for 99.99% uptime).</param>
        /// <param name="averageDowntime">Average downtime of a single link or device in hours.</param>
        /// <returns>Number of redundant links or devices required.</returns>
        public int CalculateRedundancy(double desiredUptimePercentage, double averageDowntime)
        {
            if (desiredUptimePercentage < 0 || desiredUptimePercentage > 100)
            {
                throw new ArgumentException("Desired uptime percentage should be between 0 and 100.");
            }

            if (averageDowntime < 0)
            {
                throw new ArgumentException("Average downtime cannot be negative.");
            }

            double uptimeRequirementHours = (100 - desiredUptimePercentage) / 100 * 8760; // 8760 hours in a year
            return (int)Math.Ceiling(averageDowntime / uptimeRequirementHours);
        }
       
        
    }



}

