using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

/// <summary>
/// Represents different time periods in days.
/// </summary>
public class TimePeriod
{
    // Constants representing time periods in days.
    public const int WEEK = 7;
    public const int MONTH = 30;
    public const int YEAR = 365;

    /// <summary>
    /// Converts a string representation of a time period to its corresponding value in days.
    /// </summary>
    /// <param name="timePeriod">The string representation of the time period.</param>
    /// <returns>The number of days corresponding to the time period.</returns>
    public static int Convert(string timePeriod)
    {
        switch (timePeriod.ToLower())
        {
            case "week":
                return WEEK;
            case "month":
                return MONTH;
            case "year":
                return YEAR;
            default:
                return 0; // Default value if the input doesn't match any known time period.
        }
    }

    /// <summary>
    /// Converts a numeric representation of a time period to its string equivalent.
    /// </summary>
    /// <param name="days">The number of days.</param>
    /// <returns>The string representation of the time period.</returns>
    public static string Deconvert(int days)
    {
        if (days == WEEK) return "week";
        if (days == MONTH) return "month";
        if (days == YEAR) return "year";
        return null; // Return null if the input doesn't match any known time period.
    }

    /// <summary>
    /// Converts a time value from one time period to another.
    /// </summary>
    /// <param name="tp1">The original time period.</param>
    /// <param name="tp2">The target time period.</param>
    /// <param name="time">The time value to convert.</param>
    /// <returns>The converted time value.</returns>
    public static double TimePeriodConverter(int tp1, int tp2, double time)
    {
        // Dictionary to map conversions between different time periods.
        var conversion = new Dictionary<(int, int), double>
        {
            {(WEEK, MONTH), time / 4},
            {(WEEK, YEAR), time / 52},
            {(MONTH, WEEK), time * 4},
            {(MONTH, YEAR), time / 12},
            {(YEAR, WEEK), time * 52},
            {(YEAR, MONTH), time * 12}
        };

        return conversion.GetValueOrDefault((tp1, tp2), time); // Return the converted value or the original value if no conversion is found.
    }
}

/// <summary>
/// Represents different data types and their sizes.
/// </summary>
public class DataType
{
    // Constants representing data sizes.
    public static readonly BigInteger B = 1;
    public static readonly BigInteger KB = 1024 * B;
    public static readonly BigInteger MB = 1024 * KB;
    public static readonly BigInteger GB = 1024 * MB;
    public static readonly BigInteger TB = 1024 * GB;

    /// <summary>
    /// Converts a string representation of a data type to its corresponding value in bytes.
    /// </summary>
    /// <param name="dataType">The string representation of the data type.</param>
    /// <returns>The size of the data type in bytes.</returns>
    public static BigInteger Convert(string dataType)
    {
        switch (dataType.ToUpper())
        {
            case "B":
                return B;
            case "KB":
                return KB;
            case "MB":
                return MB;
            case "GB":
                return GB;
            case "TB":
                return TB;
            default:
                return 0; // Default value if the input doesn't match any known data type.
        }
    }

    /// <summary>
    /// Converts a numeric representation of a data type to its string equivalent.
    /// </summary>
    /// <param name="value">The size of the data type in bytes.</param>
    /// <returns>The string representation of the data type.</returns>
    public static string Deconvert(BigInteger value)
    {
        if (value == B) return "B";
        if (value == KB) return "KB";
        if (value == MB) return "MB";
        if (value == GB) return "GB";
        if (value == TB) return "TB";
        return null; // Return null if the input doesn't match any known data type.
    }

