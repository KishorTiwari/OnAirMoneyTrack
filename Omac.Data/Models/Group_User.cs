using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omack.Data.Models
{
    public class Group_User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Boolean IsActive { get; set; }
        [Required]
        public Boolean IsAdmin { get; set; }

        //Foreign Keys
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int UserId { get; set; }

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
