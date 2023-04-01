﻿namespace TaskMaster.Shared
{
    /// <summary>
    /// Contains application-wide constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Contains constants related to HTTP headers.
        /// </summary>
        public static class HttpHeaders
        {
            /// <summary>
            /// The header used to pass the User ID in HTTP requests.
            /// </summary>
            public const string UserIdHeader = "TM-User-Id";
        }

        /// <summary>
        /// Defines commonly used data types in PostgreSQL for Entity Framework Core mappings.
        /// </summary>
        public static class PostgresDataTypes
        {
            /// <summary>
            /// Type, used for date time in current database scheme
            /// </summary>
            public const string DateTimeType = "timestamp with time zone";
        }
    }
}