    /// <summary>
    /// Converts a data value from one data type to another.
    /// </summary>
    /// <param name="dt1">The original data type.</param>
    /// <param name="dt2">The target data type.</param>
    /// <param name="data">The data value to convert.</param>
    /// <returns>The converted data value.</returns>
    public static BigInteger DataTypeConverter(BigInteger dt1, BigInteger dt2, BigInteger data)
    {
        // Dictionary to map conversions between different data types.
        var conversion = new Dictionary<(BigInteger, BigInteger), Func<BigInteger, BigInteger>>
        {
            {(B, KB), d => BigInteger.Divide(d, 1024)},
            {(B, MB), d => BigInteger.Divide(d, BigInteger.Multiply(1024, 1024))},
            {(B, GB), d => BigInteger.Divide(d, BigInteger.Multiply(1024, BigInteger.Multiply(1024, 1024)))},
            {(B, TB), d => BigInteger.Divide(d, BigInteger.Multiply(1024, BigInteger.Multiply(1024, BigInteger.Multiply(1024, 1024))))},
            
            {(KB, B), d => BigInteger.Multiply(d, 1024)},
            {(KB, MB), d => BigInteger.Divide(d, 1024)},
            {(KB, GB), d => BigInteger.Divide(d, BigInteger.Multiply(1024, 1024))},
            {(KB, TB), d => BigInteger.Divide(d, BigInteger.Multiply(1024, BigInteger.Multiply(1024, 1024)))},
            
            {(MB, B), d => BigInteger.Multiply(d, BigInteger.Multiply(1024, 1024))},
            {(MB, KB), d => BigInteger.Multiply(d, 1024)},
            {(MB, GB), d => BigInteger.Divide(d, 1024)},
            {(MB, TB), d => BigInteger.Divide(d, BigInteger.Multiply(1024, 1024))},
            
            {(GB, B), d => BigInteger.Multiply(d, BigInteger.Multiply(1024, BigInteger.Multiply(1024, 1024)))},
            {(GB, KB), d => BigInteger.Multiply(d, BigInteger.Multiply(1024, 1024))},
            {(GB, MB), d => BigInteger.Multiply(d, 1024)},
            {(GB, TB), d => BigInteger.Divide(d, 1024)},
            
            {(TB, B), d => BigInteger.Multiply(d, BigInteger.Multiply(1024, BigInteger.Multiply(1024, BigInteger.Multiply(1024, 1024))))},
            {(TB, KB), d => BigInteger.Multiply(d, BigInteger.Multiply(1024, BigInteger.Multiply(1024, 1024)))},
            {(TB, MB), d => BigInteger.Multiply(d, BigInteger.Multiply(1024, 1024))},
            {(TB, GB), d => BigInteger.Multiply(d, 1024)}
        };

        // Return the converted value or the original value if no conversion is found.
        return conversion.ContainsKey((dt1, dt2)) ? conversion[(dt1, dt2)](data) : data;
    }
}

/// <summary>
/// Main program class.
/// </summary>
public class Program
{
    /// <summary>
    /// Reads data from a CSV file.
    /// </summary>
    /// <param name="filename">The name of the CSV file.</param>
    /// <returns>A list of string arrays representing the rows and columns of the CSV file.</returns>
    public static List<string[]> ReadCsv(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var data = new List<string[]>();
        for (int i = 1; i < lines.Length; i++)  // Start from index 1 to skip the header
        {
            data.Add(lines[i].Split(','));
        }
        return data;
    }

    /// <summary>
    /// Generates random data and writes it to a CSV file.
    /// </summary>
    /// <param name="count">The number of data rows to generate.</param>
    public static void GenerateData(int count = 47)
    {
        using (var writer = new StreamWriter("data.csv"))
        {
            writer.WriteLine("device_id,used_data,used_data_type,time,time_period");
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                var data = $"{i},{random.Next(100, 20000)},MB,1,month";
                writer.WriteLine(data);
            }
        }
    }

    /// <summary>
    /// Retrieves and processes data from the CSV file.
    /// </summary>
    /// <param name="selectedTimePeriod">The target time period for the data.</param>
    /// <returns>A list of string arrays representing the processed data.</returns>
    public static List<string[]> GetData(int selectedTimePeriod)
    {
        var data = new List<string[]>();
        var csvData = ReadCsv("data.csv");
        foreach (var row in csvData)
        {
            if (row.Length != 5) continue;
            var timePeriod = TimePeriod.Convert(row[4]);
            var time = int.Parse(row[3]);
            row[3] = TimePeriod.TimePeriodConverter(timePeriod, selectedTimePeriod, time).ToString();
            row[4] = TimePeriod.Deconvert(selectedTimePeriod);
            row[1] = (int.Parse(row[1]) / double.Parse(row[3])).ToString();
            row[3] = "1";
            data.Add(row);
        }
        return data;
    }

    /// <summary>
    /// Calculates the total bandwidth used based on the provided data.
    /// </summary>
    /// <param name="data">The data to process.</param>
    /// <param name="selectedDataType">The target data type for the bandwidth calculation.</param>
    /// <returns>The total bandwidth used.</returns>
    public static BigInteger TotalBandwidth(List<string[]> data, BigInteger selectedDataType)
    {
        BigInteger total = 0;
        foreach (var row in data)
        {
            var dataInDataType = DataType.Convert(row[2]);
            var bandwidth = DataType.DataTypeConverter(dataInDataType, selectedDataType, BigInteger.Parse(row[1]));
            total += bandwidth;
        }
        return total;
    }

    /// <summary>
    /// The main entry point for the program.
    /// </summary>
    public static void Main()
    {
        var selectedDataType = DataType.MB;
        var selectedTimePeriod = TimePeriod.MONTH;

        var data = GetData(selectedTimePeriod);
        var totalBandwidthFinal = TotalBandwidth(data, selectedDataType);
        Console.WriteLine($"{totalBandwidthFinal} {DataType.Deconvert(selectedDataType)} per {TimePeriod.Deconvert(selectedTimePeriod)}");

        var average = totalBandwidthFinal / data.Count;
        Console.WriteLine($"Average: {average} {DataType.Deconvert(selectedDataType)} per user per {TimePeriod.Deconvert(selectedTimePeriod)}");

        GenerateData(117);
    }
}