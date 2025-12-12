// var textInput = File.ReadAllLines("day11/task2/test.txt");
var textInput = File.ReadAllLines("day11/task2/puzzle.txt");

var input = new Dictionary<string, int>();
var output = new Dictionary<string, List<string>>();

var nothing = 0;
var dac = 1;
var fft = 2;
var dacfft = 3;
var total = new Dictionary<string, long>[4];
total[nothing] = new();
total[dac] = new();
total[fft] = new();
total[dacfft] = new();

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

        total[nothing][from] = 0;
        total[dac][from] = 0;
        total[fft][from] = 0;
        total[dacfft][from] = 0;
    }

    foreach(var to in tos)
    {
        if(!input.ContainsKey(to))
        {
            input[to] = 0;
        
            total[nothing][to] = 0;
            total[dac][to] = 0;
            total[fft][to] = 0;
            total[dacfft][to] = 0;
        }
        input[to]++;
        output[from].Add(to);
    }
}

total[nothing]["svr"] = 1;
total[dac]["svr"] = 0;
total[fft]["svr"] = 0;
total[dacfft]["svr"] = 0;

while(output.Count > 0)
{
    var from = output.Keys.First(x => input[x] == 0);
    var tos = output[from];
    output.Remove(from);

    foreach(var to in tos)
    {
        input[to]--;

        if(to == "dac")
        {
            // total[nothing][to] += total[nothing][from];
            total[dac][to] += total[nothing][from];
            total[dacfft][to] += total[fft][from];
        }
        else if(to == "fft")
        {
            // total[nothing][to] += total[nothing][from];
            total[fft][to] += total[nothing][from];
            total[dacfft][to] += total[dac][from];
        }
        else
        {
            total[nothing][to] += total[nothing][from];
            total[dac][to] += total[dac][from];
            total[fft][to] += total[fft][from];
            total[dacfft][to] += total[dacfft][from];
        }


    }
}

Console.WriteLine($"Total paths: {total[dacfft]["out"]}");
