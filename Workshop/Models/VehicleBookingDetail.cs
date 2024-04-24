using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workshop.Models;

public partial class VehicleBookingDetail
{
    public int BookingId { get; set; }
    public int? CustomerId { get; set; }
    public int? VehicleId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BookingDate { get; set; }
    public TimeSpan BookingTime { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public virtual CustomerDetail? Customer { get; set; }
    public virtual VehicleDetail? Vehicle { get; set; }
}
