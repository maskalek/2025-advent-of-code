var lines = File.ReadLines("day5/task2/test.txt");
// var lines = File.ReadLines("day5/task2/puzzle.txt").ToArray();

var isFirstHalf = true;
var freshRanges = new List<long[]>();

foreach(var line in lines)
{
    if(string.IsNullOrWhiteSpace(line))
    {
        break;
    }
    var parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
    var range = new long[] { long.Parse(parts[0]), long.Parse(parts[1]) };

    freshRanges = InsertAndMerge(range, freshRanges);
    Console.WriteLine("After inserting range " + string.Join("-", range) + ":   " + string.Join(", ", freshRanges.Select(r => $"{r[0]}-{r[1]}")));
}

List<long[]> InsertAndMerge(long[] range, List<long[]> freshRanges)
{
    var newFreshRanges = new List<long[]>();
    var inserted = false;
    foreach(var existingRange in freshRanges)
    {
        if(existingRange[1] < range[0] - 1)
        {
            newFreshRanges.Add(existingRange);
        }
        else if(existingRange[0] > range[1] + 1)
        {
            if(!inserted)
            {
                newFreshRanges.Add(range);
                inserted = true;
            }
            newFreshRanges.Add(existingRange);
        }
        else
        {
            // overlap, merge
            range[0] = Math.Min(range[0], existingRange[0]);
            range[1] = Math.Max(range[1], existingRange[1]);
        }
    }
    if(!inserted)
    {
        newFreshRanges.Add(range);
    }
    return newFreshRanges;
}

foreach(var range in freshRanges)
{
    Console.WriteLine($"Range: {range[0]} - {range[1]}");
}

long freshCount = 0;
// foreach candidate, check if in any range
// if yes, increase freshCount

{
    foreach(var range in freshRanges)
    {
        freshCount += range[1] - range[0] + 1;
    }
}

Console.WriteLine($"Total fresh candidates: {freshCount}");
