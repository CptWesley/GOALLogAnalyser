﻿namespace GOALLogAnalyser.Analyzation
{
    /// <summary>
    /// Static Class containing all different record message types and patterns.
    /// </summary>
    public static class RecordMessageType
    {
        /// <summary>
        /// The unknown type.
        /// </summary>
        public const uint UnknownType = 0;
        /// <summary>
        /// The module entry type.
        /// </summary>
        public const uint ModuleEntryType = 1;
        /// <summary>
        /// The module exit type.
        /// </summary>
        public const uint ModuleExitType = 2;
        /// <summary>
        /// The cycle statistics type.
        /// </summary>
        public const uint CycleStatisticsType = 3;
        /// <summary>
        /// The query success type.
        /// </summary>
        public const uint QuerySuccessType = 4;
        /// <summary>
        /// The query failure type.
        /// </summary>
        public const uint QueryFailureType = 5;

        /// <summary>
        /// The module entry pattern.
        /// </summary>
        public const string ModuleEntryPattern = @"entered '(.+)' with \[(.*)\]\.";
        /// <summary>
        /// The module exit pattern.
        /// </summary>
        public const string ModuleExitPattern = @"exited '(.+)'\.";
        /// <summary>
        /// The cycle statistics pattern.
        /// </summary>
        public const string CycleStatisticsPattern = @"non-state actions: (\d+), send actions (\d+), state queries: (\d+), total\[beliefs: (\d+), goals: (\d+), messages: (\d+), percepts: (\d+)\]|(\s+)?\+{7}\s+Cycle (\d+)\s+\+{7}";
        /// <summary>
        /// The query success pattern.
        /// </summary>
        public const string QuerySuccessPattern = @"(?s)condition of '(.+)' holds for: \[(.+)\]\.";
        /// <summary>
        /// The query failure pattern.
        /// </summary>
        public const string QueryFailurePattern = @"(?s)condition of '(.+)' does not hold\.";
    }
}
