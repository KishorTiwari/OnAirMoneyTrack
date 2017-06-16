using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Omack.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Omack.Core.Models
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
