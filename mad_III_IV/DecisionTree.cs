using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mad_III_IV
{
    class DecisionTree
    {
        int classes = 0;
        int attributes = 0;

        public DecisionTree(int classCount, int attributeCount)
        {
            classes = classCount;
            attributes = attributeCount;
        }

        public Node Evaluate(List<DataItem> itemSet)
        {
            List<int> classesCount = new List<int>();
            for (var i = 0; i < classes; i++)
            {
                classesCount.Add(0);
            }
            // add number of classes 
            foreach (var item in itemSet)
            {
                classesCount[int.Parse(item.itemClass)]++;
            }

            /*
            Console.WriteLine(classesCount[0]);
            Console.WriteLine(classesCount[1]);
            Console.WriteLine(classesCount[2]);
            */

            // iterate every attribute to find best match

            double bestSelection = 0;
            int attributeSelected = 0;
            double lastValueCheck = 0;
            double bestLastValue = 0;
            List<DataItem> SortedList = null;

            for (var i = 0; i < attributes; i++)
            {
                List<DataItem> first = new List<DataItem>();
                foreach (var item in itemSet)
                {
                    first.Add(new DataItem(new string[] { item.atributes[i], item.itemClass }));

                }
                SortedList = first.OrderBy(x => double.Parse(x.atributes[0], CultureInfo.InvariantCulture)).ToList();

                /*
                Console.WriteLine("ATTR " + i);
                foreach(var item in SortedList)
                {
                    Console.WriteLine(item.atributes[0] + "    " +item.itemClass);
                }
                Console.WriteLine();
                */

                var searchedClass = SortedList[0].itemClass;
                double internalCounterClass = 0;
                double allObj = 0;
                //Console.WriteLine();
                foreach (var item in SortedList)
                {
                    //Console.WriteLine(item.atributes[0] + "   " + item.itemClass);
                    if (searchedClass == item.itemClass)
                    {
                        //Console.WriteLine(internalCounterClass);
                        internalCounterClass++;
                        lastValueCheck = double.Parse(item.atributes[0], CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        break;
                    }

                    allObj++;
                }

                if (bestSelection <= internalCounterClass)
                {
                    bestLastValue = lastValueCheck;
                    attributeSelected = i;
                    bestSelection = internalCounterClass;
                }

                //Console.WriteLine(internalCounterClass);

            }

            
            //Console.WriteLine(bestLastValue);
            //Console.WriteLine(attributeSelected);
            

            return new Node(attributeSelected, bestLastValue, SortedList[0].itemClass);
        }






    }
}
