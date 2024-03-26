namespace HanDesign.Domain
{
    public class Entity<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }
    }
}
