using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Models.Group
{
    public class GroupServiceGM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsAdmin { get; set; }
        public string MediaUrl { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
