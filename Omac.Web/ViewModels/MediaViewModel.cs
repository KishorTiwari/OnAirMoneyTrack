using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omack.Web.ViewModels
{
    public class MediaViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Guid Guid { get; set; }
        public Boolean IsActive { get; set; }

        //System Properties  [Note: UpdatedBy & CreatedBy = Current Loggedin User ID]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
