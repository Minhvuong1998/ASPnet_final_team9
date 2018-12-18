namespace Interconnected.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InterconnectedDb : DbContext
    {
        public InterconnectedDb()
            : base("name=InterconnectedDb")
        {
        }

        public virtual DbSet<ABOUT> ABOUTS { get; set; }
        public virtual DbSet<CATEGORy> CATEGORIES { get; set; }
        public virtual DbSet<COMMENT> COMMENTS { get; set; }
        public virtual DbSet<POST> POSTs { get; set; }
        public virtual DbSet<ROLE> ROLES { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<VOTE> VOTEs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CATEGORy>()
                .HasMany(e => e.POSTs)
                .WithRequired(e => e.CATEGORy)
                .HasForeignKey(e => e.ID_CATEGORY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<COMMENT>()
                .Property(e => e.DETAIL)
                .IsUnicode(false);

            modelBuilder.Entity<POST>()
                .HasMany(e => e.COMMENTS)
                .WithRequired(e => e.POST)
                .HasForeignKey(e => e.ID_POST)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<POST>()
                .HasMany(e => e.VOTEs)
                .WithRequired(e => e.POST)
                .HasForeignKey(e => e.ID_POST)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.USERS)
                .WithRequired(e => e.ROLE)
                .HasForeignKey(e => e.ID_ROLE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.COMMENTS)
                .WithRequired(e => e.USER)
                .HasForeignKey(e => e.ID_USER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.POSTs)
                .WithRequired(e => e.USER)
                .HasForeignKey(e => e.ID_USER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.VOTEs)
                .WithRequired(e => e.USER)
                .HasForeignKey(e => e.ID_USER)
                .WillCascadeOnDelete(false);
        }
    }
}
