// var input = File.ReadAllLines("day7/task1/test.txt").Select(x => x.ToCharArray()).ToArray();
var input = File.ReadAllLines("day7/task1/puzzle.txt").Select(x => x.ToCharArray()).ToArray();

var split = 0;
for(var row = 1; row < input.Length; row++)
{
    var thisRowSplit = 0;

    for(var col = 0; col < input[0].Length; col++)
    {
        if(ShouldBeBeem(row, col))
        {
            input[row][col] = '|';
        }
        if(input[row][col] == '^' && input[row-1][col] == '|')
        {
            split++;
            thisRowSplit++;
        }
    }
    Console.WriteLine($"{row,2}: {new string(input[row])}: New splits: {thisRowSplit}, Total splits: {split}");
}
Console.WriteLine($"Total splits: {split}");

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
