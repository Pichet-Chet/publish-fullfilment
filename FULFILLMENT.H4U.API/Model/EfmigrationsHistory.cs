using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class EfmigrationsHistory
{
    public string MigrationId { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
