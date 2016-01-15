using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    //100% correct 40% performance
    public class ArrayInversionCount
    {
        public static void Test()
        {
            int result; 
            result = new ArrayInversionCount().solution(new int[] { -1, 6, 3, 4, 7, 4 });
        }

        private const int Limit = 1000000000; 

        public int solution(int[] A)
        {
            int output = 0;
            int index = 0;

            while(index < A.Length)
            {
                output += CountInversions(index, A);
                if (output >= Limit)
                    return -1;

                index++; 
            }

            return output; 
        }

        private int CountInversions(int start, int[] A)
        {
            int output = 0; 
            for (int i = start+1; i < A.Length; i++)
            {
                if (A[start] > A[i])
                    output++; 
            }

            return output; 
        }
    }
}
