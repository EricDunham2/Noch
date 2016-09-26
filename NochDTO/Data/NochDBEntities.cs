namespace NochDAL.Data
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class NochDBEntities : DbContext
	{
		public NochDBEntities()
			: base("name=NochDBEntities")
		{
		}

		public NochDBEntities(string connectionString) : base(connectionString) { }

		public virtual DbSet<Channels> Channels { get; set; }
		public virtual DbSet<Domains> Domains { get; set; }
		public virtual DbSet<Messages> Messages { get; set; }
		public virtual DbSet<UserDomains> UserDomains { get; set; }
		public virtual DbSet<Users> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Channels>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Channels>()
				.HasMany(e => e.Messages)
				.WithRequired(e => e.Channels)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Domains>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Domains>()
				.HasMany(e => e.Channels)
				.WithRequired(e => e.Domains)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Domains>()
				.HasMany(e => e.UserDomains)
				.WithRequired(e => e.Domains)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Messages>()
				.Property(e => e.Content)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.FirstName)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.LastName)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.Username)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.Password)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.Email)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.Phone)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.Address)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.City)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.Province)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.Country)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.Property(e => e.PostalCode)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.HasMany(e => e.Messages)
				.WithRequired(e => e.Users)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Users>()
				.HasMany(e => e.UserDomains)
				.WithRequired(e => e.Users)
				.WillCascadeOnDelete(false);
		}
	}
}
