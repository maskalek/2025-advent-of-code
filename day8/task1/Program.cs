// var input = File.ReadAllLines("day8/task1/test.txt");
// var pairs = 10;

var input = File.ReadAllLines("day8/task1/puzzle.txt");
var pairs = 1000;

var points = input.Select((line, index) => Point.Parse(line, index)).ToList();
var pq = new PriorityQueue<(Point, Point), decimal>();

for(var p1 = 0; p1 < points.Count; p1++)
{
    var point1 = points[p1];

    for(var p2 = p1 + 1; p2 < points.Count; p2++)
    {
        var point2 = points[p2];
        if(point1 == point2) continue;

        var distance = point1.Distance(point2);
        pq.Enqueue((point1, point2), -distance);
        if(pq.Count > pairs)
        {
            pq.Dequeue();
        }

    }
}

var union = new Union(points.Count);
while(pq.Count > 0)
{
    var (point1, point2) = pq.Dequeue();
    Console.WriteLine($"Connecting {point1} and {point2}, distance {point1.Distance(point2)}");
    union.Join(point1, point2);
}

var counts = union.GetCounts().OrderByDescending(kv => kv).Take(3).ToList();
Console.WriteLine($"Answer: {counts[0] * counts[1] * counts[2]}");

public record Point(int X, int Y, int Z, int Index)
{
    public static Point Parse(string str, int index)
    {
        var parts = str.Split(',');
        return new Point(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), index);
    }

    public decimal Distance(Point other)
    {
        long dx = this.X - other.X;
        long dy = this.Y - other.Y;
        long dz = this.Z - other.Z;
        return (decimal)Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    public override string ToString()
    {
        return $"({X},{Y},{Z})";
    }
}

public class Union
{
    private int[] union;
    private int[] count;

    public Union(int count)
    {
        this.union = new int[count];
        this.count = new int[count];
        for(var i = 0; i < count; i++)
        {
            this.union[i] = i;
            this.count[i] = 1;
        }    
    }
    public int Join(Point p1, Point p2)
    {
        var id1 = GetId(p1);
        var id2 = GetId(p2);

        if(id1 == id2) return id1;

        if(count[id1] < count[id2])
        {
            union[id1] = id2;
            count[id2] += count[id1];
            return id2;
        }
        else
        {
            union[id2] = id1;
            count[id1] += count[id2];
            return id1;
        }   
    }

    public int GetId(Point p)
    {
        var id = p.Index;
        while(union[id] != id)
        {
            id = union[id];
        }
        return id;
    }

    public int[] GetCounts()
    {
        var dict = new Dictionary<int, int>();
        for(var i = 0; i < union.Length; i++)
        {
            var id = GetId(new Point(0,0,0,i));
            if(!dict.ContainsKey(id))
            {
                dict[id] = 0;
            }
            dict[id] = count[id];
        }
        return dict.Values.ToArray();
    }

}


// 891 That's not the right answer; your answer is too low
// 4368 That's not the right answer; your answer is too low