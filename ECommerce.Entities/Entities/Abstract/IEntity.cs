namespace ECommerce.Entities.Entities.Abstract;

public interface IEntity
{
    public bool IsDeleted { get; set; }
    public int CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedUserId { get; set; }
    public DateTime? UpdatedDate { get; set; }
}