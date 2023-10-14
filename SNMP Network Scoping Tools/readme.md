# SNMP Monitoring Tool for Network Capacity Planning

This SNMP Monitoring Tool is designed to monitor network devices, specifically focusing on CPU utilization, CPU temperature, and memory utilization. By leveraging the SNMP protocol, this tool fetches real-time data from network devices and logs them for further analysis.

## Benefits in Network Capacity Planning Environments

1. **Real-time Monitoring**: Provides immediate insights into device performance, allowing for quick detection and resolution of issues.
2. **Historical Data Analysis**: By logging data over time, network administrators can analyze trends, predict future issues, and make informed decisions about upgrades or reconfigurations.
3. **Concurrency**: The tool's multi-threaded design allows for simultaneous monitoring of multiple devices, making it scalable for large networks.
4. **Bottleneck Detection**: By monitoring key metrics, the tool can help identify devices that are overutilized and might be causing network bottlenecks.
5. **Proactive Maintenance**: Before devices fail or cause network disruptions, administrators can proactively address issues, leading to higher network uptime and reliability.

## High-Level Explanation

The tool continuously queries specified network devices (like routers) for key metrics: CPU utilization, CPU temperature, and memory utilization. If any of these metrics exceed predefined thresholds, an alert is displayed. All the data is also logged to CSV files for historical analysis.

## Low-Level Explanation

1. **Initialization**: At the start, the SNMP community string and the IP addresses of the devices to be monitored are defined.
2. **Threading**: For each device, the tool spawns separate threads to monitor different metrics. This ensures that the monitoring process for each metric and device runs concurrently, allowing for real-time data collection.
3. **SNMP Interaction**: The tool interacts with the devices using the SNMP protocol. It sends GET requests to the devices, targeting specific OIDs (Object Identifiers) that correspond to the metrics of interest.
4. **Data Processing**: Once the data is fetched, it's processed to check against predefined thresholds. If the fetched data exceeds these thresholds, an alert is generated.
5. **Logging Mechanism**: Every piece of data fetched is logged into CSV files. These files are organized per metric and per device, ensuring that data is stored systematically for future analysis.
6. **Error Management**: The tool is equipped to handle common errors, such as when a device is unreachable or when unexpected data is returned. In such cases, relevant error messages are displayed.

