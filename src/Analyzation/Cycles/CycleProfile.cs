using System.Collections.Generic;

namespace GOALLogAnalyser.Analyzation.Cycles
{
    /// <summary>
    /// Class containing performance data of multiple cycles.
    /// </summary>
    public class CycleProfile
    {
        /// <summary>
        /// Gets the amount of cycles.
        /// </summary>
        /// <value>
        /// The amount of cycles.
        /// </value>
        public int Count => _cycles.Count;
        /// <summary>
        /// Gets the total time.
        /// </summary>
        /// <value>
        /// The total time.
        /// </value>
        public long TotalTime => GetTotalTime();
        /// <summary>
        /// Gets the average time of a cycle.
        /// </summary>
        /// <value>
        /// The average time of a cycle.
        /// </value>
        public double AverageTime => GetAverageTime();
        /// <summary>
        /// Gets the lowest time of a cycle.
        /// </summary>
        /// <value>
        /// The lowest time of a cycle.
        /// </value>
        public long LowestTime => GetLowestTime();
        /// <summary>
        /// Gets the highest time of a cycle.
        /// </summary>
        /// <value>
        /// The highest time of a cycle.
        /// </value>
        public long HighestTime => GetHighestTime();

        /// <summary>
        /// Gets the total amount of actions.
        /// </summary>
        /// <value>
        /// The total amount of actions.
        /// </value>
        public int TotalActions => GetTotalActions();
        /// <summary>
        /// Gets the average amount of actions of a cycle.
        /// </summary>
        /// <value>
        /// The average amount of actions of a cycle.
        /// </value>
        public double AverageActions => GetAverageActions();
        /// <summary>
        /// Gets the lowest amount of actions of a cycle.
        /// </summary>
        /// <value>
        /// The lowest amount of actions of a cycle.
        /// </value>
        public int LowestActions => GetLowestActions();
        /// <summary>
        /// Gets the highest amount of actions of a cycle.
        /// </summary>
        /// <value>
        /// The highest amount of actions of a cycle.
        /// </value>
        public int HighestActions => GetHighestActions();

        /// <summary>
        /// Gets the total amount of beliefs.
        /// </summary>
        /// <value>
        /// The total amount of beliefs.
        /// </value>
        public int TotalBeliefs => GetTotalBeliefs();
        /// <summary>
        /// Gets the average amount of beliefs of a cycle.
        /// </summary>
        /// <value>
        /// The average amount of beliefs of a cycle.
        /// </value>
        public double AverageBeliefs => GetAverageBeliefs();
        /// <summary>
        /// Gets the lowest amount of beliefs of a cycle.
        /// </summary>
        /// <value>
        /// The lowest amount of beliefs of a cycle.
        /// </value>
        public int LowestBeliefs => GetLowestBeliefs();
        /// <summary>
        /// Gets the highest amount of beliefs of a cycle.
        /// </summary>
        /// <value>
        /// The highest amount of beliefs of a cycle.
        /// </value>
        public int HighestBeliefs => GetHighestBeliefs();

        /// <summary>
        /// Gets the total amount of queries.
        /// </summary>
        /// <value>
        /// The total amount of queries.
        /// </value>
        public int TotalQueries => GetTotalQueries();
        /// <summary>
        /// Gets the average amount of queries of a cycle.
        /// </summary>
        /// <value>
        /// The average amount of queries of a cycle.
        /// </value>
        public double AverageQueries => GetAverageQueries();
        /// <summary>
        /// Gets the lowest amount of queries of a cycle.
        /// </summary>
        /// <value>
        /// The lowest amount of queries of a cycle.
        /// </value>
        public int LowestQueries => GetLowestQueries();
        /// <summary>
        /// Gets the highest amount of queries of a cycle.
        /// </summary>
        /// <value>
        /// The highest amount of queries of a cycle.
        /// </value>
        public int HighestQueries => GetHighestQueries();

        /// <summary>
        /// Gets the total amount of percepts.
        /// </summary>
        /// <value>
        /// The total amount of percepts.
        /// </value>
        public int TotalPercepts => GetTotalPercepts();
        /// <summary>
        /// Gets the average amount of percepts of a cycle.
        /// </summary>
        /// <value>
        /// The average amount of percepts of a cycle.
        /// </value>
        public double AveragePercepts => GetAveragePercepts();
        /// <summary>
        /// Gets the lowest amount of percepts of a cycle.
        /// </summary>
        /// <value>
        /// The lowest amount of percepts of a cycle.
        /// </value>
        public int LowestPercepts => GetLowestPercepts();
        /// <summary>
        /// Gets the highest amount of percepts of a cycle.
        /// </summary>
        /// <value>
        /// The highest amount of percepts of a cycle.
        /// </value>
        public int HighestPercepts => GetHighestPercepts();

