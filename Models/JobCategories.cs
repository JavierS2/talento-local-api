namespace TalentoLocal.Models
{
    public class JobCategories
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //Relationship with Offer m : n
        public List<Offer>? Offers { get; set; } 
    }
}
