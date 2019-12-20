using AoC2019.Shared;
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
                        
            long thrusterOutput = 0;


            foreach (var permutation in permutations)
            {  
                var amplifierA = new IntCode(GetCodes());
                var amplifierB = new IntCode(GetCodes());
                var amplifierC = new IntCode(GetCodes());
                var amplifierD = new IntCode(GetCodes());
                var amplifierE = new IntCode(GetCodes());

                long outputA = 0;
                long outputB = 0;
                long outputC = 0;
                long outputD = 0;
                long outputE = 0;

                for (var i = 0; i < permutation.Length; i++)
                {                                        
                    var phaseSetting = Convert.ToInt64(permutation.Substring(i, 1));

                    switch (i)
                    {
                        case 0:
                            amplifierA.InputSignals.Enqueue(phaseSetting);
                            amplifierA.InputSignals.Enqueue(0L);
                            outputA = amplifierA.Output();
                            break;

                        case 1:
                            amplifierB.InputSignals.Enqueue(phaseSetting);
                            amplifierB.InputSignals.Enqueue(outputA);
                            outputB = amplifierB.Output();
                            break;

                        case 2:
                            amplifierC.InputSignals.Enqueue(phaseSetting);
                            amplifierC.InputSignals.Enqueue(outputB);
                            outputC = amplifierC.Output();
                            break;

                        case 3:
                            amplifierD.InputSignals.Enqueue(phaseSetting);
                            amplifierD.InputSignals.Enqueue(outputC);
                            outputD = amplifierD.Output();
                            break;

                        case 4:
                            amplifierE.InputSignals.Enqueue(phaseSetting);
                            amplifierE.InputSignals.Enqueue(outputD);
                            outputE = amplifierE.Output();

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
    
}
