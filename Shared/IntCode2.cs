using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Shared
{
    public class IntCode2
    {
        public long[] ProgramCode { get; private set; }
        public Queue<long> InputSignals { get; set; }
        public List<long> Outputs { get; set; }
        public ProgramState State { get; set; }

        public IntCode2(long[] codes) 
        {
            ProgramCode = codes;
            InputSignals = new Queue<long>();
            Outputs = new List<long>();
            State = ProgramState.Running;
        }

        public void Run() 
        {
            long pointer = 0;
            while (pointer <= ProgramCode.Length)
            {
                var opCodestring = ProgramCode[pointer].ToString("D5");
                var instruction = opCodestring.Substring(opCodestring.Length - 2);
                long value1;
                long value2;
                long value3;
                long parameter1;
                long parameter2;

                //Console.WriteLine(string.Join(",", ProgramCode));
                switch (instruction)
                {
                    case "01":                        
                        value1 = ProgramCode[pointer + 1];
                        value2 = ProgramCode[pointer + 2];
                        value3 = ProgramCode[pointer + 3];

                        parameter1 = opCodestring.Substring(opCodestring.Length - 3, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value1] : value1;
                        parameter2 = opCodestring.Substring(opCodestring.Length - 4, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value2] : value2;

                        ProgramCode[value3] =
                            parameter1 + parameter2;

                        pointer += 4;
                        break;

                    case "02":
                        value1 = ProgramCode[pointer + 1];
                        value2 = ProgramCode[pointer + 2];
                        value3 = ProgramCode[pointer + 3];

                        parameter1 = opCodestring.Substring(opCodestring.Length - 3, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value1] : value1;
                        parameter2 = opCodestring.Substring(opCodestring.Length - 4, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value2] : value2;

                        ProgramCode[value3] =
                            parameter1 * parameter2; ;

                        pointer += 4;
                        break;

                    case "03":
                        if (InputSignals.Any())
                        {
                            value1 = ProgramCode[pointer + 1];
                            ProgramCode[value1] = InputSignals.Dequeue();
                            pointer += 2;
                        }
                        else 
                        {
                            return;                            
                        }                        

                        break;

                    case "04":
                        value1 = ProgramCode[pointer + 1];
                        Outputs.Add(ProgramCode[value1]);
                        pointer += 2;

                        break;

                    case "05": //jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        value1 = ProgramCode[pointer + 1];
                        value2 = ProgramCode[pointer + 2];

                        parameter1 = opCodestring.Substring(opCodestring.Length - 3, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value1] : value1;
                        parameter2 = opCodestring.Substring(opCodestring.Length - 4, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value2] : value2;

                        if (parameter1 != 0)
                        {
                            pointer = parameter2;
                        }
                        else 
                        {
                            pointer += 3;
                        }

                        break;

                    case "06": //jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        value1 = ProgramCode[pointer + 1];
                        value2 = ProgramCode[pointer + 2];

                        parameter1 = opCodestring.Substring(opCodestring.Length - 3, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value1] : value1;
                        parameter2 = opCodestring.Substring(opCodestring.Length - 4, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value2] : value2;
                        
                        if (parameter1 == 0)
                        {
                            pointer = parameter2;
                        }
                        else
                        {
                            pointer += 3;
                        }

                        break;

                    case "07": //less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        value1 = ProgramCode[pointer + 1];
                        value2 = ProgramCode[pointer + 2];
                        value3 = ProgramCode[pointer + 3];

                        parameter1 = opCodestring.Substring(opCodestring.Length - 3, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value1] : value1;
                        parameter2 = opCodestring.Substring(opCodestring.Length - 4, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value2] : value2;

                        if (parameter1 < parameter2)
                        {
                            ProgramCode[value3] = 1;
                        }
                        else 
                        {
                            ProgramCode[value3] = 0;
                        }

                        pointer += 4;

                        break;

                    case "08": //equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        value1 = ProgramCode[pointer + 1];
                        value2 = ProgramCode[pointer + 2];
                        value3 = ProgramCode[pointer + 3];

                        parameter1 = opCodestring.Substring(opCodestring.Length - 3, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value1] : value1;
                        parameter2 = opCodestring.Substring(opCodestring.Length - 4, 1) == Convert.ToInt32(IntCodeMode.Position).ToString() ? ProgramCode[value2] : value2;

                        if (parameter1 == parameter2)
                        {
                            ProgramCode[value3] = 1;
                        }
                        else
                        {
                            ProgramCode[value3] = 0;
                        }

                        pointer += 4;

                        break;

                    case "99":
                        State = ProgramState.Stopped;
                        return;
                }
            }
        }
    }

    public enum IntCodeMode 
    {
        Position = 0,
        Immediate = 1
    }

    public enum ProgramState
    {
        Stopped = 0,
        Running = 1
    }
}
