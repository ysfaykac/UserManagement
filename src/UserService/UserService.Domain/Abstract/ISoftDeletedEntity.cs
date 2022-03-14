namespace UserService.Domain.Abstract;

public interface ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
}