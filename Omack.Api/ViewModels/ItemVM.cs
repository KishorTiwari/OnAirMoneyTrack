using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Omack.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Omack.Api.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime DateOfPurchase { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))] //serialize into enum string instead of value when making get request.
        public ItemType ItemType { get; set; }
    }
}
