using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    //100% correct 100% performance
    public class PermMissingElem
    {
        public static void Test()
        {
            new PermMissingElem().solution(new int[] { 2, 3, 1, 5 }); 
        }

        public int solution(int[] A)
        {
            bool[] found = new bool[A.Length + 1];

            for (int i = 0; i < A.Length; i++)
            {
                found[A[i] - 1] = true;
            }

            for (int i = 0; i < found.Length; i++)
            {
                if (!found[i])
                    return i + 1;
            }

            return 1;
        }
    }
}
