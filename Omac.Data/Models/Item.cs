using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omack.Data.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public decimal? Price { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int? ItemType { get; set; }
        public Boolean  IsActive { get; set; }
        
        //Foreign Keys
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public int? MediaId { get; set; }
       
        //Navigation Properties
        public Group Group { get; set; }
        public Media Media { get; set; }
        public User User { get; set; }

        //System Properties  [Note: UpdatedBy & CreatedBy = Current Loggedin User ID]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
