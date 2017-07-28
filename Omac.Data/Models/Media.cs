using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omack.Data.Models
{
    public class Media
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public Boolean IsActive { get; set; }

        //Nav Properties
        public Group Group { get; set; }
        public User User { get; set; }
        public Transaction Transaction { get; set; }
        public Item Item { get; set; }

        //System Properties  [Note: UpdatedBy & CreatedBy = Current Loggedin User ID]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
