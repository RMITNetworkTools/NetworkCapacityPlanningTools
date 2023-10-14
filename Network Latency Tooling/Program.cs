using System;
using System.Diagnostics;

public class PingLatencyChecker
{
    public static void Main()
    {
        // Prompt the user to enter a hostname or IP address and store it in the "hostname" variable
        Console.Write("Enter hostname or IP address (example: google.com): ");
        string hostname = Console.ReadLine(); // Get the desired hostname or IP address from the user

        // Prompt the user to enter the number of packets to send and store it in the "packetCount" variable
        Console.Write("Enter the number of packets to send: ");
        
        if (int.TryParse(Console.ReadLine(), out int packetCount))
        {
            // If the user input is a valid integer, display a message indicating the number of packets being pinged
            Console.WriteLine($"Currently pinging {packetCount} packets...");

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "ping";
                    process.StartInfo.Arguments = $"/n {packetCount} {hostname}";
                    // Configure a Process object to run the "ping" command with the specified packet count and hostname
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();    // Start the ping process
                    string output = process.StandardOutput.ReadToEnd(); // Read the output of the ping process
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        // If the ping process exits with code 0 (no errors),
                        double averageLatency = ParseAverageLatency(output); // Calculate and store the average latency from the ping output
                        Console.WriteLine($"Average Latency to {hostname}: {averageLatency} ms"); // Display the calculated average latency
                    }
                    else
                    {
                        // Display a message indicating that the ping failed
                        Console.WriteLine($"Ping to {hostname} failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle and display any exceptions that occur during the ping process
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            // Display a message if the user enters an invalid packet count
            Console.WriteLine("Invalid packet count entered. Please enter a valid integer.");
        }
    }

    private static double ParseAverageLatency(string pingOutput)
    {
        int startIndex = pingOutput.LastIndexOf("Average = ");
        // Find the last occurrence of "Average = " in the ping output
        int endIndex = pingOutput.IndexOf("ms", startIndex);
        // Find the first occurrence of "ms" after the "Average = " string

        if (startIndex >= 0 && endIndex > startIndex)
        {
            // If "Average = " and "ms" were found and are in the correct order,
            string latencyString = pingOutput.Substring(startIndex + 10, endIndex - startIndex - 10);
            // Extract the latency value from the ping output
            if (double.TryParse(latencyString, out double averageLatency))
            {
                // Try to parse the extracted latency value as a double
                return averageLatency;      // Return the parsed average latency
            }
        }
        // If parsing fails or the expected substrings are not found, throw an exception
        throw new InvalidOperationException("Failed to parse average latency.");   
    }
}
