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
            var program = GetCodes();
            var sequence = "01234";
            var sequenceLength = sequence.Length;
            var permutations = Permute(sequence, 0, sequenceLength - 1);             
                        
            long thrusterOutput = 0;


            foreach (var permutation in permutations)
            {  
                var amplifierA = new IntCode2(program);
                var amplifierB = new IntCode2(program);
                var amplifierC = new IntCode2(program);
                var amplifierD = new IntCode2(program);
                var amplifierE = new IntCode2(program);

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
                            amplifierA.Run();
                            outputA = amplifierA.Outputs.Last();
                            break;

                        case 1:
                            amplifierB.InputSignals.Enqueue(phaseSetting);
                            amplifierB.InputSignals.Enqueue(outputA);
                            amplifierB.Run();
                            outputB = amplifierB.Outputs.Last();
                            break;

                        case 2:
                            amplifierC.InputSignals.Enqueue(phaseSetting);
                            amplifierC.InputSignals.Enqueue(outputB);
                            amplifierC.Run();
                            outputC = amplifierC.Outputs.Last();
                            break;

                        case 3:
                            amplifierD.InputSignals.Enqueue(phaseSetting);
                            amplifierD.InputSignals.Enqueue(outputC);
                            amplifierD.Run();
                            outputD = amplifierD.Outputs.Last();
                            break;

                        case 4:
                            amplifierE.InputSignals.Enqueue(phaseSetting);
                            amplifierE.InputSignals.Enqueue(outputD);
                            amplifierE.Run();
                            outputE = amplifierE.Outputs.Last();

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
            var sequence = "56789";
            var sequenceLength = sequence.Length;
            var permutations = new List<string> { "98765" }; //Permute(sequence, 0, sequenceLength - 1);
            long thrusterOutput = 0;

            foreach (var permutation in permutations)
            {
                var program = GetCodes();
                var phaseSettings1 = Convert.ToInt64(permutation.Substring(0, 1));
                var phaseSettings2 = Convert.ToInt64(permutation.Substring(1, 1));
                var phaseSettings3 = Convert.ToInt64(permutation.Substring(2, 1));
                var phaseSettings4 = Convert.ToInt64(permutation.Substring(3, 1));
                var phaseSettings5 = Convert.ToInt64(permutation.Substring(4, 1));
                long inputSignal = 0;

                var amplifierA = new IntCode2(program);
                amplifierA.InputSignals.Enqueue(phaseSettings1);

                var amplifierB = new IntCode2(program);
                amplifierB.InputSignals.Enqueue(phaseSettings2);

                var amplifierC = new IntCode2(program);
                amplifierC.InputSignals.Enqueue(phaseSettings3);

                var amplifierD = new IntCode2(program);
                amplifierD.InputSignals.Enqueue(phaseSettings4);

                var amplifierE = new IntCode2(program);
                amplifierE.InputSignals.Enqueue(phaseSettings5);
                
                while (amplifierA.State != ProgramState.Stopped && amplifierB.State != ProgramState.Stopped &&
                       amplifierC.State != ProgramState.Stopped && amplifierD.State != ProgramState.Stopped &&
                       amplifierE.State != ProgramState.Stopped) 
                {
                    amplifierA.InputSignals.Enqueue(inputSignal);
                    amplifierA.Run();
                    var outputA = amplifierA.Outputs.Last();

                    amplifierB.InputSignals.Enqueue(outputA);
                    amplifierB.Run();
                    var outputB = amplifierB.Outputs.Last();

                    amplifierC.InputSignals.Enqueue(outputB);
                    amplifierC.Run();
                    var outputC = amplifierC.Outputs.Last();

                    amplifierD.InputSignals.Enqueue(outputC);
                    amplifierD.Run();
                    var outputD = amplifierD.Outputs.Last();

                    amplifierE.InputSignals.Enqueue(outputD);
                    amplifierE.Run();
                    var outputE = amplifierE.Outputs.Last();

                    inputSignal = outputE;
                    if (outputE > thrusterOutput)
                        thrusterOutput = outputE;
                }
            }
            
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
