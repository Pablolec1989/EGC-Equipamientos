namespace EGC.Domain.Abstractions
{
    public abstract class EntityBase<TEntityId>
    {
        protected EntityBase(TEntityId id) 
        { 
            Id = id;
        }
        public TEntityId Id { get; protected set; }
    }
}
