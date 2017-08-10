using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omack.Data.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; } // "{user name} has paid {amount}."
        public Boolean IsActive { get; set; }
        public int Type { get; set; } //payment, someone purchase 
        //Foreign Keys
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
        
        
        //Nav properties
        public Group Group { get; set; }
        public User User { get; set; }

        //System Properties  [Note: UpdatedBy & CreatedBy = Current Loggedin User ID]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
