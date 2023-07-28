namespace AOC2023.Day8;

public class TreeHousePlanner
{
    private static string inputPath = File.OpenText("D:\\WORK\\aoc\\AdventofCode2023\\AOC2023\\Day8\\example.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    private static List<List<Tree>> forest = new List<List<Tree>>();

    public enum Direction
    {
        North = 1,
        South = 2,
        East = 3,
        West = 4
    }
    
    public class Tree
    {
        public int height { get; set; }
        public bool north { get; set; } //T
        public bool south { get; set; } //B
        public bool east { get; set; } //L
        public bool west { get; set; } //R
    }

    //A method that rotates the tree 2d array by a given angle
    public void ViewChecker(Direction driection)
    {
        var tallestTree = 0;
        var viewingAngle = ArboristViewingAngle(driection, forest);
        foreach (var treeRow in viewingAngle)
        {
            for (var i = 0; i < treeRow.Count; i++)
            {
                var tree = treeRow[i];
                //If n-1 doesn’t exist set n to true in visibilityList
                if (tree.height > tallestTree)
                {
                    tallestTree = tree.height;
                }
                
                if (i == 0)
                {
                    switch (driection)
                    {
                        case Direction.North:
                            treeRow[i].north = true;
                            break;
                        case Direction.South:
                            treeRow[i].south = true;
                            break;
                        case Direction.East:
                            treeRow[i].east = true;
                            break;
                        case Direction.West:
                            treeRow[i].west = true;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(driection), driection, null);
                    }
                }
                else
                {
                    switch (driection)
                    {
                        case Direction.North:
                            treeRow[i].north = tallestTree < tree.height;
                            break;
                        case Direction.South:
                            treeRow[i].south = tallestTree < tree.height;
                            break;
                        case Direction.East:
                            treeRow[i].east = tallestTree < tree.height;
                            break;
                        case Direction.West:
                            treeRow[i].west = tallestTree < tree.height;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(driection), driection, null);
                    }
                }
            }
        }
    }
    
    private List<List<Tree>> ArboristViewingAngle(Direction rotateAmount, List<List<Tree>> tempForest) //array rotator
    {
        for (var i = 0; i < Array.IndexOf(Enum.GetValues(rotateAmount.GetType()), rotateAmount); i++)
        {
            tempForest = tempForest.SelectMany(a => a.Select((b, pos) => new {a, b, i = pos}))
                .GroupBy(x => x.i % tempForest.Count)
                .Select(x => x.Select(y => y.b).Reverse().ToList())
                .ToList();
        }
        return tempForest;
    }
    
    public void Part1()
    {
        for (var index = 0; index < lines.Length; index++)
        {
            forest.Add(new List<Tree>());
            foreach (var treeHeight in lines[index].Where(treeHeight => treeHeight is >= '0' and <= '9'))
            {
                forest[index].Add(new Tree {height = treeHeight-'0'});
            }
        }
        ViewChecker(Direction.North);
        ViewChecker(Direction.South);
        ViewChecker(Direction.East);
        ViewChecker(Direction.West);
        
    }

    public void Part2()
    {
        
    }
}