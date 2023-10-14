using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capacity_Planning_Tool_CLI
{
    class interfaceUsage
    {
        public void getInterfaceUsageCumulative()
        {
            try
            {
                string ipAddress = "192.168.20.7"; // Replace with the IP address you want to monitor

                IPAddress targetIp = IPAddress.Parse(ipAddress);

                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

                NetworkInterface targetInterface = Array.Find(interfaces, ni =>
                {
                    IPInterfaceProperties ipProps = ni.GetIPProperties();
                    return ipProps.UnicastAddresses.Any(addr => addr.Address.Equals(targetIp));
                });

                if (targetInterface != null)
                {
                    IPv4InterfaceStatistics stats = targetInterface.GetIPv4Statistics();
                    long initialReceived = stats.BytesReceived;

                    while (true)
                    {
                        stats = targetInterface.GetIPv4Statistics();
                        long currentReceived = stats.BytesReceived;
                        long usage = (currentReceived - initialReceived) / 1024;

                        DateTime now = DateTime.Now;
                        string timestamp = now.ToString("yyyy-MM-dd HH:mm:ss");

                        Console.WriteLine($"[{timestamp}] Network usage for {ipAddress}: {usage} KB");

                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine($"No network interface found for {ipAddress}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            Console.Write("\nPress Enter to Exit: ");
            Console.ReadLine();
        }
        public void getInterfaceUsageRealtime()
        {
            try
            {
                string ipAddress = "192.168.20.7"; // Replace with the IP address you want to monitor

                IPAddress targetIp = IPAddress.Parse(ipAddress);

                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

                NetworkInterface targetInterface = Array.Find(interfaces, ni =>
                {
                    IPInterfaceProperties ipProps = ni.GetIPProperties();
                    return ipProps.UnicastAddresses.Any(addr => addr.Address.Equals(targetIp));
                });

                if (targetInterface != null)
                {
                    while (true)
                    {
                        IPv4InterfaceStatistics stats = targetInterface.GetIPv4Statistics();
                        long initialReceived = stats.BytesReceived;

                        Thread.Sleep(1000);

                        stats = targetInterface.GetIPv4Statistics();
                        long currentReceived = stats.BytesReceived;
                        long usage = (currentReceived - initialReceived) / 1024;

                        DateTime now = DateTime.Now;
                        string timestamp = now.ToString("yyyy-MM-dd HH:mm:ss");

                        Console.WriteLine($"[{timestamp}] Network usage for {ipAddress}: {usage} KB in the last second");
                    }
                }
                else
                {
                    Console.WriteLine($"No network interface found for {ipAddress}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
