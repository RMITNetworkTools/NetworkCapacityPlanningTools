using System;
using System.Collections.Generic;
using System.Threading;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System.IO;

namespace SNMPMonitoring
{
    class Program
    {
        /// <summary>
        /// Queries the CPU utilization of a device using SNMP and logs the results to a CSV file.
        /// </summary>
        /// <param name="IP">The IP address of the device.</param>
        /// <param name="Community">The SNMP community string.</param>
        /// <param name="OID">The OID for CPU utilization.</param>
        /// <param name="Threshold">The CPU utilization threshold for alerts.</param>
        /// <param name="Interval">The interval (in milliseconds) between queries.</param>
        static void QueryCPUUtilization(string IP, string Community, string OID, int Threshold, int Interval)
        {
            var endpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(IP), 161);
            var oid = new ObjectIdentifier(OID);
            var variables = new List<Variable> { new Variable(oid) };

            while (true)
            {
                try
                {
                    var result = Messenger.Get(VersionCode.V2, endpoint, new OctetString(Community), variables, 60000);
                    foreach (var variable in result)
                    {
                        int cpuUsage = int.Parse(variable.Data.ToString());
                        if (cpuUsage > Threshold)
                        {
                            Console.WriteLine($"ALERT: CPU utilization for device {IP} is above {Threshold}%!");
                        }

                        // Log to CSV
                        string csvPath = $"cpu_usage_log_{IP}.csv";
                        bool fileExists = File.Exists(csvPath);
                        using (StreamWriter writer = new StreamWriter(csvPath, true))
                        {
                            if (!fileExists)
                            {
                                writer.WriteLine("Timestamp, CPU Utilization (%)");
                            }
                            writer.WriteLine($"{DateTime.Now}, {cpuUsage}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Thread.Sleep(Interval);
            }
        }

        /// <summary>
        /// Queries the CPU temperature of a device using SNMP and logs the results to a CSV file.
        /// </summary>
        /// <param name="IP">The IP address of the device.</param>
        /// <param name="Community">The SNMP community string.</param>
        /// <param name="OID">The OID for CPU temperature.</param>
        /// <param name="MaxTemperature">The maximum allowed CPU temperature for alerts.</param>
        /// <param name="Interval">The interval (in milliseconds) between queries.</param>
        static void QueryCPUTemperature(string IP, string Community, string OID, int MaxTemperature, int Interval)
        {
            var endpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(IP), 161);
            var oid = new ObjectIdentifier(OID);
            var variables = new List<Variable> { new Variable(oid) };

            while (true)
            {
                try
                {
                    var result = Messenger.Get(VersionCode.V2, endpoint, new OctetString(Community), variables, 60000);
                    foreach (var variable in result)
                    {
                        if (variable.Data.ToString() == "NoSuchObject")
                        {
                            Console.WriteLine($"No temperature sensor found on device {IP}.");
                            return;
                        }

                        int cpuTemperature = int.Parse(variable.Data.ToString());
                        if (cpuTemperature > MaxTemperature)
                        {
                            Console.WriteLine($"ALERT: CPU temperature for device {IP} is above {MaxTemperature}°C!");
                        }

                        // Log to CSV
                        string csvPath = $"cpu_temperature_log_{IP}.csv";
                        bool fileExists = File.Exists(csvPath);
                        using (StreamWriter writer = new StreamWriter(csvPath, true))
                        {
                            if (!fileExists)
                            {
                                writer.WriteLine("Timestamp, CPU Temperature (°C)");
                            }
                            writer.WriteLine($"{DateTime.Now}, {cpuTemperature}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Thread.Sleep(Interval);
            }
        }

        /// <summary>
        /// Queries the memory utilization of a device using SNMP and logs the results to a CSV file.
        /// </summary>
        /// <param name="IP">The IP address of the device.</param>
        /// <param name="Community">The SNMP community string.</param>
        /// <param name="OIDUsedMemory">The OID for used memory.</param>
        /// <param name="OIDTotalMemory">The OID for total memory.</param>
        /// <param name="MaxMemoryPercentage">The maximum allowed memory utilization percentage for alerts.</param>
        /// <param name="Interval">The interval (in milliseconds) between queries.</param>
        static void QueryMemoryUtilization(string IP, string Community, string OIDUsedMemory, string OIDTotalMemory, int MaxMemoryPercentage, int Interval)
        {
            var endpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(IP), 161);
            var oidUsedMemory = new ObjectIdentifier(OIDUsedMemory);
            var oidTotalMemory = new ObjectIdentifier(OIDTotalMemory);
            var variables = new List<Variable> { new Variable(oidUsedMemory), new Variable(oidTotalMemory) };

            while (true)
            {
                try
                {
                    var result = Messenger.Get(VersionCode.V2, endpoint, new OctetString(Community), variables, 60000);
                    
                    int usedMemory = 0;
                    int totalMemory = 0;

                    foreach (var variable in result)
                    {
                        if (variable.Id.ToString() == OIDUsedMemory)
                        {
                            usedMemory = int.Parse(variable.Data.ToString());
                        }
                        else if (variable.Id.ToString() == OIDTotalMemory)
                        {
                            totalMemory = int.Parse(variable.Data.ToString());
                        }
                    }

                    if (totalMemory == 0)
                    {
                        Console.WriteLine("Error: Total memory reported as zero. Cannot compute percentage.");
                        continue;
                    }

                    int memoryPercentage = (usedMemory * 100) / totalMemory;

                    if (memoryPercentage > MaxMemoryPercentage)
                    {
                        Console.WriteLine($"ALERT: Memory utilization for device {IP} is above {MaxMemoryPercentage}%!");
                    }

                    // Log to CSV
                    string csvPath = $"memory_utilization_log_{IP}.csv";
                    bool fileExists = File.Exists(csvPath);
                    using (StreamWriter writer = new StreamWriter(csvPath, true))
                    {
                        if (!fileExists)
                        {
                            writer.WriteLine("Timestamp, Used Memory, Total Memory, Memory Utilization (%)");
                        }
                        writer.WriteLine($"{DateTime.Now}, {usedMemory}, {totalMemory}, {memoryPercentage}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Thread.Sleep(Interval);
            }
        }

        //getOID. gets the oid and returns the value
        static string getVariables(string IP, string Community, string OID)
        {
            var endpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(IP), 161);
            var oid = new ObjectIdentifier(OID);
            var variables = new List<Variable> { new Variable(oid) };
            var result = Messenger.Get(VersionCode.V2, endpoint, new OctetString(Community), variables, 60000);
            //return variables
            return result.ToString();
        
        }

        static string[] getTargetInfo(string IP, string Community)
        {
            const string sysDescrOID  = "1.3.6.1.2.1.1.1.0";
            const string sysObjectIDOID  = "1.3.6.1.2.1.1.2.0";
            const string sysNameOID = "1.3.6.1.2.1.1.5.0";

            string sysDescr = getVariables(IP, Community, sysDescrOID);
            string sysObjectID = getVariables(IP, Community, sysObjectIDOID);
            string sysName = getVariables(IP, Community, sysNameOID);
            
            //return 
            string[] targetInfo = {sysDescr, sysObjectID, sysName};
            return targetInfo;
        }

        /// <summary>
        /// Initiates the monitoring threads for CPU utilization, CPU temperature, and memory utilization.
        /// </summary>
        /// <param name="Community">The SNMP community string.</param>
        /// <param name="IP">The IP address of the device.</param>
        static void Runner(string Community, string IP)
        {
            const int cpu_usage_threshold = 15;
            const int cpu_temperature_threshold = 10;
            const int memory_utilization_threshold = 80;
            const int interval = 10000;
            const string CPU_Usage_OID = "1.3.6.1.4.1.9.9.109.1.1.1.1.3.1";
            const string CPU_Temperature_OID = "1.3.6.1.4.1.9.9.13.1.3.1.3";
            const string OIDUsedMemory = "1.3.6.1.4.1.9.9.48.1.1.1.5.1";
            const string OIDTotalMemory = "1.3.6.1.4.1.9.9.48.1.1.1.6.1";

            Thread cpuUsageThread = new Thread(() => QueryCPUUtilization(IP, Community, CPU_Usage_OID, cpu_usage_threshold, interval));
            Thread cpuTemperatureThread = new Thread(() => QueryCPUTemperature(IP, Community, CPU_Temperature_OID, cpu_temperature_threshold, interval));
            Thread memoryUtilizationThread = new Thread(() => QueryMemoryUtilization(IP, Community, OIDUsedMemory, OIDTotalMemory, memory_utilization_threshold, interval));

            cpuUsageThread.Start();
            cpuTemperatureThread.Start();
            memoryUtilizationThread.Start();

            Console.ReadLine();
        }

        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        static void Main()
        {
            const string Community = "TestEnv";
            const string IP = "10.10.10.2";

            Thread Device1 = new Thread(() => Runner(Community, IP));
            Device1.Start();

            //get the target info
            string[] targetInfo = getTargetInfo(IP, Community);
            //save  target info to csv
            string csvPath = $"target_info_{IP}.csv";
            bool fileExists = File.Exists(csvPath);
            using (StreamWriter writer = new StreamWriter(csvPath, true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Timestamp, System Description, System Object ID, System Name");
                }
                writer.WriteLine($"{DateTime.Now}, {targetInfo[0]}, {targetInfo[1]}, {targetInfo[2]}");
            }
        }
    }
}