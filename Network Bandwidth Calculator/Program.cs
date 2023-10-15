using System;

namespace NetworkCapacityPlanningTool
{
    public class BandwidthCalculator
    {
        public static void Main(string[] args)
        {
            BandwidthCalculator calculator = new BandwidthCalculator();

            Console.WriteLine("Welcome to the Bandwidth Calculator!");
            Console.WriteLine();

            Console.WriteLine("Enter the following parameters:");

            Console.Write("Total number of users/devices: ");
            int totalUsers = int.Parse(Console.ReadLine());

            Console.Write("Average data usage per user/device (in Mbps): ");
            double dataUsagePerUser = double.Parse(Console.ReadLine());

            Console.Write("Peak usage factor (e.g., 1.5 for 150% peak usage): ");
            double peakUsageFactor = double.Parse(Console.ReadLine());

            Console.Write("Redundancy factor (e.g., 1.2 for 20% redundancy): ");
            double redundancyFactor = double.Parse(Console.ReadLine());

            double requiredBandwidth = calculator.CalculateRequiredBandwidth(totalUsers, dataUsagePerUser, peakUsageFactor, redundancyFactor);

            Console.WriteLine();
            Console.WriteLine($"Required bandwidth: {requiredBandwidth} Mbps");

            Console.ReadLine();
        }

        public double CalculateRequiredBandwidth(int totalUsers, double dataUsagePerUser, double peakUsageFactor, double redundancyFactor)
        {
            if (totalUsers <= 0 || dataUsagePerUser <= 0 || peakUsageFactor <= 0 || redundancyFactor <= 0)
            {
                throw new ArgumentException("Input parameters must be greater than zero.");
            }

            // Calculate total bandwidth required
            double totalBandwidth = totalUsers * dataUsagePerUser * peakUsageFactor * redundancyFactor;

            return totalBandwidth;
        }
    }
}
