using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Models
{
    public class TransactionServiceModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int? ReceiverId { get; set; }
        public int? SenderId { get; set; }
        public Boolean IsComplete { get; set; }
        public Boolean IsActive { get; set; }

        public int UserId { get; set; }
        public int GroupId { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
