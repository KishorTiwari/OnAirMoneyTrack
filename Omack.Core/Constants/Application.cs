using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Core.Constants
{
    public static class Application
    {
        public static string Name = "Omack";

        public static DateTime CurrentDate
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
        public static int DefaultMediaId { get; set; } = 2;
    }
}
