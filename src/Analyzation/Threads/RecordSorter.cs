using System.Collections.Generic;
using GOALLogAnalyser.Parsing;

namespace GOALLogAnalyser.Analyzation.Threads
{
    /// <summary>
    /// Class that sorts records.
    /// </summary>
    public class RecordSorter
    {
        private List<Record> _list;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordSorter"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        public RecordSorter(List<Record> input)
        {
            _list = input;
        }

        /// <summary>
        /// Sorts this instance using in-place quicksort.
        /// </summary>
        public void Sort()
        {
            if (_list.Count <= 0)
                return;
            Sort(0, _list.Count - 1);
        }

        /// <summary>
        /// Sorts the instance in-place using quick sort between the start and end points.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        private void Sort(int start, int end)
        {
            if (start >= end)
                return;

            int li = start; // left index
            int ri = end; // right index

            long pivotVal = _list[end].Sequence;

            do
            {
                // Skip records that are already correct.
                while (_list[li].Sequence < pivotVal)
                    ++li;
                while (_list[ri].Sequence > pivotVal)
                    --ri;

                if (li <= ri)
                {
                    Swap(li, ri);
                    ++li;
                    --ri;
                }

            } while (li < ri);

            if (start < ri)
                Sort(start, ri);

            if (end > li)
                Sort(li, end);

        }

        /// <summary>
        /// Swaps the the element at a and b around in the list.
        /// </summary>
        /// <param name="a">Index a.</param>
        /// <param name="b">Index b.</param>
        private void Swap(int a, int b)
        {
            Record temp = _list[a];
            _list[a] = _list[b];
            _list[b] = temp;
        }
    }
}
