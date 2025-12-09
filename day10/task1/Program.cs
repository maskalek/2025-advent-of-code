var input = File.ReadAllLines("day10/task1/test.txt");
// var input = File.ReadAllLines("day10/task1/puzzle.txt");

foreach(var line in input)
{
    Console.WriteLine(line);
}

Console.WriteLine($"Total lines: {input.Length}");
