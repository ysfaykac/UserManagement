namespace UserManagement.Domain.Abstract;

public interface ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
}