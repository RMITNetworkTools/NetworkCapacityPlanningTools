
Bandwidth Calculator - Network Capacity Planning Tool
The Bandwidth Calculator is a C# console application designed to assist network administrators and IT professionals in calculating the required bandwidth for a network based on specific parameters. This tool is useful for network capacity planning, ensuring that the network has adequate bandwidth to meet user requirements during peak usage and redundancy scenarios.

How to Use
Compile the Code: Ensure you have a C# development environment like Visual Studio 2022 set up. Compile the BandwidthCalculator.cs file to create an executable.

Run the Application: Execute the compiled program to run the Bandwidth Calculator.

Input Parameters: Enter the following network parameters when prompted:

Total number of users or devices.
Average data usage per user or device (in Mbps).
Peak usage factor (e.g., 1.5 for 150% peak usage).
Redundancy factor (e.g., 1.2 for 20% redundancy).
Calculate Required Bandwidth: The program will calculate the required bandwidth based on the provided parameters.

View Result: The calculated required bandwidth (in Mbps) will be displayed on the screen.

Example
Here is an example of how to use the Bandwidth Calculator:


Welcome to the Bandwidth Calculator!

Enter the following parameters:
Total number of users/devices: 100
Average data usage per user/device (in Mbps): 5
Peak usage factor (e.g., 1.5 for 150% peak usage): 1.5
Redundancy factor (e.g., 1.2 for 20% redundancy): 1.2

Required bandwidth: 900 Mbps
In this example, the user specified 100 users/devices with an average data usage of 5 Mbps per user/device. The peak usage factor was set to 1.5 (indicating 150% peak usage), and the redundancy factor was set to 1.2 (indicating 20% redundancy). The calculator determined that 900 Mbps of bandwidth is required to meet these parameters.

Error Handling
The Bandwidth Calculator includes error handling to address invalid input scenarios:

If any input parameter is less than or equal to zero, the program will display an error message and request valid input.
Calculation Method
The Bandwidth Calculator calculates the required bandwidth using the following formula:


Required Bandwidth (Mbps) = Total Users/Devices * Average Data Usage per User/Device (Mbps) * Peak Usage Factor * Redundancy Factor
Total Users/Devices is the total number of users or devices on the network.
Average Data Usage per User/Device (Mbps) is the average data usage per user or device in Mbps.
Peak Usage Factor represents the peak usage as a factor (e.g., 1.5 for 150% peak usage).
Redundancy Factor represents the redundancy level as a factor (e.g., 1.2 for 20% redundancy).
Customization
You can customize and integrate the Bandwidth Calculator into your network capacity planning processes to determine the appropriate bandwidth required for your specific network configuration. Feel free to modify and extend the code to suit your organization's requirements and integrate it into your network management tools.
