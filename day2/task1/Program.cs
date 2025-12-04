var lines = File.ReadAllLines("day2/task1/puzzle.txt");
var ranges = GetRanges(lines[0]);
long totalSum = 0;
foreach(var range in ranges)
{
    Console.WriteLine($"Processing range {range[0]}-{range[1]}");
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
            sum += number;
        }
    }
    return sum;
}

bool IsValidNumber(long number)
{
    var strNumber = number.ToString();
    if(strNumber.Length % 2 == 1) return true;
    var left = 0;
    var right = strNumber.Length / 2;
    while(right < strNumber.Length)
    {
        if(strNumber[left] != strNumber[right]) return true;
        left++;
        right++;
    }
    return false;
}