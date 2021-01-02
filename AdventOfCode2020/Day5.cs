using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    class Day5
    {
        public static string SolvePuzzleP1(FileInfo fileInfo)
        {
            var boardingpasses = File.ReadAllLines(fileInfo.FullName);
            long result = 0;
            foreach (var pass in boardingpasses)
            {
                var t = GetSeat(pass);
                if (t.id > result)
                    result = t.id;
            }
            return result.ToString();
        }

        public static string SolvePuzzleP2(FileInfo fileInfo)
        {
            var allSeats = new List<(int row, int column, int id)>();

            for (int i = 0; i < 128; i++)
            {
                var row = i;
                for (int col = 0; col < 8; col++)
                {
                    allSeats.Add((row, col, (row * 8) + col));
                }
            }

            var takenSeats = new List<(int row, int column, int id)>();

            var boardingpasses = File.ReadAllLines(fileInfo.FullName);
            long result = 0;
            foreach (var pass in boardingpasses)
            {
                var t = GetSeat(pass);
                takenSeats.Add(t);
                if (allSeats.Contains(t))
                    allSeats.Remove(t);
            }

            var idsAvail = allSeats.Select(c => c.id);

            var mySeat = idsAvail.Where(c => !idsAvail.Contains(c + 1) && !idsAvail.Contains(c - 1));

            if (mySeat.Count() == 1)
                result = mySeat.First();
            //mySeat = from id in idsAvail
            //             where !idsAvail.Contains(id + 1) && !idsAvail.Contains(id - 1)
            //             select id;

            //foreach (var id in idsAvail)
            //{
            //    if (!idsAvail.Contains(id + 1) && !idsAvail.Contains(id - 1))
            //        result = id;
            //}


            return result.ToString();

        }


        private static (int row, int column, int id) GetSeat(string pass)
        {
            var row = 0;
            var rowFrom = 0;
            var rowTo = 127;

            var column = 0;
            var columnFrom = 0;
            var columnTo = 7;

            var id = 0;
            for (int i = 0; i < 7; i++)
            {
                var diff = rowTo - rowFrom;
                diff = diff / 2 == 0 ? diff / 2 : (diff + 1) / 2;

                if (pass[i] == 'F')
                    rowTo -= diff;
                else
                    rowFrom += diff;

                if (i == 6)
                    row = pass[i] == 'F' ? rowFrom : rowTo;
            }

            for (int i = 7; i < 10; i++)
            {
                var diff = columnTo - columnFrom;
                diff = diff / 2 == 0 ? diff / 2 : (diff + 1) / 2;

                if (pass[i] == 'L')
                    columnTo -= diff;
                else
                    columnFrom += diff;
                if (i == 9)
                    column = pass[i] == 'L' ? columnFrom : columnTo;
            }

            id = row * 8 + column;

            return (row, column, id);
        }

        public static bool Test()
        {
            return GetSeat("FBFBBFFRLR") == (44, 5, 357) && GetSeat("BFFFBBFRRR") == (70, 7, 567) && GetSeat("FFFBBBFRRR") == (14, 7, 119) && GetSeat("BBFFBBFRLL") == (102, 4, 820);
        }
    }
}
