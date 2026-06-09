using Microsoft.EntityFrameworkCore;
using Normative.Models;
using Normative.Models.Config;
using Normative.Models.Screen.Settings;
using Normative.Models.STD.Pipe;
using Normative.Models.Table;
using Normative.Models.View;

namespace Normative.Data;

public partial class NormativeContext(DbContextOptions<NormativeContext> options) : DbContext(options)
{
    public DbSet<RolePermission> RolePermission { get; set; }
    public DbSet<Permissions> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }

    public virtual DbSet<Operation> Operation { get; set; }

    public virtual DbSet<OperationStep> OperationStep { get; set; }

    public virtual DbSet<ProductLine> ProductLine { get; set; }

    public virtual DbSet<ProductSize> ProductSize { get; set; }

    public virtual DbSet<ProductType> ProductType { get; set; }

    public virtual DbSet<V_VTC_Pipe_OPSQ_20> V_VTC_PipeOperation20 { get; set; }

    public virtual DbSet<V_VTC_Pipe_OPSQ_30> V_VTC_PipeOperation30 { get; set; }

    public virtual DbSet<V_VTC_Pipe_20_30> V_VTC_PipeOperation_20_30 { get; set; }

    public virtual DbSet<V_VTC_InnerVessel_OPSQ_10> V_VTC_InnerVessel_OPSQ_10 { get; set; }

    public virtual DbSet<V_VTC_InnerVessel_OPSQ_20> V_VTC_InnerVessel_OPSQ_20 { get; set; }

    public virtual DbSet<V_VTC_InnerVessel_OPSQ_40> V_VTC_InnerVessel_OPSQ_40 { get; set; }

    public virtual DbSet<V_VTC_OutherVessel_OPSQ_10> V_VTC_OutherVessel_OPSQ_10 { get; set; }

    public virtual DbSet<V_VTC_OutherVessel_OPSQ_20> V_VTC_OutherVessel_OPSQ_20 { get; set; }

    public virtual DbSet<V_VTC_OutherVessel_OPSQ_40> V_VTC_OutherVessel_OPSQ_40 { get; set; }

    public virtual DbSet<Preparation> Preparations { get; set; }

    public virtual DbSet<PreparationType> PreparationType { get; set; }

    public virtual DbSet<V_Preparation> V_Preparation { get; set; }
    public virtual DbSet<ViewUsersRoles> ViewUsersRoles { get; set; }
    public virtual DbSet<ViewPermission> ViewPermission { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // PostgreSQL: schéma "cfg" — lowercase je standard, ale funguje i PascalCase pokud ho vytvoříš stejně v DB
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<Permissions>(entity =>
        {
            entity.ToTable("Permissions", schema: "cfg");
            entity.HasKey(e => e.PermissionId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles", schema: "cfg");
            entity.HasKey(d => d.RoleId);
            entity.Property(d => d.Name).HasMaxLength(100);
            entity.Property(d => d.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("RolePermissions", schema: "cfg");
            entity.HasKey(e => e.Id);

            entity.HasOne(c => c.Permission)
                .WithMany(c => c.Roles)
                .HasForeignKey(d => d.PermissionId)
                // PostgreSQL nepodporuje DeleteBehavior.NoAction stejně jako MSSQL —
                // použij Restrict, které má stejný efekt (zamítne delete pokud existují závislé záznamy)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Role)
                .WithMany(c => c.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", schema: "cfg");
            entity.HasKey(d => d.UserId);
            entity.Property(d => d.UserName).HasMaxLength(20);
            entity.Property(d => d.DisplayName).HasMaxLength(255);
            entity.Property(d => d.Email).HasMaxLength(320);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRoles", schema: "cfg");

            entity.HasOne(c => c.User)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Role)
                .WithMany(c => c.UsersWithRole)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ViewUsersRoles>(entity =>
        {
            entity.ToView("V_UsersRoles").HasNoKey();
        });

        modelBuilder.Entity<ViewPermission>(entity =>
        {
            entity.ToView("V_ViewPermission").HasNoKey();
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            // PostgreSQL generuje vlastní názvy PK — HasName() je volitelné,
            // ale lze ponechat pro konzistenci pokud migration pojmenování řídíš ručně

            entity.ToTable("Operation", schema: "cfg");

            entity.HasKey(e => e.OperationId).HasName("PK_Operation");

            entity.HasOne(d => d.ProductLine)
                .WithMany(p => p.Operation)
                .HasConstraintName("FK_Operation_ProductLine")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.ProductType)
                .WithMany(p => p.Operation)
                .HasConstraintName("FK_Operation_ProductType")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OperationStep>(entity =>
        {
            entity.ToTable("OperationStep", schema: "cfg");

            entity.HasKey(e => e.OperationStepId).HasName("PK_OperationStep");

            entity.HasOne(d => d.Operation)
                .WithMany(p => p.OperationStep)
                .HasConstraintName("FK_OperationStep_Operation")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.ProductSize)
                .WithMany(p => p.OperationStep)
                .HasConstraintName("FK_OperationStep_ProductSize")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ProductLine>(entity =>
        {
            entity.ToTable("ProductLine", schema: "cfg");
            entity.HasKey(e => e.ProductLineId).HasName("PK_ProductLine");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.ToTable("ProductSize", schema: "cfg");
            entity.HasKey(e => e.ProductSizeId).HasName("PK_ProductSize");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.ToTable("ProductType", schema: "cfg");
            entity.HasKey(e => e.ProductTypeId).HasName("PK_ProductType");
        });

        modelBuilder.Entity<V_Preparation>(entity =>
        {
            entity.ToView("v_Preparation");
        });

        modelBuilder.Entity<V_VTC_Pipe_OPSQ_20>(entity =>
        {
            entity.ToView("v_VTC_Pipe_OPSQ_20");
        });

        modelBuilder.Entity<V_VTC_Pipe_OPSQ_30>(entity =>
        {
            entity.ToView("v_VTC_Pipe_OPSQ_30");
        });

        modelBuilder.Entity<V_VTC_Pipe_20_30>(entity =>
        {
            entity.ToView("v_VTC_Pipe_20_30");
        });

        modelBuilder.Entity<V_VTC_InnerVessel_OPSQ_10>(entity =>
        {
            entity.ToView("v_VTC_InnerVessel_OPSQ_10");
        });

        modelBuilder.Entity<V_VTC_InnerVessel_OPSQ_20>(entity =>
        {
            entity.ToView("v_VTC_InnerVessel_OPSQ_20");
        });

        modelBuilder.Entity<V_VTC_InnerVessel_OPSQ_40>(entity =>
        {
            entity.ToView("v_VTC_InnerVessel_OPSQ_40");
        });

        modelBuilder.Entity<V_VTC_OutherVessel_OPSQ_10>(entity =>
        {
            entity.ToView("v_VTC_OutherVessel_OPSQ_10");
        });

        modelBuilder.Entity<V_VTC_OutherVessel_OPSQ_20>(entity =>
        {
            entity.ToView("v_VTC_OutherVessel_OPSQ_20");
        });

        modelBuilder.Entity<V_VTC_OutherVessel_OPSQ_40>(entity =>
        {
            entity.ToView("v_VTC_OutherVessel_OPSQ_40");
        });

        modelBuilder.Entity<Preparation>(entity =>
        {
            entity.ToTable("Preparation", schema: "cfg");
            // numeric(18,4) — PostgreSQL ekvivalent decimal(18,4), HasPrecision funguje stejně
            entity.Property("Fitter").HasPrecision(18, 4);
            entity.Property("Welder").HasPrecision(18, 4);
            entity.Property("ProductSizeId").IsRequired(true);
            entity.Property("PreparationTypeId").IsRequired(true);
        });

        modelBuilder.Entity<PreparationType>(entity =>
        {
            entity.ToTable("PreparationType", schema: "cfg");
            entity.Property("Name").HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);

        ModelBuilderExtensions.SeedData(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}