﻿using System.Text.Json;

namespace ShippingDocuments.Infrastructure.Whs.Models
{
    public class MngrOrder
    {
        public string Документ_Id { get; set; } = null!;
        public string? Документ_Name { get; set; }
        public string Распоряжение_Id { get; set; } = null!;
        public string? Распоряжение_Name { get; set; }

        public static MngrOrder[]? From(string mngrOrderStr)
        {            
            return JsonSerializer.Deserialize<MngrOrder[]>(mngrOrderStr);
        }
    }
}
