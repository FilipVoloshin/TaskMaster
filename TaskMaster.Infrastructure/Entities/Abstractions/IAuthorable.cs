namespace TaskMaster.Infrastructure.Entities.Abstractions
{
    /// <summary>
    /// Represents an entity that has an author.
    /// </summary>
    public interface IAuthorable
    {
        /// <summary>
        /// Gets or sets the identifier of the author.
        /// </summary>
        public Guid AuthorId { get; set; }
    }
}
