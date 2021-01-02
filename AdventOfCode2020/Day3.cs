using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day3
    {
        public static int SolvePuzzleP1()
        {
            var items = File.ReadAllLines(@".\Files\day3.txt");

            return CountTrees(items, 3, 1);
        }

        private static int CountTrees(string[] rows, int stepsRight, int stepsDown)
        {
            int trees = 0;
            var position = 0;
            var row = 0;

            while (row < rows.Length)
            {
                var currRow = rows[row];
                while (position >= currRow.Length)
                    currRow += rows[row];

                if (currRow[position] == '#')
                    trees++;

                position += stepsRight;
                row += stepsDown;
            }

            return trees;
        }

        public static string SolvePuzzleP2()
        {
            var items = File.ReadAllLines(@".\Files\day3.txt");

            long result = CountTrees(items, 1, 2);

            var trees = new List<int>();

            for (int i = 1; i < 8; i +=2)
            {
                trees.Add(CountTrees(items, i, 1));
            }

            foreach (var item in trees)
            {
                result *= item;
            }

            return result.ToString();
        }

    }
}
