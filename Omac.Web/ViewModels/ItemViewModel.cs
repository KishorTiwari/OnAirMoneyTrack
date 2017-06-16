using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omack.Web.ViewModels
{
    public class ItemViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public decimal Price { get; set; }
        public DateTime DateOfPurchase { get; set; }
        [Required]
        public int ItemType { get; set; }
        public Boolean  IsActive { get; set; }
        
        //Foreign Keys
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int MediaId { get; set; }
       
        //System Properties  [Note: UpdatedBy & CreatedBy = Current Loggedin User ID]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
