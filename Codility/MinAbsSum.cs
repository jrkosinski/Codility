using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    //100% correct, 20% performance 
    public class MinAbsSum
    {
        public static void Test()
        {
            int result;

            result = new MinAbsSum().solution(new int[] { 1, 5, 2 });
            List<int> array = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                array.Add(10 * i + 5);
            }
            result = new MinAbsSum().solution(array.ToArray());
            result = new MinAbsSum().solution(new int[] { 15, 25, 35, 45, 55 });
            result = new MinAbsSum().solution(new int[] { 1, 5, 2, -2 , 3});
            result = new MinAbsSum().solution(new int[] { 1, 5, 2, -2 });
            result = new MinAbsSum().solution(new int[] { 1, 5 });
        }


        public int solution(int[] A)
        {
            long output = -1;
            char[] S = String.Empty.PadLeft(A.Length, '0').ToCharArray();

            if (A.Length == 0)
                return 0; 

            TryCombination(A, S, output);
            if (output == 0)
                return 0; 

            while (FindFirstZero(S))
            {
                output = TryCombination(A, S, output);
                if (output == 0)
                    return 0; 
            }

            return (int)output; 
        }

        private bool FindFirstZero(char[] S)
        {
            for (int n = 0; n < S.Length; n++)
            {
                if (S[n] == '0')
                {
                    S[n] = '1';
                    return true;
                }

                S[n] = '0';
            }

            return false;
        }


        /*
        so, we are at 6
        set a 1 

         0 1 2 3 4 5 6
        ----------------
        |0|0|0|1|0|1|0| 
        
        */

        private long TryCombination(int[] A, char[] S, long minSoFar)
        {
            //sets.Add(new String(S));
            long sum = CalculateAbsSum(A, S);

            if (sum < minSoFar || minSoFar < 0)
                minSoFar = (int)sum;

            return minSoFar;
        }

        private long CalculateAbsSum(int[] A, char[] S)
        {
            long sum = 0;
            for (int n = 0; n < A.Length; n++)
            {
                sum += A[n] * (S[n] == '0' ? 1 : -1); 
            }

            return Math.Abs(sum); 
        }
    }
}
