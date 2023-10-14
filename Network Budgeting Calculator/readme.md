High-Level Explanation:

In the application, we've built a system that processes data usage metrics from various devices. This data is stored in a CSV format, with each row representing a device's data usage over a specific time period. The primary goal of the application is to calculate and present the total and average bandwidth used by these devices over a standardized time period and data type.

The application is structured around two primary classes: TimePeriod and DataType. 

- TimePeriod deals with the conversion and manipulation of time-related metrics. It can convert between different time units like weeks, months, and years. This ensures that when we're analyzing data usage, we can standardize the time frame for a consistent comparison.
  
- DataType handles the conversion and manipulation of data size metrics. It can convert between different data units like bytes, kilobytes, megabytes, etc. This ensures that we can standardize the data size for a consistent comparison.

The main program reads the CSV data, processes it using the aforementioned classes, and then calculates the total and average bandwidth used. The results are then displayed to the user.

Low-Level Explanation:

Diving deeper into the code:

1. TimePeriod Class: 
    - Constants like WEEK, MONTH, and YEAR represent the number of days in each time period.
    - The Convert method translates string representations (e.g., "week") into their numeric equivalents in days.
    - Deconvert does the opposite, translating numeric values back into string representations.
    - TimePeriodConverter is a method that, given two time periods and a time value, converts the time value from the first period to the second.

2. DataType Class:
    - We've defined constants for data sizes using BigInteger to handle very large numbers without overflow.
    - The Convert method translates string representations (e.g., "MB") into their BigInteger equivalents.
    - Deconvert translates BigInteger values back into string representations.
    - DataTypeConverter is a method that, given two data types and a data value, converts the data value from the first type to the second. It uses a dictionary to map conversions between different data types.

3. Main Program:
    - ReadCsv reads the CSV file, skipping the header, and returns a list of string arrays.
    - GenerateData creates a CSV file with random data for testing purposes.
    - GetData reads the CSV data, processes each row to standardize the time period and data type, and returns a list of processed data.
    - TotalBandwidth calculates the total bandwidth used based on the provided data and the selected data type.
    - In the Main method, we set our desired data type and time period, retrieve and process the data, calculate the total and average bandwidth, and then display the results.

Throughout the application, we've utilized dictionaries for efficient lookups, especially for conversions. The use of BigInteger ensures that we can handle very large data sizes without running into overflow issues.