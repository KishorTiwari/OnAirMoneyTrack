using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Core.Constants
{
    public static class ErrorMessage
    {
        //fetching
        public static string Get { get; set; } = "Sorry. Something went wrong when fetching data from server. Please try again.";
        public static string GetUnAuth { get; set; } = "Sorry. Either you're not authorized to view this data or it does not exists.";

        //adding
        public static string Add { get; set; } = "Sorry. Something went wrong when writing data to the server. Please try again.";

        //delete
        public static string Delete { get; set; } = "Sorry. Something went wrong when deleting this entity.";
        public static string DeleteUnAuth { get; set; } = "Sorry. Either you're not authorized to delete this entity or it does not exists.";

        //update
        public static string Update { get; set; } = "Sorry. Something went wrong when updating this entity.";
        public static string UpdateUnAuth { get; set; } = "Sorry. Either you're not authorized to update this entity or it does not exists.";

        //no data
        public static string NoData { get; set; } = "Sorry. There is no data to return.";
    }
}
