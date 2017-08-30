namespace GOALLogAnalyser.Analyzation.Queries
{
    /// <summary>
    /// Class containing info regarding queries.
    /// </summary>
    public class QueryProfile
    {
        /// <summary>
        /// Gets the amount of times this query succeeded.
        /// </summary>
        /// <value>
        /// The amount of times this query succeeded.
        /// </value>
        public int Hits { get; private set; }
        /// <summary>
        /// Gets the amount of times this query failed.
        /// </summary>
        /// <value>
        /// The amount of times this query failed.
        /// </value>
        public int Misses { get; private set; }
        /// <summary>
        /// Gets the amount of times this query was done.
        /// </summary>
        /// <value>
        /// The amount of times this query was done.
        /// </value>
        public int Times => Hits + Misses;

        /// <summary>
        /// Gets the total time spent doing this query.
        /// </summary>
        /// <value>
        /// The total time spent doing this query.
        /// </value>
        public long TotalTime { get; private set; }
        /// <summary>
        /// Gets the shortest time spent doing this query.
        /// </summary>
        /// <value>
        /// The shortest time spent doing this query.
        /// </value>
        public long ShortestTime { get; private set; }
        /// <summary>
        /// Gets the longest time spent doing this query.
        /// </summary>
        /// <value>
        /// The longest time spent doing this query.
        /// </value>
        public long LongestTime { get; private set; }
        /// <summary>
        /// Gets the average time spent doing this query.
        /// </summary>
        /// <value>
        /// The average time spent doing this query.
        /// </value>
        public long AverageTime => GetAverageTime();

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public string Query { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryProfile"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        public QueryProfile(string query)
        {
            Query = query;
            Hits = 0;
            Misses = 0;
            TotalTime = 0;
            ShortestTime = long.MaxValue;
            LongestTime = long.MinValue;
        }

        private long GetAverageTime()
        {
            return TotalTime / (Hits + Misses);
        }

        /// <summary>
        /// Adds the specified query data.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="hit">if set to <c>true</c> [hit].</param>
        public void Add(long time, bool hit)
        {
            if (hit)
                ++Hits;
            else
                ++Misses;

            TotalTime += time;
            if (ShortestTime > time)
                ShortestTime = time;
            if (LongestTime < time)
                LongestTime = time;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">First object.</param>
        /// <param name="b">Second object.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static QueryProfile operator +(QueryProfile a, QueryProfile b)
        {
            QueryProfile res = new QueryProfile(a.Query)
            {
                Hits = a.Hits + b.Hits,
                Misses = a.Misses + b.Misses,
                TotalTime = a.TotalTime + b.TotalTime
            };

            if (a.ShortestTime < b.ShortestTime)
            {
                res.ShortestTime = a.ShortestTime;
            }
            else
            {
                res.ShortestTime = b.ShortestTime;
            }

            if (a.LongestTime > b.LongestTime)
            {
                res.LongestTime = a.LongestTime;
            }
            else
            {
                res.LongestTime = b.LongestTime;
            }


            return res;
        }
    }
}
