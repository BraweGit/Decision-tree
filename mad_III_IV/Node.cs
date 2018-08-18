using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mad_III_IV
{
    class Node
    {
        public Node rightNode;
        public Node parrent;
        public int attributeDecision;
        public double valueDecision;
        public string classSelection;

        public Node(int aDecision, double vDecision, string cDetection)
        {
            attributeDecision = aDecision;
            valueDecision = vDecision;
            classSelection = cDetection;
        }
    }
}
