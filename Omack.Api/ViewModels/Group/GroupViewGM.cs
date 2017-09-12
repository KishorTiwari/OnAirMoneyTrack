using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omack.Api.ViewModels.Group
{
    public class GroupViewGM
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
