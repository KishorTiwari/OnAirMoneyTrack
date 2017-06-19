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
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsAdmin { get; set; }

        //Navigation Properties
        public Group Group { get; set; }
        public User User { get; set; }

        //System Properties  [Note: UpdatedBy & CreatedBy = Current Loggedin User ID]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
