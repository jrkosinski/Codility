using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    class Program
    {
        static void Main(string[] args)
        {
            MinMaxDivision.Test(); 

            //List<int> list = new List<int>(new int[] {21, 34, 8, 7, 65, 13, 89, 77, 5, 43, 23, 22, 90, 99, 3, 4});
            //HeapSort.Sort(list, new IntComparer()); 

            //List<int> list = new List<int>(new int[] { 0, 3, 2, 1, 5, 4 });
            //QuickSort.Sort(list, new IntComparer()); 

            ////PermMissingElem.Test(); 
            //Flags.Test();
        }

        class IntComparer : IComparer<int>
        {
            public int Compare(int a, int b)
            {
                return a.CompareTo(b); 
            }
        }
    }
}
