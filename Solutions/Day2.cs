using AoC2019.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Solutions
{
    public class Day2
    {
        public static long Day2_1()
        {
            long[] inputData = GetInputs();

            //replace position 1 value with 12 and position 2 with 2
            inputData[1] = 12;
            inputData[2] = 2;

            var intCode2 = new IntCode2(inputData);

            intCode2.Run();

            return inputData[0];
        }

        public static int Day2_2()
        {
            var noun = 0;
            var verb = 0;
            var hasRequiredOutput = false;

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    long[] inputData = GetInputs();

                    inputData[1] = i;
                    inputData[2] = j;

                    var intCode2 = new IntCode2(inputData);
                    intCode2.Run();

                    if (inputData[0] == 19690720)
                    {
                        hasRequiredOutput = true;
                        noun = i;
                        verb = j;
                        break;
                    }                        
                }

                if (hasRequiredOutput)
                    break;
            }

            return 100 * noun + verb;
        }

        private static long[] GetInputs()
        {
            return File.ReadAllText(@"C:\AoC\AoC2019\InputData\day2.txt").Split(',')
                                     .ToList().Select(c => Convert.ToInt64(c)).ToArray();
        }
    }
}
