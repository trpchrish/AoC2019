using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Solutions
{
    public class Day6
    {
        public static int Day6_1()
        {
            var nodeList = GetNodes();
            var orbitTree = BuildTree(nodeList, "COM");

            var totalOrbits = GetTotalOrbits(orbitTree);

            return totalOrbits;
        }
        private static int GetTotalOrbits(TreeNode<string> node)
        {
            var depth = node.Depth;

            foreach (var child in node.Children)
            {
                depth += GetTotalOrbits(child);
            }

            return depth;
        }

        public static int Day6_2()
        {
            var nodeList = GetNodes();
            var orbitTree = BuildTree(nodeList, "COM");
            var santaNode = GetNode(orbitTree, "SAN");
            var youNode = GetNode(orbitTree, "YOU");
            var santaPlanets = GetParentValues(santaNode);
            var youPlanets = GetParentValues(youNode);
            var commonAncester = santaPlanets.Intersect(youPlanets).FirstOrDefault();

            var jumps = 0;

            foreach (var planet in santaPlanets)
            {
                jumps++;
                if (planet.ToLower().Equals(commonAncester.ToLower()))
                    break;
            }

            foreach (var planet in youPlanets)
            {
                jumps++;
                if (planet.ToLower().Equals(commonAncester.ToLower()))
                    break;
            }

            jumps -= 4;

            return jumps;
        }

        private static List<string> GetParentValues(TreeNode<string> node) 
        {
            var planets = new List<string>();
            planets.Add(node.Value);

            if (node.Parent == null)
                return planets;

            planets.AddRange(GetParentValues(node.Parent));

            return planets;                       
        }
        
        private static TreeNode<string> GetNode(TreeNode<string>node, string leafNodeValue)
        {
            var leafNode = node;

            if (!leafNode.Value.ToLower().Equals(leafNodeValue.ToLower())) 
            {
                foreach (var child in node.Children)
                {
                    leafNode = GetNode(child, leafNodeValue);
                    if (leafNode.Value.ToLower().Equals(leafNodeValue.ToLower()))
                        break;
                }
            }

            return leafNode;
        }
        
        private static TreeNode<string> BuildTree(List<TreeNode<string>> nodes, string rootValue)
        {
            var rootNodeValue = nodes.Where(n => n.Parent.Value.ToLower().Equals(rootValue.ToLower())).Single().Parent.Value;
            var rootNode = new TreeNode<string>(rootNodeValue);
            BuildTree(rootNode, nodes);

            return rootNode;
        }

        private static TreeNode<string> BuildTree(TreeNode<string> node, List<TreeNode<string>> nodeList)
        {
            var children = nodeList.Where(n => n.Parent.Value == node.Value).ToList();

            foreach (var child in children)
            {
                node.AddChild(child);
                nodeList.Remove(child);
            }

            foreach (var child in node.Children) 
                BuildTree(child, nodeList);

            return node;
            
        }
        private static List<TreeNode<string>> GetNodes()
        {
            var inputData = File.ReadAllLines(@"C:\AoC\AoC2019\InputData\day6.txt").ToArray();
            var nodeList = new List<TreeNode<string>>();

            foreach (var input in inputData)
            {
                nodeList.Add(
                    new TreeNode<string>(input.Split(')')[1])
                    { Parent = new TreeNode<string>(input.Split(')')[0]) }
               );
            }

            return nodeList;
        }
    }

    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }
        public TreeNode<T> Parent { get; set; }

        public int Depth => (Parent == null) ? 0 : Parent.Depth + 1;

        public TreeNode(T value) 
        {
            Value = value;
            Children = new List<TreeNode<T>>();
        }

        public void AddChild(T value)
        {
            var node = new TreeNode<T>(value)
            {
                Parent = this
            };

            Children.Add(node);
        }

        public void AddChild(TreeNode<T> node)
        {
            node.Parent = this;
            Children.Add(node);
        }       

    }
}
