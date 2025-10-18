using System;
using System.Collections.Generic;

namespace TalentoLocal.Models
namespace TalentoLocal.Models.
{
    public class PublishingEntity
    {
        public int id { get; set; }
        public string name { get; set;} = string.Empty;

        public string type { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string phone { get; set; } = string.Empty;

        public string siteWeb { get; set; } = string.Empty;

        public string address { get; set; } = string.Empty;

        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}
