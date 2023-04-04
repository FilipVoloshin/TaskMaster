namespace TaskMaster.Api.Models
{
    /// <summary>
    /// Represents a record containing information about a route.
    /// </summary>
    public record RouteInfo(string HttpMethod, string Endpoint)
    {
        /// <summary>
        /// Gets or sets the description of the route.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Gets or sets the summary of the route.
        /// </summary>
        public string? Summary { get; init; }

        /// <summary>
        /// Gets or sets the tag of the route.
        /// </summary>
        public string? Tag { get; init; }
    }
}
