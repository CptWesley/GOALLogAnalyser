namespace GOALLogAnalyser.Analyzation.Cycles
{
    /// <summary>
    /// Struct containing data on a single cycle.
    /// </summary>
    public struct Cycle
    {
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public long Time { get; set; }
        /// <summary>
        /// Gets or sets the amount of actions.
        /// </summary>
        /// <value>
        /// The amount of actions.
        /// </value>
        public int Actions { get; set; }
        /// <summary>
        /// Gets or sets the amount of beliefs.
        /// </summary>
        /// <value>
        /// The amount of beliefs.
        /// </value>
        public int Beliefs { get; set; }
        /// <summary>
        /// Gets or sets the amount of messages sent.
        /// </summary>
        /// <value>
        /// The amount of messages sent.
        /// </value>
        public int MessagesSent { get; set; }
        /// <summary>
        /// Gets or sets the amount of messages received.
        /// </summary>
        /// <value>
        /// The messages amount of received.
        /// </value>
        public int MessagesReceived { get; set; }
        /// <summary>
        /// Gets or sets amount of the queries.
        /// </summary>
        /// <value>
        /// The amount of queries.
        /// </value>
        public int Queries { get; set; }
        /// <summary>
        /// Gets or sets amount of the percepts.
        /// </summary>
        /// <value>
        /// The amount of percepts.
        /// </value>
        public int Percepts { get; set; }
        /// <summary>
        /// Gets or sets amount of the goals.
        /// </summary>
        /// <value>
        /// The amount of goals.
        /// </value>
        public int Goals { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cycle"/> struct.
        /// </summary>
        /// <param name="time">The time.</param>
        public Cycle(long time)
        {
            Time = time;
            Actions = -1;
            Beliefs = -1;
            MessagesSent = -1;
            MessagesReceived = -1;
            Queries = -1;
            Percepts = -1;
            Goals = -1;
        }

    }
}
