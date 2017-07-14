using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Omack.Data.LogModels
{
    public class ApiLog
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Application { get; set; }
        public DateTime Logged { get; set; }
        [MaxLength(50)]
        public string Level { get; set; }
        [MaxLength(500)]
        public string Message { get; set; }
        [MaxLength(500)]
        public string Logger { get; set; }
        [MaxLength(500)]
        public string CallSite { get; set; }
        [MaxLength(500)]
        public string Exception { get; set; }
        [MaxLength(10)]
        public string ThreadId { get; set; }
        [MaxLength(50)]
        public string MachineName { get; set; }
    }
    public class WebLog
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Application { get; set; }
        public DateTime Logged { get; set; }
        [MaxLength(50)]
        public string Level { get; set; }
        [MaxLength(500)]
        public string Message { get; set; }
        [MaxLength(500)]
        public string Logger { get; set; }
        [MaxLength(500)]
        public string CallSite { get; set; }
        [MaxLength(500)]
        public string Exception { get; set; }
        [MaxLength(10)]
        public string ThreadId { get; set; }
        [MaxLength(50)]
        public string MachineName { get; set; }
    }
}
