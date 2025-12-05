// var lines = File.ReadLines("day4/task1/test.txt").ToArray();
var lines = File.ReadLines("day4/task1/puzzle.txt").ToArray();
var totalForklifts = 0;
for(var r = 0; r < lines.Length; r++)
{
    var liftsOnThisLine = 0;
    for(var c = 0; c < lines[r].Length; c++)
    {
        if(IsValid(r, c))
        {
            liftsOnThisLine++;
        }
    }
    totalForklifts += liftsOnThisLine;
    Console.WriteLine($"Line {lines[r]}: {liftsOnThisLine} accessible forklifts");
}

Console.WriteLine($"Total accessible forklifts: {totalForklifts}");

bool IsValid(int i, int j)
{
    if(lines[i][j] != '@')
    {
        return false;
    }
    var coordinates = new (int, int)[]
    {
        (i-1, j), // up
        (i+1, j), // down
        (i, j-1), // left
        (i, j+1),  // right
        (i-1, j-1), // up-left
        (i-1, j+1), // up-right
        (i+1, j-1), // down-left
        (i+1, j+1)  // down-right
    };
    var count = 0;
    foreach(var (x, y) in coordinates)
    {
        if(x < 0 || y < 0 || x >= lines.Length || y >= lines[0].Length)
        {
            continue;
        }
        if(lines[x][y] == '@')
        {
            count++;
        }
    }
    return count < 4;
}
