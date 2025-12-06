// var lines = File.ReadLines("day5/task1/test.txt");
var lines = File.ReadLines("day5/task1/puzzle.txt").ToArray();

var isFirstHalf = true;
var freshRanges = new List<long[]>();
var candidates = new List<long>();

foreach(var line in lines)
{
    if(string.IsNullOrWhiteSpace(line))
    {
        isFirstHalf = false;
        continue;
    }

    // parse ranges
    if(isFirstHalf)
    {
        var parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
        freshRanges.Add(new long[] { long.Parse(parts[0]), long.Parse(parts[1]) });
        continue;
    }

    // if met emtpy line, switch to second half
    // parse candidates

    candidates.Add(long.Parse(line));
    
}

foreach(var range in freshRanges)
{
    Console.WriteLine($"Range: {range[0]} - {range[1]}");
}
foreach(var candidate in candidates)
{
    Console.WriteLine($"Candidate: {candidate}");
}

var freshCount = 0;
// foreach candidate, check if in any range
// if yes, increase freshCount

foreach(var candidate in candidates)
{
    foreach(var range in freshRanges)
    {
        if(candidate >= range[0] && candidate <= range[1])
        {
            freshCount++;
            break;
        }
    }
}

Console.WriteLine($"Total fresh candidates: {freshCount}");
