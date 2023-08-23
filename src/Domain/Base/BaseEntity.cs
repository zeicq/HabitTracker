namespace Domain.Base;

public abstract class BaseEntity
{
    public string CreatedBy { get; protected set; }
    public DateTime Created { get; protected set; }
    public string LastModifiedBy { get; protected set; }
    public DateTime? LastModified { get; protected set; }
}