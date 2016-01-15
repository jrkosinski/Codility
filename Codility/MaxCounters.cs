using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    //100% correct, 80% performance 
    public class MaxCounters
    {
        public static void Test()
        {
            int[] result;
            result = new MaxCounters().solution(5, new int[] { 3, 4, 4, 6, 1, 4, 4 });
            result = new MaxCounters().solution(5, new int[] { 6, 6, 6, 6, 6, 6, 6 });
        }

        public int[] solution(int N, int[] A)
        {
            int[] output = new int[N];
           // bool[] changed = new bool[N]; 
            int currentMax = 0;
            //int currentVal = 0; 

            for (int n = 0; n < A.Length; n++)
            {
                if (A[n] <= N)
                {
                    //increase 
                    var index = A[n] - 1;
                    increase(index, output);

                    //if (!changed[index])
                    //{
                    //    output[index] = currentVal + 1;
                    //    changed[index] = true;
                    //}
                    //else
                    //    output[index] += 1;

                    if (output[index] > currentMax)
                        currentMax = output[index];
                }
                else
                {
                    //set max
                    setMax(currentMax, output); 
                    //currentVal = currentMax;
                    //changed = new bool[N]; 
                }
            }

            //changed any unchanged to currentMax
            //if (currentMax > 0)
            //{
            //    for (int n = 0; n < output.Length; n++)
            //    {
            //        if (!changed[n])
            //            output[n] = currentVal;
            //    }
            //}

            return output; 
        }

        private void increase(int n, int[] counters)
        {
            counters[n] += 1;
        }

        private void setMax(int max, int[] counters)
        {
            for (int n = 0; n < counters.Length; n++)
            {
                counters[n] = max;
            }
        }
    }
}
