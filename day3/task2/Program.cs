using System;
using System.IO;
using System.Text;
using System.Reflection;

string[] lines = File.ReadAllLines("./day3/task2/puzzle.txt");

long totalJoltage = 0;
foreach(var line in lines)
{
    var maxJoltage = GetMaximumJoltage(line, 0, new StringBuilder());
    totalJoltage += maxJoltage;
    Console.WriteLine($"The adapter with rating {line} can produce a maximum joltage of {maxJoltage}");
}

Console.WriteLine($"The total maximum joltage is {totalJoltage}");

long GetMaximumJoltage(string joltageRatings, int index, StringBuilder current)
{
    if(current.Length == 12)
    {
        return long.Parse(current.ToString());
    }
    for(var c = '9'; c >= '0'; c--)
    {
        for(var i = index; i < joltageRatings.Length; i++)
        {
            if(i + 12 - current.Length > joltageRatings.Length)
            {
                break;
            }
            
            if(joltageRatings[i] == c)
            {
                current.Append(c);
                return GetMaximumJoltage(joltageRatings, i + 1, current);
            }
        }
    }
    return 0;
}