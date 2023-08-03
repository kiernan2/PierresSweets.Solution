using System.Collections.Generic;

namespace PierresSweets.Models
{
  public class Treat
  {
    public int TreatId { get; set; }
    public string Name { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
    public Treat()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }
  }
}