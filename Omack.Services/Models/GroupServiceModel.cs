using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Models
{
    public class GroupServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Boolean IsActive { get; set; }
        public int MediaId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
