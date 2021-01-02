using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day6
    {
        public static string SolvePuzzleP1(FileInfo fileInfo)
        {
            int result = 0;

            List<List<string>> list = GetGroupAnswers(fileInfo);

            var groupAnswers = from item in list
                                select item.SelectMany(c => c.ToCharArray());

            foreach (var answers in groupAnswers)
            {
                result += answers.Distinct().Count();
            }

            return result.ToString();
        }

        public static string SolvePuzzleP2(FileInfo fileInfo)
        {
            int result = 0;
            List<List<string>> list = GetGroupAnswers(fileInfo);

            foreach (var group in list)
            {
                var answers = group.Select(c => c.ToCharArray());

                var firstPerson = answers.First();

                result += (firstPerson.Where(c => answers.All(b => b.Contains(c)))).Count();
                //foreach (var answer in firstPerson)
                //{
                //    if (answers.All(c => c.Contains(answer)))
                //        result++;
                //}
            }

            return result.ToString();
        }


        private static List<List<string>> GetGroupAnswers(FileInfo fileInfo)
        {
            var raw = File.ReadAllText(fileInfo.FullName).Split(Environment.NewLine + Environment.NewLine);
            var list = new List<List<string>>();

            foreach (var group in raw)
            {
                var answers = group.Split(Environment.NewLine).ToList();
                list.Add(answers);
            }

            return list;
        }


    }
}
