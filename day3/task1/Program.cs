var lines = File.ReadLines("day3/task1/puzzle.txt");
var totalJoltage = 0;
foreach(var line in lines)
{
    var maxJoltage = GetMaximumJoltage(line);
    totalJoltage += maxJoltage;
    Console.WriteLine($"The adapter with rating {line} can produce a maximum joltage of {maxJoltage}");
}

Console.WriteLine($"The total maximum joltage is {totalJoltage}");

int GetMaximumJoltage(string joltageRatings)
{
    var maxJoltage = 0;
    var currentMax = joltageRatings[0] - '0';
    for(int i = 1; i < joltageRatings.Length; i++)
    {
        var rating = joltageRatings[i] - '0';
        maxJoltage = Math.Max(maxJoltage, 10 * currentMax + rating);
        currentMax = Math.Max(rating, currentMax);
    }
    return maxJoltage;
}