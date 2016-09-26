namespace NochDAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserDomains
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserDomainID { get; set; }

        public int UserID { get; set; }

        public int DomainID { get; set; }

        public virtual Domains Domains { get; set; }

        public virtual Users Users { get; set; }
    }
}
