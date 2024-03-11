using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplicationEfCore.Models.DataModel;

namespace WebApplicationEfCore.Models;

public partial class ApiDbContext : DbContext
{
    public ApiDbContext()
    {
    }

    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }
    public DbSet<Users> users { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-G0OQTNQ;Database=EfCoreDB;User Id=sa;Password=P@ssw0rd;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AI");

        modelBuilder.HasDefaultSchema("EfCoreDB");

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.FirstName).HasColumnType("nvarchar(50)");
            entity.Property(entity => entity.LastName).HasColumnType("nvarchar(50)");
            entity.Property(entity => entity.DepartmentId).HasColumnType("int");
            entity.Property(entity => entity.IsActive).HasColumnType("bit");
            entity.Property(entity => entity.CreatedDate).HasColumnType("datetime");
            entity.Property(entity => entity.UpdatedDate).HasColumnType("datetime");
            entity.ToTable("Users", "dbo");
        });

        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
