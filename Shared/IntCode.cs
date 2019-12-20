using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Shared
{
    public class IntCode
    {
        public long[] Codes { get; private set; }
        public Queue<long> InputSignals { get; set; }

        public IntCode(long[] codes)
        {
            Codes = codes;
            InputSignals = new Queue<long>();
        }

        public IntCode(long[] codes, Queue<long> inputs)
        {
            Codes = codes;
            InputSignals = inputs;
        }

        public long Output()
        {
            var opCode = Codes[0];
            var opCodeString = opCode.ToString("D5");
            var opcodeIndex = 0L;
            var output = 0L;

            while (opCodeString != "99")
            {
                opCodeString = Codes[opcodeIndex].ToString("D5");

                //0 = position mode - address
                //1 = immediate mode =- value
                var param2 = opCodeString.Substring(opCodeString.Length - 4, 1);
                var param1 = opCodeString.Substring(opCodeString.Length - 3, 1);


                opCodeString = opCodeString.Substring(opCodeString.Length - 2);

                long value1;
                long value2;
                long value3;


                switch (opCodeString)
                {
                    case "01":
                        value1 = param1.Equals("0") ? Codes[Codes[opcodeIndex + 1]] : Codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? Codes[Codes[opcodeIndex + 2]] : Codes[opcodeIndex + 2];
                        value3 = Codes[opcodeIndex + 3];

                        Codes[value3] = value1 + value2;
                        opcodeIndex += 4;

                        break;

                    case "02":
                        value1 = param1.Equals("0") ? Codes[Codes[opcodeIndex + 1]] : Codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? Codes[Codes[opcodeIndex + 2]] : Codes[opcodeIndex + 2];
                        value3 = Codes[opcodeIndex + 3];

                        Codes[value3] = value1 * value2;
                        opcodeIndex += 4;

                        break;

                    case "03": //address
                        if (InputSignals.Any())
                        {
                            value1 = Codes[opcodeIndex + 1];
                            Codes[value1] = InputSignals.Dequeue();
                            opcodeIndex += 2;
                        }

                        break;

                    case "04": //value
                        value1 = Codes[opcodeIndex + 1];
                        output = param1.Equals("0") ? Codes[value1] : value1;
                        opcodeIndex += 2;

                        break;

                    case "05": //jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        value1 = param1.Equals("0") ? Codes[Codes[opcodeIndex + 1]] : Codes[opcodeIndex + 1];
                        value2 = Codes[opcodeIndex + 2];
                        if (value1 != 0)
                        {
                            opcodeIndex = param2.Equals("0") ? Codes[value2] : value2;
                        }
                        else
                        {
                            opcodeIndex += 3;
                        }
                        break;

                    case "06": //jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        value1 = param1.Equals("0") ? Codes[Codes[opcodeIndex + 1]] : Codes[opcodeIndex + 1];
                        value2 = Codes[opcodeIndex + 2];
                        if (value1 == 0)
                        {
                            opcodeIndex = param2.Equals("0") ? Codes[value2] : value2;
                        }
                        else
                        {
                            opcodeIndex += 3;
                        }
                        break;

                    case "07": //less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        value1 = param1.Equals("0") ? Codes[Codes[opcodeIndex + 1]] : Codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? Codes[Codes[opcodeIndex + 2]] : Codes[opcodeIndex + 2];
                        value3 = Codes[opcodeIndex + 3];
                        if (value1 < value2)
                        {
                            Codes[value3] = 1;
                        }
                        else
                        {
                            Codes[value3] = 0;
                        }
                        opcodeIndex += 4;
                        break;

                    case "08": //equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        value1 = param1.Equals("0") ? Codes[Codes[opcodeIndex + 1]] : Codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? Codes[Codes[opcodeIndex + 2]] : Codes[opcodeIndex + 2];
                        value3 = Codes[opcodeIndex + 3];
                        if (value1 == value2)
                        {
                            Codes[value3] = 1;
                        }
                        else
                        {
                            Codes[value3] = 0;
                        }
                        opcodeIndex += 4;
                        break;

                    case "99":
                        break;

                    default:
                        Console.WriteLine($"Unrecognised code {opCodeString} at {Codes[opcodeIndex]}");
                        throw new Exception("unrecognised code");

                }

            }

            return output;
        }
    }
}
