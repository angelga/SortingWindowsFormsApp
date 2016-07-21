using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class Algorithms
    {
        private static List<int> parse(string input)
        {
            List<string> elements = input.Split(',').ToList();
            List<int> numbers = new List<int>();

            for (int i = 0; i < elements.Count; i++)
            {
                if (i+1 == elements.Count && String.IsNullOrWhiteSpace(elements[i]))
                {
                    continue;
                }

                numbers.Add(Int32.Parse(elements[i]));
            }

            return numbers;
        }

        public static List<int> BubbleSort(string input)
        {
            List<int> result = parse(input);
            
            for (int i = 0; i < result.Count; i++)
            {
                bool swapped = false;
                for (int k = result.Count-1; k > i; k--)
                {
                    if (result[k] < result[k - 1])
                    {
                        var temp = result[k];
                        result[k] = result[k - 1];
                        result[k - 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }

            return result;
        }

        public static List<int> SelectionSort(string input)
        {
            List<int> result = parse(input);

            for (int i = 0; i < result.Count-1; i++)
            {
                var smallest_index = i;
                for (int k = i+1; k < result.Count; k++)
                {
                    if (result[k] < result[smallest_index])
                    {
                        smallest_index = k;
                    }
                }

                var temp = result[i];
                result[i] = result[smallest_index];
                result[smallest_index] = temp;
            }

            return result;
        }

        public static List<int> InsertionSort(string input)
        {
            List<int> result = parse(input);

            for (int index = 1; index < result.Count; index++)
            {
                var last = result[index];

                var i = index - 1;

                while (i >= 0 && result[i] > last)
                {
                    result[i + 1] = result[i];
                    i--;
                }

                result[i + 1] = last;
            }

            return result;
        }

        public static List<int> MergeSort(string input)
        {
            return MergeSort(parse(input));
        }

        public static List<int> MergeSort(List<int> input)
        {
            if (input.Count <= 1)
            {
                return input;
            }

            var middle = input.Count / 2;

            var left = MergeSort(input.GetRange(0, middle));
            var right = MergeSort(input.GetRange(middle, (input.Count - middle)));

            return Merge(left, right);
        }

        public static List<int> Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();

            int i = 0;
            int k = 0;

            while (i < left.Count && k < right.Count)
            {
                if (left[i] < right[k])
                {
                    result.Add(left[i++]);
                }
                else
                {
                    result.Add(right[k++]);
                }
            }

            while (i < left.Count)
            {
                result.Add(left[i++]);
            }
            while (k < right.Count)
            {
                result.Add(right[k++]);
            }

            return result;
        }

        public static List<int> LomutoQuickSort(string input)
        {
            var items = parse(input);
            LomutoQuickSort(items, start: 0, end: items.Count - 1);
            return items;
        }

        public static void LomutoQuickSort(List<int> input, int start, int end)
        {
            if (start < end)
            {
                var p = lomutoPartition(input, start, end);
                LomutoQuickSort(input, start, p - 1);
                LomutoQuickSort(input, p + 1, end);
            }
        }

        public static int lomutoPartition(List<int> input, int start, int end)
        {
            var pivot = input[end];
            var indexPivot = start;
            var i = start;
            int temp; 

            while (i < end)
            {
                if (input[i] <= pivot)
                {
                    temp = input[indexPivot];
                    input[indexPivot] = input[i];
                    input[i] = temp;
                    indexPivot++;
                }
                i++;
            }

            temp = input[indexPivot];
            input[indexPivot] = input[end];
            input[end] = temp;

            return indexPivot;
        }

        public static List<int> HoareQuickSort(string input)
        {
            var items = parse(input);
            HoareQuickSort(items, 0, items.Count-1);
            return items;
        }

        public static void HoareQuickSort(List<int> input, int start, int end)
        {
            if (start < end)
            {
                var p = hoarePartition(input, start, end);
                HoareQuickSort(input, start, p);
                HoareQuickSort(input, p + 1, end);
            }
        }

        public static int hoarePartition(List<int> input, int start, int end)
        {
            var pivot = input[start];
            var i = start - 1;
            var k = end + 1;

            while (true)
            {
                i++;
                while (input[i] < pivot)
                {
                    i++;
                }

                k--;
                while (input[k] > pivot)
                {
                    k--;
                }

                if (i >= k)
                {
                    return k;
                }

                var temp = input[i];
                input[i] = input[k];
                input[k] = temp;
            }
        }
    }
}
