﻿namespace HanDesign.Domain
{
    public interface IEntity
    {

    }
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }
}
