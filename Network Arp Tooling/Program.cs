using System; // Import the System namespace for basic system functionality.
using System.Collections.Generic; // Import the System.Collections.Generic namespace for using generic collections like Dictionary.
using System.Diagnostics; // Import the System.Diagnostics namespace for working with processes.
using System.Net; // Import the System.Net namespace for handling IP addresses.
using System.Net.NetworkInformation; // Import the System.Net.NetworkInformation namespace for accessing network-related information.

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("ARP Table:"); // Display a message indicating the ARP table will be printed.

            foreach (var arpEntry in GetArpTable()) // Iterate through the ARP table entries.
            {
                Console.WriteLine($"IP Address: {arpEntry.Key}, MAC Address: {arpEntry.Value}"); // Display each ARP entry's IP and MAC addresses.
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}"); // Display an error message if an exception occurs.
        }
    }

    static Dictionary<IPAddress, PhysicalAddress> GetArpTable()
    {
        var arpTable = new Dictionary<IPAddress, PhysicalAddress>(); // Initialize an empty dictionary to store ARP table entries.

        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces()) // Iterate through all network interfaces on the local device.
        {
            var properties = networkInterface.GetIPProperties(); // Retrieve IP properties for the network interface.

            foreach (var unicastIPAddressInformation in properties.UnicastAddresses) // Iterate through unicast IP addresses.
            {
                if (unicastIPAddressInformation.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    // Check if the IP address family is IPv4.

                    foreach (var arpInfo in GetArpEntries(networkInterface.Id)) // Retrieve ARP entries for the network interface.
                    {
                        arpTable[arpInfo.Key] = arpInfo.Value; // Add ARP entries to the dictionary.
                    }
                }
            }
        }

        return arpTable; // Return the dictionary containing ARP table entries.
    }

    static Dictionary<IPAddress, PhysicalAddress> GetArpEntries(string interfaceId)
    {
        var arpTable = new Dictionary<IPAddress, PhysicalAddress>(); // Initialize an empty dictionary to store ARP entries.

        ProcessStartInfo psi = new ProcessStartInfo("arp", "-a")
        {
            UseShellExecute = false, // Configure the process to not use the shell for execution.
            CreateNoWindow = true, // Configure the process to create no window.
            RedirectStandardOutput = true, // Redirect the standard output of the process (capturing the ARP table).
        };

        Process process = new Process { StartInfo = psi }; // Create a new process with the specified start info.
        process.Start(); // Start the process.

        string output = process.StandardOutput.ReadToEnd(); // Read the standard output (ARP table) of the process.
        process.WaitForExit(); // Wait for the process to exit.

        string[] lines = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries); // Split the output into lines.
        foreach (string line in lines) // Iterate through lines in the output.
        {
            string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries); // Split each line into parts.
            if (parts.Length == 3) // Check if there are three parts (IP, MAC, and type).
            {
                try
                {
                    IPAddress ipAddress = IPAddress.Parse(parts[0]); // Parse the IP address.
                    PhysicalAddress macAddress = PhysicalAddress.Parse(parts[1]); // Parse the MAC address.
                    arpTable[ipAddress] = macAddress; // Add the IP-MAC mapping to the dictionary.
                }
                catch { } // Ignore any parsing errors.
            }
        }

        return arpTable; // Return the dictionary containing ARP entries.
    }
}
