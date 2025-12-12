var textInput = File.ReadAllLines("day11/task1/test.txt");
// var textInput = File.ReadAllLines("day11/task1/puzzle.txt");

var input = new Dictionary<string, int>();
var output = new Dictionary<string, List<string>>();
var total = new Dictionary<string, int>();

foreach(var line in textInput)
{
    // aaa: you hhh
    var parts = line.Split(':');
    var from = parts[0];
    var tos = parts[1].Trim().Split(' ').ToArray();
    if(!output.ContainsKey(from))
    {
        output[from] = new();
    }
    if(!input.ContainsKey(from))
    {
        input[from] = 0;
        total[from] = 0;
    }

    foreach(var to in tos)
    {
        if(!input.ContainsKey(to))
        {
            input[to] = 0;
            total[to] = 0;
        }
        input[to]++;
        output[from].Add(to);
    }
}

total["you"] = 1;

while(output.Count > 0)
{
    var from = output.Keys.First(x => input[x] == 0);
    var tos = output[from];
    output.Remove(from);

    foreach(var to in tos)
    {
        total[to] += total[from];
        input[to]--;
    }
}

Console.WriteLine($"Total paths: {total["out"]}");
