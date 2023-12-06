namespace GameZone.Models
{
    public class Category : BaseEntity
    {
        public ICollection<Game> games { get; set;} = new List<Game>();
    }
}
