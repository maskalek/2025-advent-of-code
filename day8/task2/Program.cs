// var input = File.ReadAllLines("day8/task2/test.txt");
// var pairs = 10;

var input = File.ReadAllLines("day8/task2/puzzle.txt");
// var pairs = 1000;

var points = input.Select((line, index) => Point.Parse(line, index)).ToList();
var list = new List<(Point Point1, Point Point2, long DistanceSquared)>();

for(var p1 = 0; p1 < points.Count; p1++)
{
    var point1 = points[p1];

    for(var p2 = p1 + 1; p2 < points.Count; p2++)
    {
        var point2 = points[p2];
        if(point1 == point2) continue;

        var distanceSquared = point1.DistanceSquared(point2);
        list.Add((point1, point2, distanceSquared));

    }
}

list = list.OrderBy(x => x.DistanceSquared).ToList();

var union = new Union(points.Count);
var index = 0;
while(union.GetComponentSizes().Count != 1)
{
    var (point1, point2) = (list[index].Point1, list[index].Point2);
    Console.WriteLine($"Connecting {point1} and {point2}, distance {point1.DistanceSquared(point2)}");
    union.Join(point1, point2);

    index++;
}

var last = list[index - 1];
Console.WriteLine($"Answer: {(long)last.Point1.X * (long)last.Point2.X}");

public record Point(int X, int Y, int Z, int Index)
{
    public static Point Parse(string str, int index)
    {
        var parts = str.Split(',');
        return new Point(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), index);
    }

    public long DistanceSquared(Point other)
    {
        long dx = this.X - other.X;
        long dy = this.Y - other.Y;
        long dz = this.Z - other.Z;
        return dx * dx + dy * dy + dz * dz;
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
        var id1 = Find(p1.Index);
        var id2 = Find(p2.Index);

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

    public int Find(int x)
    {
        if (union[x] == x)
            return x;

        union[x] = Find(union[x]);
        return union[x];
    }

    public List<int> GetComponentSizes()
    {
        {
            var seen = new HashSet<int>();
            var result = new List<int>();

            for (int i = 0; i < union.Length; i++)
            {
                int root = Find(i);
                if (seen.Add(root))
                {
                    result.Add(count[root]);
                }
            }

            return result;
        }
    }

}


// 670023973 That's not the right answer; your answer is too low