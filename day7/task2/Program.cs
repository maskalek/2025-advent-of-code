// var input = File.ReadAllLines("day7/task2/test.txt").Select(x => x.ToCharArray()).ToArray();
var input = File.ReadAllLines("day7/task2/puzzle.txt").Select(x => x.ToCharArray()).ToArray();

var curr = new long[input[0].Length];
var next = new long[input[0].Length];
for(var col = 0; col < input[0].Length; col++)
{
    if(input[0][col] == 'S')
    {
        curr[col] = 1;
    }
}
for(var row = 1; row < input.Length; row++)
{
    for(var col = 0; col < input[0].Length; col++)
    {

        if(input[row][col] == '^')
        {
            next[col - 1] += curr[col];
            next[col + 1] += curr[col];
        }
        else
        {
            next[col] += curr[col];
        }
        if(ShouldBeBeem(row, col))
        {
            input[row][col] = '|';
        }
    }
    // var timeline = new string(curr.Select(x => x == 0 ? '.' : (char)(x + '0')).ToArray());
    // Console.WriteLine($"{row,2}: {new string(input[row])}: Timelines: {timeline}, Total timelines: {next.Sum()}");

    curr = next;
    next = new long[input[0].Length];

}
Console.WriteLine($"Total timelines: {curr.Sum()}");

bool ShouldBeBeem(int row, int col)
{
    if(input[row][col] == '^')
    {
        return false;
    }
    if(input[row-1][col] == 'S' || input[row-1][col] == '|')
    {
        return true;
    }
    if(col > 0 && input[row-1][col-1] == '^')
    {
        return true;
    }
    if(col < input[row].Length - 1 && input[row-1][col+1] == '^')
    {
        return true;
    }

    return false;

}
