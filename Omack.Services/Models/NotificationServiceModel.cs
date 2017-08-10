using Omack.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Models
{
    public class NotificationServiceModel
    {
        public int Id { get; set; }
        public string Description { get; set; } 
        public Boolean IsActive { get; set; }
        public NotificationType Type { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
