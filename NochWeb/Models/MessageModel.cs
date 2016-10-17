using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security;
using System.Web;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NochWeb.Models
{
    public class MessageModel
    {
        [Key]
        public int MessageID { get; set; }
        public int ChannelID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsEdited { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}