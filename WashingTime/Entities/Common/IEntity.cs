using System;

namespace WashingTime.Entities.Common
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}