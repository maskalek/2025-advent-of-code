// var input = File.ReadAllLines("day6/task2/test.txt");
var input = File.ReadAllLines("day6/task2/puzzle.txt");

var grandTotal = 0L;
var operations = input.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

// var problems = input.Take(input.Length - 1).Select(ParseProblem).ToArray();
var problems = new List<List<long>>();
foreach(var operation in operations)
{
    problems.Add(new List<long>());
}

var problemIndex = 0;
var numberIndex = 0;
for(var col = 0; col < input[0].Length; col++)
{
    if(IsEmptyColumn(col))
    {
        problemIndex++;
        numberIndex = 0;
        continue;
    }

    for(var row = 0; row < input.Length - 1; row++)
    {
        if(row == 0)
        {
            problems[problemIndex].Add(0);
        }
        if(input[row][col] == ' ')
        {
            continue;
        }
        var val = long.Parse(input[row][col].ToString());
        problems[problemIndex][numberIndex] = problems[problemIndex][numberIndex] * 10 + val;
    }

    numberIndex++;
}

foreach(var problem in problems)
{
    Console.WriteLine(string.Join(' ', problem));
}

for(var col = 0; col < operations.Length; col++)
{
    var colScore = GetColumnScore(col);
    grandTotal += colScore;
    Console.WriteLine($"Column {col} score: {colScore}");
}

Console.WriteLine($"Grand total: {grandTotal}");

bool IsEmptyColumn(int col)
{
    for(var row = 0; row < input.Length - 1; row++)
    {
        if(input[row][col] != ' ')
        {
            return false;
        }
    }
    return true;
}

long GetColumnScore(int col)
{
    var total = 0L;
    var op = operations[col];
    var problem = problems[col];
    {
        foreach(var number in problem)
        {
            if(total == 0)
            {
                total = number;
                continue;
            }
            if(op == "+")
            {
                total += number;
            }
            else if(op == "*")
            {
                total *= number;
            }
        }
        // else if(op == '-')
        // {
        //     total -= val;
        // }
        // else if(op == '/')
        // {
        //     total /= val;
        // }
    }
    return total;
}

