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
    }
}
