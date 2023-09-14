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
    
    private Dictionary<int, List<Vector2>> SimulateRope(int ropeLength)
    {
        //GetMapSize();
        var rope = CreateRope(ropeLength);
        var currentPos = new Vector2(0, 0);

        foreach (var ropePosition in rope)
        {
            ropePosition.Value.Add(new Vector2(currentPos.X, currentPos.Y));
        }
        
        foreach (var (stepDir, stepCount) in GetInstructions())
        {
            for (var step = 0; step < stepCount; step++)
            {
                switch (stepDir)
                {
                    case 'R':
                        currentPos.Y++;
                        break;
                    case 'L':
                        currentPos.Y--;
                        break;
                    case 'U':
                        currentPos.X++;
                        break;
                    case 'D':
                        currentPos.X--;
                        break;
                }
                rope[0].Add(new Vector2(currentPos.X, currentPos.Y));

                for (var i = 1; i < rope.Count; i++)
                {
                    var currentRopePiece = rope[i].Last();
                    var previousRopePiece = rope[i - 1].Last();
                    var magnitude = Vector2.Distance(previousRopePiece, currentRopePiece);
                    var radius = 1;
                    if (magnitude >= 2)
                    {
                        var northEast = new Vector2(currentRopePiece.X + 1, currentRopePiece.Y + 1);
                        var southEast = new Vector2(currentRopePiece.X + 1, currentRopePiece.Y - 1);
                        var southWest = new Vector2(currentRopePiece.X - 1, currentRopePiece.Y - 1);
                        var northWest = new Vector2(currentRopePiece.X - 1, currentRopePiece.Y + 1);
                        var difference = previousRopePiece - currentRopePiece;

                        if ((int)previousRopePiece.X == (int)currentRopePiece.X || (int)previousRopePiece.Y == (int)currentRopePiece.Y)
                        {
                            var direction = Vector2.Normalize(difference);
                            var newPosition = currentRopePiece + direction * (magnitude - radius);
                            rope[i].Add(new Vector2((int)newPosition.X, (int)newPosition.Y));
                        }
                        else
                        {
                            if (difference.X > 0 && difference.Y > 0)
                            {
                                rope[i].Add(northEast);
                            }
                            else if (difference.X > 0 && difference.Y < 0)
                            {
                                rope[i].Add(southEast);
                            }
                            else if (difference.X < 0 && difference.Y < 0)
                            {
                                rope[i].Add(southWest);
                            }
                            else if (difference.X < 0 && difference.Y > 0)
                            {
                                rope[i].Add(northWest);
                            }
                        }
                    }
                }
                //VisualiseData(new Vector2(0, 0), rope);
            }
        }
        return rope;
    }
    //public List<Vector2> headPosition = new List<Vector2>();

    /*public void GetMapSize()
    {
        var instructions = GetInstructions();
        var currentPos = new Vector2(0, 0);

        foreach (var instruction in instructions)
        {
            switch (instruction.Item1)
            {
                case 'R':
                    currentPos.X += instruction.Item2;
                    break;
                case 'L':
                    currentPos.X -= instruction.Item2;
                    break;
                case 'U':
                    currentPos.Y += instruction.Item2;
                    break;
                case 'D':
                    currentPos.Y -= instruction.Item2;
                    break;
            }
            headPosition.Add(new Vector2(currentPos.X, currentPos.Y));
        }
        //find extremes of the head's path
        var maxX = headPosition.Max(x => x.X);
        var maxY = headPosition.Max(x => x.Y);
        var minX = headPosition.Min(x => x.X);
        var minY = headPosition.Min(x => x.Y);
        
        //find map size
        var mapSizeX = maxX - minX + 1;
        var mapSizeY = maxY - minY + 1;
    }*/

    //Create a map of the rope's path
    /*public void VisualiseData(Vector2 beginning, Dictionary<int, List<Vector2>> rope)
    {
        var map = new char[(int) mapSize.X, (int) (mapSize.Y+1)];
        map[(int) beginning.X, (int) beginning.Y] = 's';
        for (var i = rope.Count - 1; i >= 0; i--)
        {
            if (i == 0)
                map[(int) rope[i].Last().X, (int) rope[i].Last().Y] = 'H';

            else
                map[(int) rope[i].Last().X, (int) rope[i].Last().Y] = rope.Keys.ElementAt(i).ToString()[0];
        }

        Console.Clear();
        for (var i = map.GetLength(0) - 1; i >= 0; i--)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j] == '\0' ? '.' : map[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }*/

    private Dictionary<int, List<Vector2>> CreateRope(int ropeLength)
    {
        var rope = new Dictionary<int, List<Vector2>>();
        for (var i = 0; i < ropeLength; i++)
        {
            rope.Add(i, new List<Vector2>());
        }

        return rope;
    }
    
    public void Part1()
    {
        Console.WriteLine($"Part 1 Tail Count {SimulateRope(2).Last().Value.Distinct().Count()}");
    }
    
    public void Part2()
    {
        Console.WriteLine($"Part 2 Tail Count {SimulateRope(10).Last().Value.Distinct().Count()}");
    }
}