namespace GOALLogAnalyser.Analyzation.Modules
{
    /// <summary>
    /// Class containing the performance data of a single module.
    /// </summary>
    public class ModuleProfile
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
        /// <summary>
        /// Gets the number of executions.
        /// </summary>
        /// <value>
        /// The number of executions.
        /// </value>
        public int Executions { get; private set; }
        /// <summary>
        /// Gets the total execution time.
        /// </summary>
        /// <value>
        /// The total execution time.
        /// </value>
        public long TotalExecutionTime { get; private set; }
        /// <summary>
        /// Gets the shortest execution time.
        /// </summary>
        /// <value>
        /// The shortest execution time.
        /// </value>
        public long ShortestExecutionTime { get; private set; }
        /// <summary>
        /// Gets the longest execution time.
        /// </summary>
        /// <value>
        /// The longest execution time.
        /// </value>
        public long LongestExecutionTime { get; private set; }
        /// <summary>
        /// Gets the average execution time.
        /// </summary>
        /// <value>
        /// The average execution time.
        /// </value>
        public double AverageExecutionTime => GetAverageExecutionTime();

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleProfile"/> class.
        /// </summary>
        /// <param name="name">The module name.</param>
        public ModuleProfile(string name)
        {
            Name = name;
            Executions = 0;
            TotalExecutionTime = 0;
            ShortestExecutionTime = long.MaxValue;
            LongestExecutionTime = long.MinValue;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">First object.</param>
        /// <param name="b">Second object.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static ModuleProfile operator +(ModuleProfile a, ModuleProfile b)
        {
            ModuleProfile res = new ModuleProfile(a.Name)
            {
                Executions = a.Executions + b.Executions,
                TotalExecutionTime = a.TotalExecutionTime + b.TotalExecutionTime
            };

            if (a.ShortestExecutionTime < b.ShortestExecutionTime)
            {
                res.ShortestExecutionTime = a.ShortestExecutionTime;
            }
            else
            {
                res.ShortestExecutionTime = b.ShortestExecutionTime;
            }

            if (a.LongestExecutionTime > b.LongestExecutionTime)
            {
                res.LongestExecutionTime = a.LongestExecutionTime;
            }
            else
            {
                res.LongestExecutionTime = b.LongestExecutionTime;
            }


            return res;
        }

        /// <summary>
        /// Adds an execution.
        /// </summary>
        /// <param name="executionTime">The execution time.</param>
        public void AddExecution(long executionTime)
        {
            ++Executions;
            TotalExecutionTime += executionTime;
            if (ShortestExecutionTime > executionTime)
                ShortestExecutionTime = executionTime;
            if (LongestExecutionTime < executionTime)
                LongestExecutionTime = executionTime;
        }

        /// <summary>
        /// Gets the average execution time.
        /// </summary>
        /// <returns></returns>
        public double GetAverageExecutionTime()
        {
            if (Executions == 0)
                return 0;
            return TotalExecutionTime / Executions;
        }
    }
}
