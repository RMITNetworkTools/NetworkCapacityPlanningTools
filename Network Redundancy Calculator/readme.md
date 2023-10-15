Network Capacity Planning Tool - Redundancy Calculator
The Redundancy Calculator is a simple C# console application designed to help you calculate the required redundancy of network links or devices based on your desired uptime percentage and the average downtime of a single link or device in hours. This tool is useful for network administrators and IT professionals involved in network capacity planning to ensure high availability and reliability of network resources.

How to Use
Compile the Code: Ensure you have the C# development environment set up. Compile the RedundancyCalculator.cs file to create an executable.

Run the Application: Execute the compiled program to run the Redundancy Calculator.

Input Desired Uptime Percentage: Enter the desired uptime percentage (e.g., 99.99) when prompted. This represents the level of availability you want to achieve.

Input Average Downtime: Enter the average downtime of a single link or device in hours when prompted.

Calculate Required Redundancy: The program will calculate the required redundancy and display the result.

View Result: The calculated required redundancy (number of redundant links or devices) will be shown on the screen.

Example
Here is an example of how to use the Redundancy Calculator:

plaintext
Copy code
Enter desired uptime percentage (e.g., 99.99):
99.99
You entered: 99.99%

Enter average downtime of a single link or device in hours:
1
You entered: 1 hours

Required redundancy: 88
In this example, the user specified a desired uptime percentage of 99.99% and an average downtime of 1 hour. The calculator determined that 88 redundant links or devices are required to achieve this level of uptime.

Error Handling
The Redundancy Calculator includes error handling to address invalid input scenarios:

If the desired uptime percentage is not between 0 and 100, an error message will be displayed.
If the average downtime is negative, an error message will be displayed.
Calculation Method
The Redundancy Calculator uses the following formula to calculate the required redundancy:

scss
Copy code
Required Redundancy = Ceiling(Average Downtime / (Uptime Requirement in Hours))
Average Downtime is the average downtime of a single link or device in hours.
Uptime Requirement in Hours is calculated based on the desired uptime percentage and is equivalent to the annual downtime allowed to achieve the desired uptime.
Customization
You can customize and integrate the Redundancy Calculator into your network capacity planning processes to determine the appropriate level of redundancy required for your specific network configuration.

Feel free to modify and extend the code to suit your organization's requirements and integrate it into your network management tools.

Feedback and Contributions
If you have any feedback, suggestions, or would like to contribute to the development of this tool, please feel free to reach out. Your input is valuable in improving the functionality and usability of network capacity planning tools like the Redundancy Calculator.
