# Ping Latency Checker

The Ping Latency Checker is a simple C# console application that allows you to check the average latency (ping time) to a specified hostname or IP address by sending a specified number of ICMP packets. This tool utilizes the "ping" command to perform these latency checks and provides the average latency in milliseconds.

## Usage

1. When prompted, enter the hostname or IP address of the target you want to ping. You can use any valid hostname or IP address.

2. Next, enter the number of packets you want to send to the target for latency measurement.

3. The program will execute the "ping" command with the provided parameters and display the average latency if successful.

## Example

Here is an example of how to use the Ping Latency Checker:

```bash
Enter hostname or IP address: google.com
Enter the number of packets to send: 10
Currently pinging 10 packets...
Average Latency to google.com: 4 ms
```

In this example, the program successfully pinged "example.com" with 5 packets and displayed the average latency as 30 milliseconds.

## Error Handling

The program includes error handling to address the following scenarios:

- Invalid user input: If you provide an invalid hostname, IP address, or packet count, the program will display an error message.

- Ping failure: If the "ping" command fails for any reason, such as the target being unreachable, the program will display an error message.

## Author
- Dende Te
