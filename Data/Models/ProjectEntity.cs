using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Data.Models;

public class ProjectEntity
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Decimal? Budget { get; set; }
}
