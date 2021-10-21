namespace AllSpice.Models
{
  public class Recipe
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public Profile Writer { get; set; }
  }

}