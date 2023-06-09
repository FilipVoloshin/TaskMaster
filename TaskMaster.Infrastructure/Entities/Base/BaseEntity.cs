﻿using TaskMaster.Infrastructure.Entities.Abstractions;

namespace TaskMaster.Infrastructure.Entities.Base
{
    /// <summary>
    /// Represents the base entity for all entities in the application. Implements the IEntity interface with Guid as the identifier type.
    /// </summary>
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
