using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day4
    {
        private static readonly List<string> requiredItems = new List<string>() { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };

        public static string SolvePuzzleP1(FileInfo fileInfo)
        {
            var result = 0;
            var file = File.ReadAllText(fileInfo.FullName);

            var passports = GetPassports(file);

            foreach (var passportItems in passports)
            {
                if (AllItemsExist(passportItems))
                    result++;
            }

            return result.ToString();

        }

        public static string SolvePuzzleP2(FileInfo fileInfo)
        {
            var result = 0;
            var file = File.ReadAllText(fileInfo.FullName);

            var passports = GetPassports(file);

            foreach (var passport in passports)
            {
                if (AllItemsExist(passport))
                {
                    var dictItems = new Dictionary<string, string>();
                    foreach (var item in passport)
                    {
                        var divider = item.IndexOf(":");
                        dictItems.Add(item.Substring(0, divider), item.Substring(divider + 1, item.Length - divider - 1));
                    }
                    if (ItemsValid(dictItems))
                        result++;
                }

            }
            return result.ToString();
        }



        private static bool AllItemsExist(List<string> passportItems)
        {
            var items = passportItems;

            if (items.Count >= 7 & items.Count <= 8)
            {
                items = items.Select(c => c.Substring(0, c.IndexOf(":") + 1)).ToList();

                var missing = requiredItems.Except(items).ToList();
                if (missing.Count == 0)
                    return true;
                else if (missing.Count == 1 && missing.First() == "cid:")
                    return true;
            }
            return false;
        }

        private static bool ItemsValid(Dictionary<string, string> dictItems)
        {
            foreach (var item in dictItems)
            {
                switch (item.Key)
                {
                    case "byr":
                        if (!NumberValid(item.Value, 1920, 2002))
                            return false;
                        break;
                    case "iyr":
                        if (!NumberValid(item.Value, 2010, 2020))
                            return false;
                        break;
                    case "eyr":
                        if (!NumberValid(item.Value, 2020, 2030))
                            return false;
                        break;
                    case "hgt":
                        if (item.Value.Trim().ToLower().EndsWith("cm"))
                        {
                            if (!NumberValid(item.Value.Remove(item.Value.Length - 2), 150, 193))
                                return false;
                        }
                        else if (item.Value.Trim().ToLower().EndsWith("in"))
                        {
                            if (!NumberValid(item.Value.Remove(item.Value.Length - 2), 59, 76))
                                return false;
                        }
                        else
                            return false;
                        break;
                    case "hcl":
                        if (new Regex("^#[0-9\a-f]{6}").IsMatch(item.Value))
                            break;
                        else
                            return false;
                    case "ecl":
                        var allowedEcl = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                        if (!allowedEcl.Contains(item.Value))
                            return false;
                        break;
                    case "pid":
                        if (item.Value.Length != 9 || !int.TryParse(item.Value, out int _i))
                            return false;
                        break;
                    case "cid":
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        private static bool NumberValid(string value, int minVal, int maxVal)
        {
            if (int.TryParse(value, out int year))
            {
                if (year >= minVal && year <= maxVal)
                    return true;
            }
            return false;
        }

        private static List<List<string>> GetPassports(string file)
        {
            var result = new List<List<string>>();
            var passports = file.Split(Environment.NewLine + Environment.NewLine);
            foreach (var passport in passports)
            {
                result.Add(GetPassportItems(passport));
            }

            return result;
        }

        private static List<string> GetPassportItems(string passport)
        {
            var passportItems = new Dictionary<string, string>();
            var items = passport.Split(Environment.NewLine).ToList();

            items = items.Where(c => c.Contains(" ")).Select(c => c.Split(" ")).SelectMany(c => c).Concat(items).ToList();

            items = items.Except(items.Where(c => c.Contains(" ")).ToList()).ToList();
            return items;
        }
    }
}
