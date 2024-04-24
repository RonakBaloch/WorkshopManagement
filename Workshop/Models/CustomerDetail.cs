using System;
using System.Collections.Generic;

namespace Workshop.Models;

public partial class CustomerDetail
{
    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public string? MobileNo { get; set; }

    public string? EmailId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<CustomerVehicleDetail> CustomerVehicleDetails { get; set; } = new List<CustomerVehicleDetail>();

    public virtual ICollection<VehicleBookingDetail> VehicleBookingDetails { get; set; } = new List<VehicleBookingDetail>();
}
