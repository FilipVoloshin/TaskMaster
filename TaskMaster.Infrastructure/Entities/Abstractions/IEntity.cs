namespace TaskMaster.Infrastructure.Entities.Abstractions
{
    /// <summary>
    /// Represents an entity with a unique identifier of type T.
    /// </summary>
    /// <typeparam name="T">The type of the unique identifier.</typeparam>
    public interface IEntity<T> where T : struct
    {
        /// <summary>
        /// Unique identifier of the entity
        /// </summary>
        T Id { get; set; }
    }
}