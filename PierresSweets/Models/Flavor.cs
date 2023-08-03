using System.Collections.Generic;

namespace PierresSweets.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }
    public string Name { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
    public Flavor()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }
  }
}