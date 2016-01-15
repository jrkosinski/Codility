using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class QuickSort
    {
        public static void Sort<T>(List<T> input, IComparer<T> comparer)
        {
            Sort(input, 0, input.Count - 1, comparer); 
        }

        private static void Sort<T>(List<T> input, int left, int right, IComparer<T> comparer)
        {
            int min = (left + right) / 2;

            int i = left;
            int j = right;
            T pivot = input[min];

            while (left < j || i < right)
            {
                while (comparer.Compare(input[i], pivot) < 0)
                    i++;
                while (comparer.Compare(input[j], pivot) > 0)
                    j--;

                if (i <= j)
                {
                    Swap(i, j, input);
                    i++;
                    j--;
                }
                else
                {
                    if (left < j)
                        Sort(input, left, j, comparer);
                    if (i < right)
                        Sort(input, i, right, comparer);
                    return;
                }
            }
        }

        private static void Swap<T>(int i, int j, List<T> input)
        {
            var temp = input[i];
            input[i] = input[j];
            input[j] = temp;
        }
    }
}
