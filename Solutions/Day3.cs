using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Solutions
{
    public class Day3
    {
        public static int Day3_1()
        {
            var inputData = File.ReadAllLines(@"C:\AoC\AoC2019\InputData\day3.txt");
            var wire1 = inputData[0].Split(',').ToArray();
            var wire2 = inputData[1].Split(',').ToArray();

            var coordinatesList1 = GetCoordinatesWithSteps(wire1);
            var coordinatesList2 = GetCoordinatesWithSteps(wire2);

            (int x, int y)[] intersectionPoints = coordinatesList1.Values.Intersect(coordinatesList2.Values).ToArray();

            return intersectionPoints.Min(p => Math.Abs(p.x) + Math.Abs(p.y));
        }

        public static int Day3_2()
        {
            var inputData = File.ReadAllLines(@"C:\AoC\AoC2019\InputData\day3.txt");
            var wire1 = inputData[0].Split(',').ToArray();
            var wire2 = inputData[1].Split(',').ToArray();

            var coordinatesList1 = GetCoordinatesWithSteps(wire1);
            var coordinatesList2 = GetCoordinatesWithSteps(wire2);

            
            var intersectionPoints = coordinatesList1.Values.Intersect(coordinatesList2.Values).ToList();
            var stepsTaken = new List<int>();

            foreach (var point in intersectionPoints) 
                stepsTaken.Add(coordinatesList1.FirstOrDefault(x => x.Value == point).Key + coordinatesList2.FirstOrDefault(x => x.Value == point).Key);

            return stepsTaken.Min(s => s);
        }

        private static Dictionary<int, ValueTuple<int, int>> GetCoordinatesWithSteps(string[] instuctions)
        {
            var listOfCorodinates = new Dictionary<int, ValueTuple<int, int>>();
            var x = 0;
            var y = 0;
            var stepsTaken = 0;

            foreach (var instruction in instuctions)
            {
                var direction = instruction.Substring(0, 1);
                var steps = Convert.ToInt32(instruction.Substring(1));

                for (var i = 0; i < steps; i++)
                {
                    switch (direction)
                    {
                        case "R":
                            x++;
                            break;

                        case "L":
                            x--;
                            break;

                        case "U":
                            y++;
                            break;

                        case "D":
                            y--;
                            break;

                        default:
                            break;
                    }
                    stepsTaken++;
                    listOfCorodinates.Add(stepsTaken, (x, y));
                }
            }

            return listOfCorodinates;
        }

    }
}
