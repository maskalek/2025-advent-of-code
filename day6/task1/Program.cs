// var input = File.ReadAllLines("day6/task1/test.txt");
var input = File.ReadAllLines("day6/task1/puzzle.txt");

var grandTotal = 0L;
var problems = input.Take(input.Length - 1).Select(ParseProblem).ToArray();
var operations = input.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

for(var col = 0; col < operations.Length; col++)
{
    var colScore = GetColumnScore(col);
    grandTotal += colScore;
    Console.WriteLine($"Column {col} score: {colScore}");
}

Console.WriteLine($"Grand total: {grandTotal}");

long[] ParseProblem(string problem)
{
    return problem.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
}

long GetColumnScore(int col)
{
    var total = 0L;
    var op = operations[col];
    foreach(var problem in problems)
    {
        var val = problem[col];
        if(total == 0)
        {
            total = val;
            continue;    
        }

        if(op == "+")
        {
            total += val;
        }
        else if(op == "*")
        {
            total *= val;
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

