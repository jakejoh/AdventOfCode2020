using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day2
    {
        public int SolvePuzzleP1()
        {
            var result = 0;
            var items = File.ReadAllLines(@".\Files\day2.txt");

            foreach (var item in items)
            {
                var counter = item.Substring(0, item.IndexOf(' '));
                var letter = item[item.IndexOf(':') - 1];
                var pw = item.Substring(item.IndexOf(':') + 2);

                var min = int.Parse(counter.Substring(0, counter.IndexOf('-')).Trim());
                var max = int.Parse(counter.Substring(counter.IndexOf('-') + 1).Trim());

                var count = pw.Count(c => c.Equals(letter));

                if (count >= min && count <= max)
                    result++;

            }

            return result;
        }

        public int SolvePuzzleP2()
        {
            var result = 0;
            var items = File.ReadAllLines(@".\Files\day2.txt");

            foreach (var item in items)
            {
                int.TryParse(item.Substring(0, item.IndexOf('-')), out int index1);
                int.TryParse(item.Substring(item.IndexOf('-') + 1, item.IndexOf(' ') - item.IndexOf('-')), out int index2);

                if(index1 > 0 && index2 > 0)
                {
                    var letter = item[item.IndexOf(':') - 1];
                    var pw = item.Substring(item.IndexOf(':') + 2);

                    var lenght = pw.Length;

                    if (pw[index1 - 1] == letter && pw[index2 - 1] != letter)
                        result++;
                    else if (pw[index1 - 1] != letter && pw[index2 - 1] == letter)
                        result++;
                }

            }

            return result;
        }
    }
}
