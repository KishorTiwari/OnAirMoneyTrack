using Omack.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Models
{
    public class MediaServiceModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public Boolean IsActive { get; set; }
        public string Url { get; set; }
        public MediaType TypeId { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
