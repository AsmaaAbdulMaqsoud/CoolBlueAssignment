using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.DTO;

public class SurchargeRateDto
{
    public int ProductTypeId { get; set; }
    public decimal SurchargeRate { get; set; }
}
