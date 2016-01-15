using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    //100% correct, 28% performance 
    public class Flags
    {
        public static void Test()
        {
            int result;
            //3
            result = new Flags().solution(new int[] { 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 });

            //2
            result = new Flags().solution(new int[] { 0, 1, 0, 1, 0, 1, 0 });

            //2
            result = new Flags().solution(new int[] { 0, 1, 0, 0, 1, 0, 1, 0 });

            //3
            result = new Flags().solution(new int[] { 0, 1, 0, 0, 1, 0, 1, 0, 1, 0 });
        }

        public int solution(int[] A)
        {
            //count the number of peaks 
            List<int> peaks = GetPeaks(A); 

            //count the minimum & maximum distances between peaks 
            var numFlags = peaks.Count;
            int maxFlags = 0;

            //number of flags 
            while (numFlags > 0)
            {
                var max = CountFlags(numFlags, peaks);

                if (max > maxFlags)
                    maxFlags = max;

                numFlags--;

                //if the number of flags we're bringing is already less than the max, no point in continuing 
                if (numFlags <= maxFlags)
                    break;
            }

            return maxFlags;
        }

        private List<int> GetPeaks(int[] A)
        {
            List<int> output = new List<int>();

            for (int n = 1; n < A.Length-1; n++)
            {
                if (A[n] > A[n - 1] && A[n] > A[n + 1])
                {
                    output.Add(n);
                    n++;
                }
            }

            return output;
        }

        private int CountFlags(int numFlags, List<int> peaks)
        {
            int output = 0;

            if (peaks.Count > 0)
            {
                int peakIndex = 0;

                while (output < numFlags)
                {
                    output++;

                    //get next flag; must be [numFlags] points away 
                    int nextPossibleFlag = peaks[peakIndex] + numFlags;
                    bool foundNext = false;
                    for (int n = peakIndex + 1; n < peaks.Count; n++)
                    {
                        if (peaks[n] >= nextPossibleFlag)
                        {
                            peakIndex = n;
                            foundNext = true; 
                            break;
                        }
                    }

                    if (!foundNext)
                        return output; 
                }
            }

            return output; 
        }
    }
}
