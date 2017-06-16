using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Data.Models
{
    public class User : IdentityUser<int>
    {
        public int MediaId {get; set;}

        //Navigation Properties
        public Media Media { get; set; }

        //System Properties  [Note: UpdatedBy & CreatedBy = Current Loggedin User ID]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
