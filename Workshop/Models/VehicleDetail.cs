using System;
using System.Collections.Generic;

namespace Workshop.Models;

public partial class VehicleDetail
{
    public int VehicleId { get; set; }

    public string? VehicleType { get; set; }

    public string? VehicleName { get; set; }

    public string? Model { get; set; }

    public string? Color { get; set; }

    public virtual ICollection<CustomerVehicleDetail> CustomerVehicleDetails { get; set; } = new List<CustomerVehicleDetail>();

    public virtual ICollection<VehicleBookingDetail> VehicleBookingDetails { get; set; } = new List<VehicleBookingDetail>();
}
