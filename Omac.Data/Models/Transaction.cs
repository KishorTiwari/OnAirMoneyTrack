using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omack.Data.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int TypeId { get; set; } //[Private Or Public]
        public int? ReceiverId { get; set; }  
        public int? SenderId { get; set; }
        [Required]
        public Boolean  IsComplete { get; set; }
        [Required]
        public Boolean IsActive { get; set; }

        //Foreign Keys
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }

        //Navigation Properties
        public User User { get; set; }
        public Group Group { get; set; }

        //System Properties  [Note: UpdatedBy & CreatedBy = Current Loggedin User ID]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
//Current User Kamal
//To pay
// 1. Suman Amount: $50 . [Button: Pay] //receiverid = 46 [Suman]  , senderid = null
// 2. Kishor Amount: $40 . [Button: Pay]
// 3. Prashant Amount: $30 . [Button: Pay]

//To be paid
// 1. Ram Amount: $50 . [Button: Paid] // receiverid = null, senderid = 45[Ram] 
// 2. Shyam Amount: $40 . [Button: Paid]
// 3. Hari Amount: $30 . [Button: Paid]
