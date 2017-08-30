using System.Collections.Generic;
using GOALLogAnalyser.Parsing;

namespace GOALLogAnalyser.Analyzation.Threads
{
    public class RecordFinder
    {
        public Dictionary<int, List<Record>> Threads { get; }

        public RecordFinder(Dictionary<int, List<Record>> threads)
        {
            Threads = threads;
        }

        public int IndexOf(int thread, long sequence)
        {
            List<Record> records = Threads[thread];

            return IndexOf(records, 0, records.Count - 1, sequence);
        }

        private int IndexOf(List<Record> records, int start, int end, long sequence)
        {
            if (start > end)
                return -1;
            int pivot = (end - start) / 2 + start;
            long pivotSequence = records[pivot].Sequence;

            if (pivotSequence == sequence)
                return pivot;
            if (pivotSequence < sequence)
                return IndexOf(records, pivot+1, end, sequence);
            if (pivotSequence > sequence)
                return IndexOf(records, start, pivot-1, sequence);
            return -1;
        }
    }
}
