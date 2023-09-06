namespace AOC2023.Day8;

public class TreeHousePlanner
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day8\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    private static List<List<Tree>> forest = new List<List<Tree>>();

    private enum Direction
    {
        North,
        South,
        East,
        West
    }

    private class Tree
    {
        public int Height { get; init; }
        public bool North { get; set; } //Top
        public bool South { get; set; } //Bottom
        public bool East { get; set; } //Left
        public bool West { get; set; } //Right
    }

    private List<List<Tree>> ViewChecker(int row, int column, Direction direction, List<List<Tree>> requestedForest)
    {
        var viewingAngle = ArboristViewingAngle(direction, requestedForest);
        for (var i = row; i < viewingAngle.Count; i++)
        {
            var tallestTree = 0;
            for (var j = column; j < viewingAngle[i].Count; j++)
            {
                var tree = viewingAngle[i][j];
                switch (direction)
                {
                    case Direction.North:
                        if (tallestTree < tree.Height || i == 0)
                            tree.North = true;
                        break;
                    case Direction.South:
                        if (tallestTree < tree.Height || i == 0)
                            tree.South = true;
                        break;
                    case Direction.East:
                        if (tallestTree < tree.Height || i == 0)
                            tree.East = true;
                        break;
                    case Direction.West:
                        if (tallestTree < tree.Height || i == 0)
                            tree.West = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }

                if (tree.Height > tallestTree)
                {
                    tallestTree = tree.Height;
                }
            }
        }
        return viewingAngle;
    }
    
    private List<List<Tree>> ArboristViewingAngle(Direction rotateAmount, List<List<Tree>> newForest) //array rotator
    {
        for (var i = 0; i < Array.IndexOf(Enum.GetValues(rotateAmount.GetType()), rotateAmount); i++)
        {
            newForest = newForest.SelectMany(a => a.Select((b, pos) => new {a, b, i = pos}))
                .GroupBy(x => x.i % newForest.Count)
                .Select(x => x.Select(y => y.b).Reverse().ToList())
                .ToList();
        }
        return newForest;
    }

    private List<List<Tree>> CreateForestMap()
    {
        var createdForest = new List<List<Tree>>();
        for (var index = 0; index < lines.Length; index++)
        {
            createdForest.Add(new List<Tree>());
            foreach (var treeHeight in lines[index].Where(treeHeight => treeHeight is >= '0' and <= '9'))
            {
                createdForest[index].Add(new Tree {Height = int.Parse(treeHeight.ToString())});
            }
        }
        return createdForest;
    }

    private List<List<Tree>> OverallVisibilityChecker(int row, int column, List<List<Tree>> forest)
    {
        List<List<Tree>> forestNorth = ViewChecker(row, column, Direction.North, forest);
        List<List<Tree>> forestSouth = ViewChecker(row, column, Direction.South, forest);
        List<List<Tree>> forestEast = ViewChecker(row, column, Direction.East, forest);
        List<List<Tree>> forestWest = ViewChecker(row, column, Direction.West, forest);
        return MergeForest(forestNorth, forestSouth, forestEast, forestWest);
    }
    
    private List<List<Tree>> MergeForest(params List<List<Tree>>[] forests)
    {
        if (forests.Length == 0) 
            return new List<List<Tree>>();
        
        List<List<Tree>> mergedForsts = new List<List<Tree>>();
        foreach (var forest in forests)
        {
            for (var i = 0; i < forest.Count; i++)
            {
                for (var j = 0; j < forest[i].Count; j++)
                {
                    var tree = forest[i][j];
                    if (tree.North || tree.South || tree.East || tree.West)
                    {
                        if (mergedForsts.Count <= i)
                            mergedForsts.Add(new List<Tree>());
                        
                        mergedForsts[i].Add(tree);
                    }
                }
            }
        }

        return mergedForsts;
    }
    
    public void Part1()
    {
        forest = CreateForestMap();
        var total = OverallVisibilityChecker(0, 0, forest).Sum(treeRow => treeRow.Count(tree => tree.North || tree.South || tree.East || tree.West));
        Console.WriteLine($"Total Part 1 is {total}");
    }

    //Check forwards backwards up and down for each tree and count the distance they can see from that tree
    public void Part2()
    {
        /*CreateForestMap();
        for (var i = 0; i < forest.Count; i++)
        {
            for (var j = 0; i < forest[i].Count; i++)
            {
                OverallVisibilityChecker(i, j);
            }
        }*/
    }
}