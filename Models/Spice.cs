namespace AllSpice.Models
{
  public class Spice
  {
    public int Id { get; set; }
    public int? RecipeId { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public Recipe Recipe { get; set; }
  }
}