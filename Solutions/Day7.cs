using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Solutions
{
    public class Day7
    {
        public static long Day7_1()
        {
            var sequence = "01234";
            var sequenceLength = sequence.Length;
            var permutations = Permute(sequence, 0, sequenceLength - 1);
            var ampliferQueue = new Queue<Amplifier>();                                  
                        
            long thrusterOutput = 0;


            foreach (var permutation in permutations)
            {      
                ampliferQueue.Enqueue(new Amplifier(GetCodes()));
                ampliferQueue.Enqueue(new Amplifier(GetCodes()));
                ampliferQueue.Enqueue(new Amplifier(GetCodes()));
                ampliferQueue.Enqueue(new Amplifier(GetCodes()));
                ampliferQueue.Enqueue(new Amplifier(GetCodes()));              
                
                long outputA = 0;
                long outputB = 0;
                long outputC = 0;
                long outputD = 0;
                long outputE = 0;

                for (var i = 0; i < permutation.Length; i++)
                {
                    if (ampliferQueue.Count == 0)
                        break;
                    
                    var amp = ampliferQueue.Dequeue();
                    var phaseSetting = Convert.ToInt64(permutation.Substring(i, 1));
                    amp.InputSignals.Enqueue(phaseSetting);

                    switch (i)
                    {
                        case 0:                        
                            amp.InputSignals.Enqueue(0L);
                            outputA = amp.GetOutput();
                            break;

                        case 1:
                            amp.InputSignals.Enqueue(outputA);
                            outputB = amp.GetOutput();
                            break;

                        case 2:
                            amp.InputSignals.Enqueue(outputB);
                            outputC = amp.GetOutput();
                            break;

                        case 3:
                            amp.InputSignals.Enqueue(outputC);
                            outputD = amp.GetOutput();
                            break;

                        case 4:
                            amp.InputSignals.Enqueue(outputD);
                            outputE = amp.GetOutput();

                            if (outputE > thrusterOutput)
                                thrusterOutput = outputE;
                            break;

                        default:
                            break;
                    }
                }
            }            

            return thrusterOutput;
        }

        public static long Day7_2()
        {
            long thrusterOutput = 0;

            return thrusterOutput;
        }

        private static List<string> Permute(string sequence, int startIndex, int length)
        {
            var permutations = new List<string>();
            if (startIndex == length)
            { 
                permutations.Add(sequence);
            }
            else
            {
                for (var i = startIndex; i <= length; i++)
                {
                    sequence = Swap(sequence, startIndex, i);
                    permutations.AddRange(Permute(sequence, startIndex + 1, length));
                    sequence = Swap(sequence, startIndex, i);
                }
            }

            return permutations;
        }

        private static string Swap(string sequence, int index1, int index2)
        {            
            var charArray = sequence.ToCharArray();
            var temp = charArray[index1];

            charArray[index1] = charArray[index2];
            charArray[index2] = temp;

            return new string(charArray);
        }

        private static long[] GetCodes()
        {
            return File.ReadAllText(@"C:\AoC\AoC2019\InputData\day7.txt").Split(',')
                                    .ToList().Select(c => Convert.ToInt64(c)).ToArray();
        }
        
    }

    public class Amplifier
    {
        public long[] Codes { get; private set; }
        public Queue<long> InputSignals { get; set; }

        public Amplifier(long[] codes)
        {
            Codes = codes;
            InputSignals = new Queue<long>();
        }

        public long GetOutput()
        {
            return IntCode(Codes);
        }

        private long IntCode(long[] codes)
        {
            var opCode = codes[0];
            var opCodeString = opCode.ToString("D5");
            var opcodeIndex = 0L;
            var output = 0L;

            while (opCodeString != "99")
            {
                opCodeString = codes[opcodeIndex].ToString("D5");

                //0 = position mode - address
                //1 = immediate mode =- value
                var param3 = opCodeString.Substring(opCodeString.Length - 5, 1);
                var param2 = opCodeString.Substring(opCodeString.Length - 4, 1);
                var param1 = opCodeString.Substring(opCodeString.Length - 3, 1);


                opCodeString = opCodeString.Substring(opCodeString.Length - 2);

                long value1;
                long value2;
                long value3;


                switch (opCodeString)
                {
                    case "01":
                        value1 = param1.Equals("0") ? codes[codes[opcodeIndex + 1]] : codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? codes[codes[opcodeIndex + 2]] : codes[opcodeIndex + 2];
                        value3 = codes[opcodeIndex + 3];

                        codes[value3] = value1 + value2;
                        opcodeIndex += 4;

                        break;

                    case "02":
                        value1 = param1.Equals("0") ? codes[codes[opcodeIndex + 1]] : codes[opcodeIndex + 1];
                        value2 = param2.Equals("0") ? codes[codes[opcodeIndex + 2]] : codes[opcodeIndex + 2];
                        value3 = codes[opcodeIndex + 3];

                        codes[value3] = value1 * value2;
                        opcodeIndex += 4;

                        break;

                    case "03": //address
                        if (InputSignals.Any())
                        {
                            value1 = codes[opcodeIndex + 1];
                            codes[value1] = InputSignals.Dequeue();
                            opcodeIndex += 2;
                        }                        

                        break;

                    case "04": //value
                        value1 = codes[opcodeIndex + 1];
                        output = param1.Equals("0") ? codes[value1] : value1;
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
                        break;

                    default:
                        Console.WriteLine($"Unrecognised code {opCodeString} at {codes[opcodeIndex]}");
                        throw new Exception("unrecognised code");

                }

            }

            return output;
        }
    }
}
