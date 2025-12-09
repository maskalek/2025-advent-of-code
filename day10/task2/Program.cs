var input = File.ReadAllLines("day10/task2/test.txt");
// var input = File.ReadAllLines("day10/task2/puzzle.txt");

foreach(var line in input)
{
    Console.WriteLine(line);
}

Console.WriteLine($"Total lines: {input.Length}");
