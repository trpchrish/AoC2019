using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Solutions
{
    public class Day5
    {
        public static int Day5_1()
        {
            int[] inputData = File.ReadAllText(@"C:\AoC\AoC2019\InputData\day5.txt").Split(',')
                                    .ToList().Select(c => Convert.ToInt32(c)).ToArray();

            var inputValue = 1;

            return IntCode(inputData, inputValue);
        }

        public static int Day5_2()
        {
            int[] inputData = File.ReadAllText(@"C:\AoC\AoC2019\InputData\day5.txt").Split(',')
                                    .ToList().Select(c => Convert.ToInt32(c)).ToArray();

            var inputValue = 5;

            return IntCode(inputData, inputValue);
        }

        private static int IntCode(int[] codes, int inputValue)
        {
            var opCode = codes[0];
            var opCodeString = opCode.ToString("D5");
            var opcodeIndex = 0;
            
            while (opCodeString != "99")
            {
                opCodeString = codes[opcodeIndex].ToString("D5");

                //0 = position mode - address
                //1 = immediate mode =- value
                var param3 = opCodeString.Substring(opCodeString.Length - 5, 1);
                var param2 = opCodeString.Substring(opCodeString.Length - 4, 1);
                var param1 = opCodeString.Substring(opCodeString.Length - 3, 1);
                

                opCodeString = opCodeString.Substring(opCodeString.Length - 2);

                int value1;
                int value2;
                int value3;

                switch (opCodeString)
                {
                    case "01":
                        value1 = param1.Equals("0") ? codes[codes[opcodeIndex + 1]] : codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? codes[codes[opcodeIndex + 2]] : codes[opcodeIndex + 2];
                        value3 = codes[opcodeIndex + 3];

                        codes[value3] = value1 + value2;
                        codes[0] = codes[value3];
                        opcodeIndex += 4;
                        
                        break;

                    case "02":
                        value1 = param1.Equals("0") ? codes[codes[opcodeIndex + 1]] : codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? codes[codes[opcodeIndex + 2]] : codes[opcodeIndex + 2];
                        value3 = codes[opcodeIndex + 3];

                        codes[value3] = value1 * value2;
                        codes[0] = codes[value3];
                        opcodeIndex += 4;

                        break;

                    case "03": //address
                        value1 = codes[opcodeIndex + 1];
                        codes[value1] = inputValue;
                        opcodeIndex += 2;

                        break;

                    case "04": //value
                        value1 = codes[opcodeIndex + 1];                        
                        codes[0] = param1.Equals("0") ? codes[value1] : value1;

                        Console.WriteLine($"output: {codes[0].ToString()}");

                        opcodeIndex += 2;

                        break;

                    case "05": //jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        value1 = param1.Equals("0") ? codes[codes[opcodeIndex + 1]] : codes[opcodeIndex + 1];
                        value2 = codes[opcodeIndex + 2];
                        if (value1 != 0)
                        {
                            opcodeIndex = param2.Equals("0") ? codes[value2] : value2;
                        }
                        else
                        {
                            opcodeIndex += 3;
                        }
                        break;

                    case "06": //jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        value1 = param1.Equals("0") ? codes[codes[opcodeIndex + 1]] : codes[opcodeIndex + 1];
                        value2 = codes[opcodeIndex + 2];
                        if (value1 == 0)
                        {
                            opcodeIndex = param2.Equals("0") ? codes[value2] : value2;
                        }
                        else
                        {
                            opcodeIndex += 3;
                        }
                        break;

                    case "07": //less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        value1 = param1.Equals("0") ? codes[codes[opcodeIndex + 1]] : codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? codes[codes[opcodeIndex + 2]] : codes[opcodeIndex + 2];
                        value3 = codes[opcodeIndex + 3];
                        if (value1 < value2)
                        {
                            codes[value3] = 1;
                        }
                        else
                        {
                            codes[value3] = 0;
                        }
                        opcodeIndex += 4;
                        break;

                    case "08": //equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        value1 = param1.Equals("0") ? codes[codes[opcodeIndex + 1]] : codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? codes[codes[opcodeIndex + 2]] : codes[opcodeIndex + 2];
                        value3 = codes[opcodeIndex + 3];
                        if (value1 == value2)
                        {
                            codes[value3] = 1;
                        }
                        else
                        {
                            codes[value3] = 0;
                        }
                        opcodeIndex += 4;
                        break;

                    case "99":
                        Console.WriteLine($"End of instruction. Final output: {codes[0]}");
                        break;

                    default:
                        Console.WriteLine($"Unrecognised code {opCodeString} at {codes[opcodeIndex]}");
                        throw new Exception("unrecognised code");
                        
                }

            }

            return codes[0];
        }
    }
}
