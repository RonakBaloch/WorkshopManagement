using System;
using System.Collections.Generic;

namespace Workshop.Models;

public partial class CustomerVehicleDetail
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? VehicleId { get; set; }

    public string? VehicleNo { get; set; }

    public DateTime? ServiceDueDate { get; set; }

    public bool? IsServiceDone { get; set; }

    public virtual CustomerDetail? Customer { get; set; }

    public virtual VehicleDetail? Vehicle { get; set; }
}