        /// <summary>
        /// Gets the total amount of goals.
        /// </summary>
        /// <value>
        /// The total amount of goals.
        /// </value>
        public int TotalGoals => GetTotalGoals();
        /// <summary>
        /// Gets the average amount of goals of a cycle.
        /// </summary>
        /// <value>
        /// The average amount of goals of a cycle.
        /// </value>
        public double AverageGoals => GetAverageGoals();
        /// <summary>
        /// Gets the lowest amount of goals of a cycle.
        /// </summary>
        /// <value>
        /// The lowest amount of goals of a cycle.
        /// </value>
        public int LowestGoals => GetLowestGoals();
        /// <summary>
        /// Gets the highest amount of goals of a cycle.
        /// </summary>
        /// <value>
        /// The highest amount of goals of a cycle.
        /// </value>
        public int HighestGoals => GetHighestGoals();

        /// <summary>
        /// Gets the total amount of messages received.
        /// </summary>
        /// <value>
        /// The total amount of messages received.
        /// </value>
        public int TotalMessagesReceived => GetTotalMessagesReceived();
        /// <summary>
        /// Gets the average amount of messages received of a cycle.
        /// </summary>
        /// <value>
        /// The average amount of messages received of a cycle.
        /// </value>
        public double AverageMessagesReceived => GetAverageMessagesReceived();
        /// <summary>
        /// Gets the lowest amount of messages received of a cycle.
        /// </summary>
        /// <value>
        /// The lowest amount of messages received of a cycle.
        /// </value>
        public int LowestMessagesReceived => GetLowestMessagesReceived();
        /// <summary>
        /// Gets the highest amount of messages received of a cycle.
        /// </summary>
        /// <value>
        /// The highest amount of messages received of a cycle.
        /// </value>
        public int HighestMessagesReceived => GetHighestMessagesReceived();

        /// <summary>
        /// Gets the amount of total messages sent.
        /// </summary>
        /// <value>
        /// The total amount of messages sent.
        /// </value>
        public int TotalMessagesSent => GetTotalMessagesSent();
        /// <summary>
        /// Gets the average amount of messages sent of a cycle.
        /// </summary>
        /// <value>
        /// The average amount of messages sent of a cycle.
        /// </value>
        public double AverageMessagesSent => GetAverageMessagesSent();
        /// <summary>
        /// Gets the lowest amount of messages sent of a cycle.
        /// </summary>
        /// <value>
        /// The lowest amount of messages sent of a cycle.
        /// </value>
        public int LowestMessagesSent => GetLowestMessagesSent();
        /// <summary>
        /// Gets the highest amount of messages sent of a cycle.
        /// </summary>
        /// <value>
        /// The highest amount of messages sent of a cycle.
        /// </value>
        public int HighestMessagesSent => GetHighestMessagesSent();

        private List<Cycle> _cycles;

        /// <summary>
        /// Initializes a new instance of the <see cref="CycleProfile"/> class.
        /// </summary>
        public CycleProfile()
        {
            _cycles = new List<Cycle>();
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static CycleProfile operator +(CycleProfile a, CycleProfile b)
        {
            CycleProfile res = new CycleProfile();
            foreach (Cycle c in a._cycles)
            {
                res.Add(c);
            }
            foreach (Cycle c in b._cycles)
            {
                res.Add(c);
            }
            return res;
        }

        /// <summary>
        /// Adds the specified cycle.
        /// </summary>
        /// <param name="cycle">The cycle.</param>
        public void Add(Cycle cycle)
        {
            _cycles.Add(cycle);
        }

        private long GetTotalTime()
        {
            long res = 0;

            foreach (Cycle c in _cycles)
            {
                res += c.Time;
            }

            return res;
        }

        private double GetAverageTime()
        {
            if (_cycles.Count == 0)
                return 0;
            return GetTotalTime() / _cycles.Count;
        }

        private long GetLowestTime()
        {
            if (_cycles.Count == 0)
                return 0;

            long res = long.MaxValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Time < res)
                    res = c.Time;
            }

            return res;
        }

