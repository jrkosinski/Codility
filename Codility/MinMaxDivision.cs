using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    //100% correct, 28% performance 
    public class MinMaxDivision
    {
        private long minLargeSum = Int64.MaxValue;
        private Dictionary<string, long> storedBlockSums = new Dictionary<string, long>(); 

        public static void Test()
        {
            int result;
            //3
            result = new MinMaxDivision().solution(3, 5, new int[]{2,1,5,1,2,2,2});
        }

        public int solution(int K, int M, int[] A)
        {
            //we can divide the array into K groups. each group can be from size 0-N. sum of all group sizes must equal N 
            //each item has a max value of M and is positive (can be zero) 
            /*
            0 0 5 
            1 0 4
            0 1 4 
            1 1 3 
            2 0 3 
            ...etc. */
            if (A.Length == 0)
                return 0; 

            minLargeSum = Int64.MaxValue;
            storedBlockSums.Clear(); 

            int[] blockSizes = new int[K]; 
            TryAll(blockSizes, A, M, 0, 0);
            return (int)minLargeSum;
        }

        private void TryAll(int[] blockSizes, int[] A, int max, int index, int currentSum)
        {
            if (index == blockSizes.Length - 1)
            {
                blockSizes[index] = (A.Length - currentSum);
                //ShowArray(blockSizes, A.Length);
                long largeSum = GetLargeSum(A, blockSizes);

                if (largeSum < minLargeSum)
                    minLargeSum = largeSum; 
            }
            else
            {
                for (int n = 0; n < A.Length; n++)
                {
                    if (currentSum + n >= A.Length)
                        break;

                    blockSizes[index] = n;
                    TryAll(blockSizes, A, max, index + 1, currentSum + n);
                }
            }
        }

        private long GetLargeSum(int[] A, int[] blockSizes)
        {
            long maxBlockSum = 0; 
            int currentIndex = 0;

            for (int n = 0; n < blockSizes.Length; n++)
            {
                long blockSum = CalculateAndStoreBlockSum(A, currentIndex, blockSizes[n]); 

                currentIndex += blockSizes[n]; 

                if (blockSum > maxBlockSum)
                    maxBlockSum = blockSum;

                blockSum = 0; 
            }

            return maxBlockSum;
        }

        private long CalculateAndStoreBlockSum(int[] A, int start, int length)
        {
            string key = String.Format("{0},{1}", start, length); ;

            if (this.storedBlockSums.ContainsKey(key))
                return storedBlockSums[key];
            else
            {
                long sum = 0; 
                for (int n = start; n < (start+length); n++)
                {
                    sum += A[n]; 
                }
                storedBlockSums.Add(key, sum);

                return sum;
            }
        }

        /*
        private long GetSum(int[] array)
        {
            long output = 0;
            foreach (int i in array)
                output += i;

            return output; 
        }

        private void ShowArray(int[] array, int expectedSum)
        {
            if (GetSum(array) != expectedSum)
            {
                throw new Exception("Expected sum not met."); 
            }

            StringBuilder sb = new StringBuilder();
            bool start = true;
            foreach (int i in array)
            {
                if (!start)
                    sb.Append(", ");
                else
                    start = false;

                sb.Append(i);
            }

            Console.WriteLine(sb.ToString()); 
        }
         * */
    }
}
