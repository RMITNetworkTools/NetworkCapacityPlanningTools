using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capacity_Planning_Tool_CLI
{
    class tracert
    {
        public void traceroute()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "tracert",
                Arguments = "192.168.20.1", // replace with your target
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            logFileHandler tracertLog = new logFileHandler();
            tracertLog.createLog("traceroute.txt");

            using (var process = new Process { StartInfo = startInfo })
            {
                process.Start();

                while (!process.StandardOutput.EndOfStream)
                {
                    var line = process.StandardOutput.ReadLine();
                    Console.WriteLine(line);
                    tracertLog.appendLog("traceroute.txt", line + "\n");
                }
            }

            Console.Write("\nPress Enter to Exit: ");
            Console.ReadLine();

        }
    }
}
