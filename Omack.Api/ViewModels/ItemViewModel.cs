using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Omack.Api.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime DateOfPurchase { get; set; }

        [Required]
        public int ItemType { get; set; }

        public Boolean IsActive { get; set; }
    }
}
