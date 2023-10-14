using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("Select an option:");
        Console.WriteLine("[1] View the routing table of the local device.");
        Console.WriteLine("[2] Enter the IP address of the device to display its routing table.");

        Console.Write("Enter your choice (1 or 2): ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            // View the routing table of the local device
            ViewLocalRoutingTable();
        }
        else if (choice == "2")
        {
            // Prompt the user to enter the IP address of the device to display its routing table
            Console.Write("Enter the IP address of the device to display its routing table: ");
            string targetIP = Console.ReadLine();  // Read the user's input and store it in the targetIP variable

            // Display the routing table for the specified device
            DisplayRoutingTable(targetIP);
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter either '1' or '2'.");
        }
    }

    static void ViewLocalRoutingTable()
    {
        // Display the routing table for the local device
        DisplayRoutingTable(null);
    }

    static void DisplayRoutingTable(string targetIP)
    {
        // Create a new process start info
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",           // Specify the command to run (cmd.exe)
            RedirectStandardInput = true,   // Redirect standard input stream for interaction
            RedirectStandardOutput = true,  // Redirect standard output stream for capturing output
            UseShellExecute = false,        // Don't use the system shell to execute the command
            CreateNoWindow = true           // Don't create a visible window for the command prompt
        };

        // Create a new process
        Process process = new Process
        {
            StartInfo = psi,            // Set the start info for the process
            EnableRaisingEvents = true  // Enable event handling for the process
        };

        // Redirect the output data received from the process to the console
        process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);

        // Start the process
        process.Start();

        // Begin asynchronous read of the output
        process.BeginOutputReadLine();

        // Send the "route print" command with the specified IP address or null (local routing table)
        process.StandardInput.WriteLine(targetIP != null ? $"route print {targetIP}" : "route print");

        // Close the input stream and wait for the process to exit
        process.StandardInput.Close();
        process.WaitForExit();
    }
}
