using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mad_III_IV
{
    class Program
    {
        public static double GiniIndex(List<DataItem> items, int posAttribute, string attributeValue, string classItem)
        {
            var totalAttributeValues = 0;
            var classValPos = 0;
            var classValNeg = 0;

            foreach(var item in items)
            {

                if (item.atributes[posAttribute] == attributeValue)
                {
                    if (item.itemClass == classItem)
                    {
                        classValPos++;
                    }
                    else
                    {
                        classValNeg++;
                    }
                    totalAttributeValues++;
                }
                
            }
            
            var fN = Math.Pow(((double)classValPos / (double)totalAttributeValues), 2);
            var sN = Math.Pow(((double)classValNeg / (double)totalAttributeValues), 2);

            return (1d - (fN + sN));
        }

        static void GiniIndexCol(int pos, List<DataItem> itemSet)
        {
            var tempDict = new Dictionary<string, double>();
            foreach (var attrbt in itemSet)
            {
                if (tempDict.ContainsKey(attrbt.atributes[pos]))
                {
                    continue;
                }
                else
                {
                    tempDict.Add(attrbt.atributes[pos], GiniIndex(itemSet, pos, attrbt.atributes[pos], "0"));
                }
            }

            var sum = 0d;

            foreach (var i in tempDict)
            {
                Console.WriteLine(i.Key + " = " + i.Value);
                sum += i.Value;
            }

            
            Console.WriteLine("Col {0} = {1}", pos, sum / tempDict.Count);
            Console.WriteLine();
        }


        public static double Entropy(List<DataItem> items, int posAttribute, string attributeValue, string classItem)
        {
            var totalAttributeValues = 0;
            var classValPos = 0;
            var classValNeg = 0;

            foreach (var item in items)
            {

                if (item.atributes[posAttribute] == attributeValue)
                {
                    if (item.itemClass == classItem)
                    {
                        classValPos++;
                    }
                    else
                    {
                        classValNeg++;
                    }
                    totalAttributeValues++;
                }

            }

            var fN = ((double)classValPos / (double)totalAttributeValues) * Math.Log(((double)classValPos / (double)totalAttributeValues));
            var sN = ((double)classValNeg / (double)totalAttributeValues) * Math.Log(((double)classValNeg / (double)totalAttributeValues));


            var res = (-1d * (fN + sN));
            if(res.ToString() == "NaN")
            {
                return 0;
            }

            return res;
        }

        static void EntropyCol(int pos, List<DataItem> itemSet)
        {
            var tempDict = new Dictionary<string, double>();
            foreach (var attrbt in itemSet)
            {
                if (tempDict.ContainsKey(attrbt.atributes[pos]))
                {
                    continue;
                }
                else
                {
                    tempDict.Add(attrbt.atributes[pos], Entropy(itemSet, pos, attrbt.atributes[pos], "0"));
                }
            }

            var sum = 0d;

            foreach (var i in tempDict)
            {
                Console.WriteLine(i.Key + " = " + i.Value);
                sum += i.Value;
            }

            Console.WriteLine("Col {0} = {1}", pos, sum / tempDict.Count);
            Console.WriteLine();
        }


        static void Main(string[] args)
        {
            List<DataItem> itemSet = new List<DataItem>();

            // load data
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"iris.csv");
            while ((line = file.ReadLine()) != null)
            {
                if (line != "")
                {
                    var items = line.Split(';').ToArray();
                    itemSet.Add(new DataItem(items));
                    counter++;
                }

            }

            int maxDepth = 4;
            DecisionTree dt = new DecisionTree(3, 4);

            Node root = dt.Evaluate(itemSet);
            root.parrent = null;

            Node currentNode = root;

            for(var i = 0; i < maxDepth; i++)
            {
                Console.Write("IF atr:{0} <= {1}", currentNode.attributeDecision, currentNode.valueDecision);
                var run = true;
                var compRule = currentNode;
                while (run)
                {
                    if(compRule.parrent != null)
                    {
                        Console.Write(" && ");
                        compRule = compRule.parrent;
                        Console.Write("IF atr:{0} > {1}", compRule.attributeDecision, compRule.valueDecision);
                    }
                    else
                    {
                        run = false;
                    }
                }

                Console.WriteLine("\nTHEN class = {0}", currentNode.classSelection);

                var srs = "-1";
                for (var j = 0; j < itemSet.Count; j++)
                {
                    if (double.Parse(itemSet[j].atributes[currentNode.attributeDecision], CultureInfo.InvariantCulture) == currentNode.valueDecision && srs == "-1")
                    {
                        srs = itemSet[j].itemClass;
                        break;
                    }
                }

                for (var j = 0; j < itemSet.Count; j++)
                {
                    if (double.Parse(itemSet[j].atributes[currentNode.attributeDecision], CultureInfo.InvariantCulture) <= currentNode.valueDecision 
                        && itemSet[j].itemClass == srs)
                    {
                        itemSet[j].toBeRemoved = true;
                    }
                }


                /*
                for (var j = 0; j < itemSet.Count; j++)
                {
                    if (double.Parse(itemSet[j].atributes[currentNode.attributeDecision], CultureInfo.InvariantCulture) <= currentNode.valueDecision)
                    {
                        itemSet[j].toBeRemoved = true;
                    }
                }
                */

                itemSet.RemoveAll(x => x.toBeRemoved == true);

                if (itemSet.Count == 0)
                {
                    break;
                }

                var temp = currentNode;
                currentNode.rightNode = dt.Evaluate(itemSet);
                currentNode = currentNode.rightNode;
                currentNode.parrent = temp;
                Console.WriteLine();
            }






            /*
           Console.WriteLine("GiniIndex:\n");
           GiniIndexCol(0, itemSet);
           GiniIndexCol(1, itemSet);    
           Console.WriteLine("----------------------------------------------------------");
           Console.WriteLine("Entropy:\n");
           EntropyCol(0, itemSet);
           EntropyCol(1, itemSet);
           */


        }
    }
}
