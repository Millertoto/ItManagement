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

        public virtual DbSet<Accesory> Accesories { get; set; }
        public virtual DbSet<Computer> Computers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<SmartBoard> SmartBoards { get; set; }
        public virtual DbSet<SmartPhone> SmartPhones { get; set; }
        public virtual DbSet<Tablet> Tablets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accesory>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Errors)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.Cpr)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Errors1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.HasRepaired);

            modelBuilder.Entity<Equipment>()
                .HasOptional(e => e.Computer)
                .WithRequired(e => e.Equipment);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Errors)
                .WithRequired(e => e.Equipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .HasOptional(e => e.SmartBoard)
                .WithRequired(e => e.Equipment);

            modelBuilder.Entity<Equipment>()
                .HasOptional(e => e.SmartPhone)
                .WithRequired(e => e.Equipment);

            modelBuilder.Entity<Equipment>()
                .HasOptional(e => e.Tablet)
                .WithRequired(e => e.Equipment);

            modelBuilder.Entity<Error>()
                .Property(e => e.ErrorMessage)
                .IsUnicode(false);
        }
    }
}
