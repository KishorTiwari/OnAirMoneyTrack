using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Core.Models
{
    public class Result<T> where T: class 
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
