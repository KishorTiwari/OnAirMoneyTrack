using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Omack.Api.ViewModels
{
    public class GroupVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsActive { get; set; }
        public int? MediaId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
