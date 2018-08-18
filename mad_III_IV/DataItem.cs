using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mad_III_IV
{
    internal class DataItem
    {
        internal List<string> atributes;
        internal string itemClass;
        internal bool toBeRemoved = false;

        public DataItem(string[] atr)
        {
            //int.TryParse(atr[atr.Length-1], out this.itemClass);
            this.itemClass = atr[atr.Length - 1];
            this.atributes = new List<string>();

            for(var i = 0; i < atr.Length - 1; i++)
            {
                this.atributes.Add(atr[i]);
            }

        }
    }
}
