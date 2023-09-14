using System.Numerics;

namespace AOC2023.Day9;

public class RopePhysics
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day9\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);

    private List<Tuple<char, int>> GetInstructions()
    {
        var instructions = new List<Tuple<char, int>>();
        foreach (var line in lines)
        {
            instructions.Add(new Tuple<char, int>(Char.Parse(line.Split(" ")[0]), Int32.Parse(line.Split(" ")[1])));
        }
        return instructions;
    }
    
    private Dictionary<int, List<Vector2>> SimulateRope(int ropeLength)
    {
        var rope = CreateRope(ropeLength);
        var currentPos = new Vector2(0, 0);

        foreach (var ropePosition in rope)
            ropePosition.Value.Add(new Vector2(currentPos.X, currentPos.Y));
        
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
                            var newPosition = currentRopePiece + direction * (magnitude - 1);
                            rope[i].Add(new Vector2((int)newPosition.X, (int)newPosition.Y));
                        }
                        else
                        {
                            if (difference.X > 0 && difference.Y > 0)
                                rope[i].Add(northEast);
                            
                            else if (difference.X > 0 && difference.Y < 0)
                                rope[i].Add(southEast);
                            
                            else if (difference.X < 0 && difference.Y < 0)
                                rope[i].Add(southWest);
                            
                            else if (difference.X < 0 && difference.Y > 0)
                                rope[i].Add(northWest);
                        }
                    }
                }
            }
        }
        return rope;
    }
    
    private Dictionary<int, List<Vector2>> CreateRope(int ropeLength)
    {
        var rope = new Dictionary<int, List<Vector2>>();
        for (var i = 0; i < ropeLength; i++)
        {
            rope.Add(i, new List<Vector2>());
        }
        return rope;
    }
    
    public void Part1Part2()
    {
        Console.WriteLine($"Part 1 Tail Count {SimulateRope(2).Last().Value.Distinct().Count()}");
        Console.WriteLine($"Part 2 Tail Count {SimulateRope(10).Last().Value.Distinct().Count()}");

    }
}