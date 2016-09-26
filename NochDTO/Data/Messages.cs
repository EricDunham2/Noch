namespace NochDAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Messages
    {
        [Key]
        public int MessageID { get; set; }

        public int ChannelID { get; set; }

        public int UserID { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Timestamp { get; set; }

        public bool IsEdited { get; set; }

        public DateTime UpdatedOn { get; set; }

        public virtual Channels Channels { get; set; }

        public virtual Users Users { get; set; }
    }
}
