using System;
using System.Collections.Generic;
using TalentoLocal.Models.enums;

namespace TalentoLocal.Models
{
    public class Convocation
    {
        
        public int Id { get; set; }

        public string title { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public DateTime? createTime { get; set; }

        public DateTime? deadline { get; set; }

        public ConvocationStatus state { get; set; }

        public string location { get; set; } = string.Empty;

        public int availablePlaces { get; set; }


    }
}
