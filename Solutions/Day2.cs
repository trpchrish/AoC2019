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
        public static int Day2_1()
        {
            int[] inputData = File.ReadAllText(@"C:\AoC\AoC2019\InputData\day2.txt").Split(',')
                                     .ToList().Select(c => Convert.ToInt32(c)).ToArray();

            //replace position 1 value with 12 and position 2 with 2
            inputData[1] = 12;
            inputData[2] = 2;

            return IntCode(inputData);
        }

        public static int Day2_2()
        {
            string[] data = File.ReadAllText(@"C:\AoC\AoC2019\InputData\day2.txt").Split(',');
            var noun = 0;
            var verb = 0;
            var hasRequiredOutput = false;

            for (noun = 0; noun < 100; noun++)
            {
                for (verb = 0; verb < 100; verb++)
                {
                    int[] inputData = data.ToList().Select(c => Convert.ToInt32(c)).ToArray();

                    inputData[1] = noun;
                    inputData[2] = verb;

                    if (IntCode(inputData) == 19690720)
                    {
                        hasRequiredOutput = true;
                        break;
                    }                        
                }

                if (hasRequiredOutput)
                    break;
            }

            return 100 * noun + verb;
        }

        private static int IntCode(int[] codes)
        {
            var opCode = codes[0];
            var opcodeIndex = 0;

            while (opCode != 99)
            {
                opCode = codes[opcodeIndex];

                var index1 = codes[opcodeIndex + 1];
                var index2 = codes[opcodeIndex + 2];
                var index3 = codes[opcodeIndex + 3];

                switch (opCode)
                {
                    case 1:
                        codes[index3] = codes[index1] + codes[index2];
                        break;

                    case 2:
                        codes[index3] = codes[index1] * codes[index2];
                        break;

                    default:
                        break;
                }
                opcodeIndex += 4;
                codes[0] = codes[index3];
            }

            return codes[0];
        }
    }
}
