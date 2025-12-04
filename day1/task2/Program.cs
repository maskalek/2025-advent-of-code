var lines = File.ReadLines("day1/task2/input2.txt");
var safe = 50;
var zeros = 0;
foreach(var line in lines)
{
    (safe, var count) = RotateDial(line, safe);
    zeros += count;
    Console.WriteLine($"The dial is rotated {line} to point at {safe}, it points at 0 - {count} times.");
}

Console.WriteLine($"The dial pointed at 0 a total of {zeros} times.");

(int safe, int count) RotateDial(string direction, int safe)
{
    int step = direction[0] == 'L' ? -1 : 1;
    int moves = int.Parse(direction.Substring(1));
    var count = 0;
    while(moves-- > 0)
    {
        safe += step;
        if(safe < 0) safe = 99;
        if(safe > 99) safe = 0;
        
        if(safe == 0) count++;
    }
    return (safe, count);
    
}