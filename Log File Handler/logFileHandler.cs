using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capacity_Planning_Tool_CLI
{
    class logFileHandler
    {
        public void createLog(string filePath)
        {
            string content = "";

            try
            {
                // Write the string content to the file
                File.WriteAllText(filePath, content);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void appendLog(string filePath, string content)
        {

            try
            {
                // Write the string content to the file
                File.AppendAllText(filePath, content);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
