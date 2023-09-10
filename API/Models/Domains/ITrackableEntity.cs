namespace API.Models.Entities
{
    public interface ITrackableEntity
    {
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
    }
}