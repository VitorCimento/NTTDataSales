﻿using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Mappings.DTO.Sale;

public class UpdateSaleDTO
{
    [Required, DataType(DataType.DateTime)]
    public DateTime DATE { get; set; } = DateTime.Now;

    [Required]
    public decimal TOTALVALUE { get; set; } = 0;

    [Required]
    public decimal TOTALDISCOUNT { get; set; } = 0;

    [Required]
    public bool SALECANCELLED { get; set; } = false;

    [Required]
    public int CUSTOMERID { get; set; }

    [Required]
    public int BRANCHID { get; set; }
}
