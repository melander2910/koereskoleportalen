namespace BackOffice.API.Models.Abstracts;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    // public string CreatedBy { get; set; }
    // public string ModifiedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}