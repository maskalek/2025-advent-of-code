var lines = File.ReadLines("day1/task1/input2.txt");
var safe = 50;
var count = 0;
foreach(var line in lines)
{
    safe = RotateDial(line, safe);
    if(safe == 0) count++;
    Console.WriteLine($"The dial is rotated {line} to point at {safe}");
}

Console.WriteLine($"The dial pointed at 0 a total of {count} times.");

int RotateDial(string direction, int safe)
{
    int step = direction[0] == 'L' ? -1 : 1;
    int moves = int.Parse(direction.Substring(1));
    return (safe + step * moves + 100) % 100;
}