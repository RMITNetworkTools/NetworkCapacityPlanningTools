using System;
using System.Diagnostics;
using System.Linq;

class NetworkLinkSpeed
{
    static void Main()
    {
        string command = "wmic nic where NetEnabled=true get Name,Speed";
        string output = RunCommand(command);

        string[] lines = output.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (!string.IsNullOrEmpty(line) && !line.Contains("Name") && !line.Contains("Speed"))
            {
                string[] tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length >= 2)
                {
                    string name = string.Join(" ", tokens.Take(tokens.Length - 1)); // Combine all tokens except the last one
                    string speed = tokens[tokens.Length - 1];

                    Console.WriteLine($"Interface: {name}, Speed: {speed} bps");
                }
            }
        }
    }

    static string RunCommand(string command)
    {
        Process process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                Arguments = $"/c {command}"
            }
        };

        process.Start();

        string output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();
        process.Close();

        return output;
    }
}
