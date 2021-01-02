using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day1
    {
        public int SolvePuzzleP1()
        {
            var result = 0;
            var items = File.ReadAllLines(@".\Files\day1.txt").Select(int.Parse);

            foreach (var item in items)
            {
                var found = items.Where(c => c + item == 2020);
                if (found.Count() > 0)
                {
                    result = found.FirstOrDefault() * item;
                    break;
                }
            }

            return result;
        }

        public int SolvePuzzleP2()
        {
            var items = File.ReadAllLines(@".\Files\day1.txt").Select(int.Parse);

            foreach (var i in items)
            {
                foreach (var i1 in items)
                {
                    foreach (var i2 in items)
                    {
                        if ((i + i1 + i2) == 2020)
                        {
                            return i * i1 * i2;
                        }
                    }
                }
            }
            return -1;
        }
    }
}
