# Network Link Speed Checker

The **Network Link Speed Checker** is a simple C# console application that allows you to check the speed of network interfaces on your Windows machine. It retrieves information about network interfaces and their corresponding link speeds.

## Usage

When you run the application, it executes a command using the Windows Management Instrumentation Command-line (WMIC) to query network interface information and link speeds. The application then processes the output and displays the information in a human-readable format.

You will see a list of network interfaces along with their respective link speeds in bits per second (bps).

## How to Use

1. Compile the C# code using your preferred C# compiler or an integrated development environment (IDE) like Visual Studio or Visual Studio Code.

2. Run the compiled executable.

3. The application will execute the WMIC command and display the network interface names and their link speeds in bits per second (bps).

4. Review the output to see the network interface names and their corresponding link speeds.

## Note

- The application is intended for Windows systems and relies on the availability of the `wmic` command, which is a standard utility on Windows machines.

- Please ensure that the command prompt and the `wmic` utility are accessible on your system for the application to work correctly.

- The output may include additional network interfaces, so review the list to find the interface of interest.

- This tool can be helpful for understanding the link speeds of various network interfaces on your Windows machine, which may be useful for network configuration and troubleshooting.
