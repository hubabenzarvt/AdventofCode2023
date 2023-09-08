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
        
        public int SpecularScore => NorthTotal * EastTotal * SouthTotal * WestTotal;
    }
    
    private void CheckNorth(int row, int column, bool recurse)
    {
        var tallestTree = -1;

        for (var i = column; i >= 0; i--)
        {
            var tree = forest[i][row];
            var counter = 0;
            
            if (tallestTree < tree.Height)
            {
                counter++;
                tallestTree = tree.Height;
                
                if (recurse && row > 0)
                {
                    CheckNorth(row - 1, column, true);
                }
            }

            tree.NorthTotal += counter;
        }
    }
    
    private void CheckEast(int row, int column, bool recurse)
    {
        var tallestTree = -1;

        for (var i = row; i < forest[column].Count; i++)
        {
            var tree = forest[column][i];
            var counter = 0;

            if (tallestTree < tree.Height)
            {
                counter++;
                tallestTree = tree.Height;
                
                if (recurse && column < forest.Count - 1)
                {
                    CheckEast(row + 1, column, true);
                }
            }

            tree.EastTotal += counter;
        }
    }
    
    private void CheckSouth(int row, int column, bool recurse)
    {
        var tallestTree = -1;
        
        for (var i = column; i < forest.Count; i++)
        {
            var tree = forest[i][row];
            var counter = 0;

            if (tallestTree < tree.Height)
            {
                counter++;
                tallestTree = tree.Height;
                
                if (recurse && row < forest[i].Count - 1)
                {
                    CheckSouth(row, column + 1, true);
                }

            }

            tree.SouthTotal += counter;
        }

        
    }
    
    private void CheckWest(int row, int column, bool recurse)
    {
        var tallestTree = -1;
        
        for (var i = row; i >= 0; i--)
        {
            var tree = forest[column][i];
            var counter = 0;

            if (tallestTree < tree.Height)
            {
                counter++;
                tallestTree = tree.Height;
                if (recurse && column > 0)
                {
                    CheckWest(row, column - 1, true);
                }
            }
            
            tree.WestTotal += counter;
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
    
    private void CheckSurrounding(int row, int column, bool recurse = false)
    {
        for (int i = row; i < forest[0].Count; i++)
        {
            CheckSouth(i, 0, recurse);
        }
        for (int j = column; j < forest.Count; j++)
        {
            CheckEast(0, j, recurse);
        }
        for (int i = 0; i < forest[row].Count; i++)
        {
            CheckNorth(i, forest.Count - 1, recurse);
        }
        for (int j = 0; j < forest.Count; j++)
        {
            CheckWest(forest[column].Count - 1, j, recurse);
        }
    }
    
    public void Part1()
    {
        CreateForestMap();
        CheckSurrounding(0,0);
        
        var total = forest.Sum(treeRow => treeRow.Count(tree => tree.NorthTotal > 0 || tree.EastTotal > 0 || tree.SouthTotal > 0 || tree.WestTotal > 0));

        Console.WriteLine($"Total Part 1 is {total}");
    }

    //Check forwards backwards up and down for each tree and count the distance they can see from that tree
    public void Part2()
    {
        int spectacularRating = 0;
        Vector2 position = new Vector2();
        Vector2 currposition = new Vector2();
        CreateForestMap();
        CheckSurrounding(0, 0, true);

        for (var row = 0; row < forest.Count; row++)
        {
            for (var column = 0; column < forest[row].Count; column++)
            {
                if (forest[row][column].SpecularScore > spectacularRating)
                {
                    spectacularRating = forest[row][column].SpecularScore;
                    position = new Vector2(row, column);
                }
                
                currposition = new Vector2(row, column);
                Console.WriteLine($"The Totals for ({currposition.X}, {currposition.X}) were " +
                                  $"N:{forest[(int) currposition.X][(int) currposition.Y].NorthTotal}, " +
                                  $"E:{forest[(int) currposition.X][(int) currposition.Y].EastTotal}, " +
                                  $"S:{forest[(int) currposition.X][(int) currposition.Y].SouthTotal}, " +
                                  $"W:{forest[(int) currposition.X][(int) currposition.Y].WestTotal}, " +
                                  $"H:{forest[(int) currposition.X][(int) currposition.Y].Height}");
            }
        }
        Console.WriteLine($"Spectacularity Part 2 is {spectacularRating} at position ({position.X}, {position.Y}), which is {forest[(int) position.Y][(int) position.X].Height} high");
    }
}