using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Solutions
{
    public class Day8
    {
        public static int Day8_1()
        {
            var encodings = File.ReadAllText(@"C:\AoC\AoC2019\InputData\day8.txt").ToCharArray()
                                .Select(c => Convert.ToInt32(c.ToString())).ToList();

            var layers = new List<List<int>>();
            var pixelsCount = 0;
            var layer = new List<int>();

            for (var i = 0; i < encodings.Count; i++)
            {
                if (pixelsCount > 149)
                {
                    layers.Add(layer);
                    layer = new List<int>();
                    pixelsCount = 0;
                }
                                                
                layer.Add(encodings[i]);
                pixelsCount++;
            }

            layers.Add(layer);
            var zeroCount = 100;
            var leastZeroLayer = new List<int>();


            foreach (var l in layers)
            {
                var count = l.Where(v => v == 0).Count();

                if (count < zeroCount)
                {
                    leastZeroLayer = new List<int>();
                    leastZeroLayer.AddRange(l);
                    zeroCount = count;
                }
                    
            }

            var numberOfOneDigits = leastZeroLayer.Where(v => v == 1).Count();
            var numberOfTwoDigits = leastZeroLayer.Where(v => v == 2).Count();

            return numberOfOneDigits * numberOfTwoDigits;
        }        

        public static void Day8_2()
        {            
            var encodings = File.ReadAllText(@"C:\AoC\AoC2019\InputData\day8.txt").ToCharArray()
                                .Select(c => c.ToString()).ToList();

            var width = 25;
            var height = 6;
            var index = 0;
            var layers = new string[encodings.Count() / width / height][][];

            for (var i = 0; i < layers.Length; i++)
            {
                layers[i] = new string[height][];
                for (var  j = 0; j < layers[i].Length; j++)
                {
                    layers[i][j] = new string[width];
                    for (var k = 0; k < layers[i][j].Length; k++)
                    {
                        layers[i][j][k] = encodings[index];
                        index++;
                    }
                }
            }
            
            var image = new string[height][];

            for (int i = 0; i < image.Length; i++)
                image[i] = new string[width];

            for (var i = 0; i < layers.Length; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    for (var k = 0; k < width; k++)
                    {
                        if (layers[i][j][k] != "2")
                        {
                            if (image[j][k] == null) 
                                image[j][k] = layers[i][j][k] == "1" ? "#" : " ";
                        }
                    }
                }
            }

            foreach (var line in image)
                Console.WriteLine(string.Join(" ", line));

        }
    }
}
