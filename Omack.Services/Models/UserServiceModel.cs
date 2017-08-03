using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Models
{
    public class UserServiceModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int? MediaId { get; set; }      
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        public Media Media { get; set; }
        public ICollection<Group_User> Group_Users { get; set; }
    }
}
