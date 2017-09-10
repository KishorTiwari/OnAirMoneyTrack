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
        public int? MediaId { get; set; }
    }
}
