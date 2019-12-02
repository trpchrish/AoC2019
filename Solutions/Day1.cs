using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Solutions
{
    public class Day1
    {
        public static int Day1_1()
        {
            int[] inputData = File.ReadAllLines(@"C:\AoC\AoC2019\InputData\day1.txt")
                                     .ToList().Select(c => Convert.ToInt32(c)).ToArray();
            var total = 0;

            foreach (var input in inputData)
            {
                var output = input / 3;
                output = (int)Math.Floor((decimal)output) - 2;

                total += output;
            }

            return total;
        }

        public static int Day1_2()
        {
            int[] inputData = File.ReadAllLines(@"C:\AoC\AoC2019\InputData\day1.txt")
                                     .ToList().Select(c => Convert.ToInt32(c)).ToArray();
            var total = 0;

            foreach (var input in inputData)
                total += CalculateFuelRequirement(input);

            return total;
        }

        private static int CalculateFuelRequirement(int mass)
        {
            var fuelRequired = (int)Math.Floor((decimal)(mass / 3) - 2);

            if (fuelRequired > 0)
                return fuelRequired += CalculateFuelRequirement(fuelRequired);

            return 0;
        }
    }
}
