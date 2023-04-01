namespace TaskMaster.Infrastructure.Entities.Abstractions
{
    /// <summary>
    /// Represents an entity with a unique identifier.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Unique identifier of the entity
        /// </summary>
        Guid Id { get; set; }
    }
}