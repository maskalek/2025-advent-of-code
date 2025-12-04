#region Usings
// Common system namespaces
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text;
 using System.Threading;
 using System.Threading.Tasks;

// I/O
 using System.IO;

// Diagnostics & Debugging
 using System.Diagnostics;

// Environment & Runtime
 using System.Runtime;
 using System.Runtime.InteropServices;

// Collections and Data
 using System.Collections;
 using System.Collections.ObjectModel;

// JSON
 using System.Text.Json;
 using System.Text.Json.Serialization;


// HTTP (if your console app makes HTTP requests)
 using System.Net.Http;
 using System.Net.Http.Json;
#endregion

// var lines = File.ReadAllLines("day2/task2/test.txt");
var lines = File.ReadAllLines("day2/task2/puzzle.txt");
var ranges = GetRanges(lines[0]);
long totalSum = 0;
foreach(var range in ranges)
{
    // Console.WriteLine($"Processing range {range[0]}-{range[1]}");
    var sum = GetInvalidNumbersSumInRange(range[0], range[1]);
    totalSum += sum;
    Console.WriteLine($"The sum of invalid numbers in range {range[0]}-{range[1]} is {sum}");
}

Console.WriteLine($"The total sum of invalid numbers is {totalSum}");

// 11-22,95-115
long[][] GetRanges(string line)
{
    var ranges = new List<long[]>();
    foreach(var strRange in line.Split(','))
    {
        var parts = strRange.Split('-');
        var min = long.Parse(parts[0]);
        var max = long.Parse(parts[1]);
        ranges.Add(new long[] { min, max });
    }
    return ranges.ToArray();
}

long GetInvalidNumbersSumInRange(long min, long max)
{
    long sum = 0;
    for(var number = min; number <= max; number++)
    {
        if(!IsValidNumber(number))
        {
            Console.WriteLine($"Invalid number found: {number}");
            sum += number;
        }
    }
    return sum;
}

// 123123123
bool IsValidNumber(long number)
{
    var strNumber = number.ToString();
    var left = 0;
    for(var right = 1; right < strNumber.Length; right++)
    {
        if(strNumber[left] != strNumber[right]) continue;
        if(!IsValidNumber(strNumber, right - left)) return false;
    }
    return true;

    

    bool IsValidNumber(string strNumber, int length)
    {
        if(strNumber.Length % length != 0) return true;
        var subnumber = strNumber.Substring(0, length);
        var sb = new StringBuilder();
        while(sb.Length != strNumber.Length)
        {
            sb.Append(subnumber);
        }
        return sb.ToString() != strNumber;
    }
}