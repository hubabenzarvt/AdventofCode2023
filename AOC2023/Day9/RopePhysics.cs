using System.Numerics;

namespace AOC2023.Day9;

public class RopePhysics
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day9\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);

    public List<Tuple<char, int>> GetInstructions()
    {
        var instructions = new List<Tuple<char, int>>();
        foreach (var line in lines)
        {
            var split = line.Split(" ");
            instructions.Add(new Tuple<char, int>(Char.Parse(split[0]), Int32.Parse(split[1])));
        }
        return instructions;
    }

    public Vector2 GetMapSize()
    {
        var instructions = GetInstructions();
        var headTracker = new List<Vector2>();
        var currentX = 0;
        var currentY = 0;

        for (var i = 0; i < instructions.Count; i++)
        {
            var instruction = instructions[i];
            switch (instruction.Item1)
            {
                case 'R':
                    currentX += instruction.Item2;
                    break;
                case 'L':
                    currentX -= instruction.Item2;
                    break;
                case 'U':
                    currentY += instruction.Item2;
                    break;
                case 'D':
                    currentY -= instruction.Item2;
                    break;
            }
            headTracker.Add(new Vector2(currentX, currentY));
        }
        //find extremes of the head's path
        var maxX = headTracker.Max(x => x.X);
        var maxY = headTracker.Max(x => x.Y);
        var minX = headTracker.Min(x => x.X);
        var minY = headTracker.Min(x => x.Y);
        
        //find map size
        var mapSizeX = maxX - minX + 1;
        var mapSizeY = maxY - minY + 1;
        
        return new Vector2(mapSizeX, mapSizeY);
    }

    public void SimulateHead(int ropeLength)
    {
        //create lists to track head and tail
        var headTracker =  new List<Vector2>();
        var tailTracker =  new List<Vector2>();
        
        //find map size
        var mapSize = GetMapSize();
        
        //Find center of map
        var currentX = 0;
        var currentY = 0;
        
        //Add initial position
        tailTracker.Add(new Vector2(currentX, currentY));
        headTracker.Add(new Vector2(currentX, currentY));
        
        foreach (var instruction in GetInstructions())
        {
            Console.WriteLine();
            Console.WriteLine($"{instruction.Item1}|{instruction.Item2}");
            
            var individualSteps = instruction.Item2;
            for (var step = 0; step < individualSteps; step++)
            {
                switch (instruction.Item1)
                {
                    case 'R':
                        currentY++;
                        break;
                    case 'L':
                        currentY--;
                        break;
                    case 'U':
                        currentX++;
                        break;
                    case 'D':
                        currentX--;
                        break;
                }
                headTracker.Add(new Vector2(currentX, currentY));
                //Update window data
                var distance = Vector2.Distance(headTracker.Last(), tailTracker.Last());
                Console.WriteLine($"Distance {distance}");

                if (distance > ropeLength+1.5)
                {
                    tailTracker.Add(headTracker[^2]);
                }
                //VisualiseData(new Vector2(0, 0), headTracker.Last(), tailTracker, mapSize);
            }
        }

        Console.WriteLine($"Starting position: {currentX},{currentY}");
        Console.WriteLine($"Head Ending position: {headTracker.Last().X},{headTracker.Last().Y}");
        Console.WriteLine($"Tail Ending Position {tailTracker.Last().X},{tailTracker.Last().Y}");
        Console.WriteLine($"Tail Count {tailTracker.Distinct().Count()}");
    }

    //Create a map of the rope's path
    public void VisualiseData(Vector2 beginning, Vector2 headLast, List<Vector2> tailPath, Vector2 mapSize)
    {
        var map = new char[(int) mapSize.X, (int) (mapSize.Y+1)];
        var tailPathDistinct = tailPath.Distinct().ToList();
        
        foreach (var tailPos in tailPathDistinct)
        {
            map[(int) tailPos.X, (int) tailPos.Y] = '#';
        }
        
        map[(int) tailPath.Last().X, (int) tailPath.Last().Y] = 'T';
        map[(int) beginning.X, (int) beginning.Y] = 's';
        if (tailPathDistinct.Contains(headLast))
            map[(int) headLast.X, (int) headLast.Y] = 'Æ';
        
        else
            map[(int) headLast.X, (int) headLast.Y] = 'H';
        
        for (var i = map.GetLength(0) - 1; i >= 0; i--)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j] == '\0' ? '.' : map[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void Part1()
    {
        SimulateHead(0);
    }
}