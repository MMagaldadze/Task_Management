namespace Domain.Shared
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
