namespace Paylocity.Deduction.SharedKernel
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get => id!; set => id = value!; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
        private TId? id;
    }

}