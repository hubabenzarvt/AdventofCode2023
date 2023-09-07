using System.Numerics;

namespace AOC2023.Day8;

public class TreeHousePlanner
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day8\\example.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    private List<List<Tree>> forest = new List<List<Tree>>();

    private class Tree
    {
        public int Height { get; init; }
        
        public int NorthTotal { get; set; }
        public int EastTotal { get; set; }
        public int SouthTotal { get; set; }
        public int WestTotal { get; set; }
    }
    
    private void CheckNorth(int row, int column)
    {
        var tallestTree = -1;
        for (var i = row; i >= 0; i--)
        {
            var tree = forest[i][column];
            if (tallestTree < tree.Height)
            {
                tree.NorthTotal++;
            }
            if (tree.Height > tallestTree)
            {
                tallestTree = tree.Height;
            }
        }
    }
    
    private void CheckEast(int row, int column)
    {
        var tallestTree = -1;
        for (var i = column; i < forest[row].Count; i++)
        {
            var tree = forest[row][i];
            if (tallestTree < tree.Height)
            {
                tree.EastTotal++;
            }
            if (tree.Height > tallestTree)
            {
                tallestTree = tree.Height;
            }
        }
    }
    
    private void CheckSouth(int row, int column)
    {
        var tallestTree = -1;
        for (var i = row; i < forest.Count; i++)
        {
            var tree = forest[i][column];
            if (tallestTree < tree.Height)
            {
                tree.SouthTotal++;
            }
            if (tree.Height > tallestTree)
            {
                tallestTree = tree.Height;
            }
        }
    }
    
    private void CheckWest(int row, int column)
    {
        var tallestTree = -1;
        for (var i = column; i >= 0; i--)
        {
            var tree = forest[row][i];
            if (tallestTree < tree.Height)
            {
                tree.WestTotal++;
            }
            if (tree.Height > tallestTree)
            {
                tallestTree = tree.Height;
            }
        }
    }

    private void CreateForestMap()
    {
        forest.Clear();
        for (var index = 0; index < lines.Length; index++)
        {
            forest.Add(new List<Tree>());
            foreach (var treeHeight in lines[index].Where(treeHeight => treeHeight is >= '0' and <= '9'))
            {
                forest[index].Add(new Tree {Height = int.Parse(treeHeight.ToString())});
            }
        }
    }
    
    private void CheckSurroundings(int row, int column)
    {
        CheckNorth(row, column);
        CheckEast(row, column);
        CheckSouth(row, column);
        CheckWest(row, column);
    }
    
    private void ResetForest()
    {
        foreach (var treeRow in forest)
        {
            foreach (var tree in treeRow)
            {
                tree.NorthTotal = 0;
                tree.EastTotal = 0;
                tree.SouthTotal = 0;
                tree.WestTotal = 0;
            }
        }
    }
    
    public void Part1()
    {
        CreateForestMap();
        for (int i = 0; i < forest[0].Count; i++)
        {
            CheckSouth(0, i);
        }
        for (int j = 0; j < forest.Count; j++)
        {
            CheckEast(j, 0);
        }
        for (int i = forest[0].Count - 1; i >= 0; i--)
        {
            CheckNorth(forest.Count - 1, i);
        }
        for (int j = forest.Count - 1; j >= 0; j--)
        {
            CheckWest(j, forest[0].Count - 1);
        }
        
        var total = forest.Sum(treeRow => treeRow.Count(tree => tree.NorthTotal > 0 || tree.EastTotal > 0 || tree.SouthTotal > 0 || tree.WestTotal > 0));

        Console.WriteLine($"Total Part 1 is {total}");
    }

    //Check forwards backwards up and down for each tree and count the distance they can see from that tree
    public void Part2()
    {
        int spectacularRating = 0;
        Vector2 position = new Vector2();
        CreateForestMap();
        for (var i = 0; i < forest.Count; i++)
        {
            for (var j = 0; j < forest[i].Count; j++)
            {
                CheckSurroundings(i, j);
                var totalSpec = forest[i][j].NorthTotal * forest[i][j].EastTotal * forest[i][j].SouthTotal * forest[i][j].WestTotal;
                if (totalSpec > spectacularRating)
                {
                    spectacularRating = totalSpec;
                    position = new Vector2(i, j);
                }
                ResetForest();
            }
        }
        Console.WriteLine($"Spectacularity Part 2 is {spectacularRating} at position ({position.X+1}, {position.Y+1})");
    }
}