        private long GetHighestTime()
        {
            if (_cycles.Count == 0)
                return 0;

            long res = long.MinValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Time > res)
                    res = c.Time;
            }

            return res;
        }

        private int GetTotalActions()
        {
            int res = 0;

            foreach (Cycle c in _cycles)
            {
                res += c.Actions;
            }

            return res;
        }

        private double GetAverageActions()
        {
            if (_cycles.Count == 0)
                return 0;
            return GetTotalActions() / _cycles.Count;
        }

        private int GetLowestActions()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MaxValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Actions < res)
                    res = c.Actions;
            }

            return res;
        }

        private int GetHighestActions()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MinValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Actions > res)
                    res = c.Actions;
            }

            return res;
        }

        private int GetTotalBeliefs()
        {
            int res = 0;

            foreach (Cycle c in _cycles)
            {
                res += c.Beliefs;
            }

            return res;
        }

        private double GetAverageBeliefs()
        {
            if (_cycles.Count == 0)
                return 0;
            return GetTotalBeliefs() / _cycles.Count;
        }

        private int GetLowestBeliefs()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MaxValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Beliefs < res)
                    res = c.Beliefs;
            }

            return res;
        }

        private int GetHighestBeliefs()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MinValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Beliefs > res)
                    res = c.Beliefs;
            }

            return res;
        }

        private int GetTotalQueries()
        {
            int res = 0;

            foreach (Cycle c in _cycles)
            {
                res += c.Queries;
            }

            return res;
        }

        private double GetAverageQueries()
        {
            if (_cycles.Count == 0)
                return 0;
            return GetTotalQueries() / _cycles.Count;
        }

        private int GetLowestQueries()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MaxValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Queries < res)
                    res = c.Queries;
            }

            return res;
        }

        private int GetHighestQueries()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MinValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Queries > res)
                    res = c.Queries;
            }

            return res;
        }

        private int GetTotalPercepts()
        {
            int res = 0;

            foreach (Cycle c in _cycles)
            {
                res += c.Percepts;
            }

            return res;
        }

        private double GetAveragePercepts()
        {
            if (_cycles.Count == 0)
                return 0;
            return GetTotalPercepts() / _cycles.Count;
        }

        private int GetLowestPercepts()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MaxValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Percepts < res)
                    res = c.Percepts;
            }

            return res;
        }

        private int GetHighestPercepts()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MinValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Percepts > res)
                    res = c.Percepts;
            }

            return res;
        }

        private int GetTotalGoals()
        {
            int res = 0;

            foreach (Cycle c in _cycles)
            {
                res += c.Goals;
            }

            return res;
        }

        private double GetAverageGoals()
        {
            if (_cycles.Count == 0)
                return 0;
            return GetTotalGoals() / _cycles.Count;
        }

        private int GetLowestGoals()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MaxValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Goals < res)
                    res = c.Goals;
            }

            return res;
        }

        private int GetHighestGoals()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MinValue;
            foreach (Cycle c in _cycles)
            {
                if (c.Goals > res)
                    res = c.Goals;
            }

            return res;
        }

        private int GetTotalMessagesSent()
        {
            int res = 0;

            foreach (Cycle c in _cycles)
            {
                res += c.MessagesSent;
            }

            return res;
        }

        private double GetAverageMessagesSent()
        {
            if (_cycles.Count == 0)
                return 0;
            return GetTotalMessagesSent() / _cycles.Count;
        }

        private int GetLowestMessagesSent()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MaxValue;
            foreach (Cycle c in _cycles)
            {
                if (c.MessagesSent < res)
                    res = c.MessagesSent;
            }

            return res;
        }

        private int GetHighestMessagesSent()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MinValue;
            foreach (Cycle c in _cycles)
            {
                if (c.MessagesSent > res)
                    res = c.MessagesSent;
            }

            return res;
        }

        private int GetTotalMessagesReceived()
        {
            int res = 0;

            foreach (Cycle c in _cycles)
            {
                res += c.MessagesReceived;
            }

            return res;
        }

        private double GetAverageMessagesReceived()
        {
            if (_cycles.Count == 0)
                return 0;
            return GetTotalMessagesReceived() / _cycles.Count;
        }

        private int GetLowestMessagesReceived()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MaxValue;
            foreach (Cycle c in _cycles)
            {
                if (c.MessagesReceived < res)
                    res = c.MessagesReceived;
            }

            return res;
        }

        private int GetHighestMessagesReceived()
        {
            if (_cycles.Count == 0)
                return 0;

            int res = int.MinValue;
            foreach (Cycle c in _cycles)
            {
                if (c.MessagesReceived > res)
                    res = c.MessagesReceived;
            }

            return res;
        }
    }
}
