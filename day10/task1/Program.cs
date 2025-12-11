// var input = File.ReadAllLines("day10/task1/test.txt");
var input = File.ReadAllLines("day10/task1/puzzle.txt");

var totalPresses = 0;


foreach(var line in input)
{
    var machine = Parse(line);

    var presses = GetPressesToTurnOffAllLights(machine);
    totalPresses += presses;
    Console.WriteLine($"Machine: {machine}, Presses: {presses}");
}

Console.WriteLine($"Total presses: {totalPresses}");


Machine Parse(string line)
{
    // [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
    // [.##.] - lights
    // (3) (1,3) (2) (2,3) (0,2) (0,1) - buttons
    // {3,5,4,7} - ignore

    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var lightsPart = parts[0].Trim('[', ']');
    var lights = lightsPart.Select(c => new Light(c == '#')).ToArray();
    var buttonsParts = parts.Skip(1).Where(p => p.StartsWith('(') && p.EndsWith(')')).ToArray();
    var buttons = new Button[buttonsParts.Length];
    for(var i = 0; i < buttonsParts.Length; i++)
    {
        var buttonPart = buttonsParts[i].Trim('(', ')');
        var lightIndices = buttonPart.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.Parse(s))
            .ToArray();
        buttons[i] = new Button { LightIndices = lightIndices };
    }

    return new Machine
    {
        Lights = lights,
        Buttons = buttons
    };
}

int GetPressesToTurnOffAllLights(Machine machine)
{
    var queue = new Queue<(Machine machine, int presses)>();
    var visited = new HashSet<string>();

    queue.Enqueue((machine, 0));
    visited.Add(machine.ToString());

    while(queue.Count > 0)
    {
        var (currentMachine, presses) = queue.Dequeue();

        if(currentMachine.IsAllLightsOff())
        {
            return presses;
        }

        for(var i = 0; i < currentMachine.Buttons.Length; i++)
        {
            var nextMachine = currentMachine.PressButton(i);
            var stateString = nextMachine.ToString();
            if(!visited.Contains(stateString))
            {
                visited.Add(stateString);
                queue.Enqueue((nextMachine, presses + 1));
            }
        }
    }

    return -1; // not found
}

record struct Machine()
{
    public Button[] Buttons = Array.Empty<Button>();
    public Light[] Lights = Array.Empty<Light>();

    public override string ToString()
    {
        return new string(Lights.Select(l => l.IsOn ? '#' : '.').ToArray());
    }

    public Machine PressButton(int buttonIndex)
    {
        var button = Buttons[buttonIndex];
        var newLights = (Light[])Lights.Clone();

        foreach (var lightIndex in button.LightIndices)
        {
            newLights[lightIndex] = newLights[lightIndex].Toggle();
        }

        return this with { Lights = newLights };

    }

    public bool IsAllLightsOff()
    {
        return Lights.All(l => !l.IsOn);
    }

    // public Machine Clone()
    // {
    //     return new Machine
    //     {
    //         Buttons = Buttons,
    //         Lights = (Light[])Lights.Clone()
    //     };
    // }
}

record Button()
{
    public int[] LightIndices = Array.Empty<int>();
}

record Light(bool IsOn)
{
    public Light Toggle() => new Light(!IsOn);
}