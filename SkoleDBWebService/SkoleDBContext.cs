namespace SkoleDBWebService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SkoleDBContext : DbContext
    {
        public SkoleDBContext()
            : base("name=SkoleDBContext")
        {
            base.Configuration.LazyLoadingEnabled = false;
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Accesories> Accesories { get; set; }
        public virtual DbSet<Computer> Computer { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Errors> Errors { get; set; }
        public virtual DbSet<SmartBoard> SmartBoard { get; set; }
        public virtual DbSet<SmartPhone> SmartPhone { get; set; }
        public virtual DbSet<Tablet> Tablet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accesories>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .HasOptional(e => e.Computer)
                .WithRequired(e => e.Equipment);

            modelBuilder.Entity<Equipment>()
                .HasOptional(e => e.SmartBoard)
                .WithRequired(e => e.Equipment);

            modelBuilder.Entity<Equipment>()
                .HasOptional(e => e.SmartPhone)
                .WithRequired(e => e.Equipment);

            modelBuilder.Entity<Equipment>()
                .HasOptional(e => e.Tablet)
                .WithRequired(e => e.Equipment);

            modelBuilder.Entity<Errors>()
                .Property(e => e.ErrorMessage)
                .IsUnicode(false);
        }
    }
}
