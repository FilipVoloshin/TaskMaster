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

        /// <summary>
        /// Static class containing constants for pagination.
        /// </summary>
        public static class Pagination
        {
            /// <summary>
            /// The default page number used for pagination.
            /// </summary>
            public const int DefaultPageNumber = 1;

            /// <summary>
            /// The default page size used for pagination.
            /// </summary>
            public const int DefaultPageSize = 10;
        }

        public static class ResponseMessages
        {
            /// <summary>
            /// The request was successful and the expected data is returned
            /// </summary>
            public const string Ok = "The request was successful.";

            /// <summary>
            /// The request was invalid or cannot be served
            /// </summary>
            public const string BadRequest = "The request was invalid or cannot be served.";

            /// <summary>
            /// The server successfully processed the request but there is no response body to return
            /// </summary>
            public const string NoContent = "The server successfully processed the request but there is no response body to return.";

            /// <summary>
            /// The server encountered an unexpected error while processing the request
            /// </summary>
            public const string InternalServerError = "The server encountered an unexpected error while processing the request.";
        }
    }
}
