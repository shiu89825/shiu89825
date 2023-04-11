using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categorys { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-JLPU7283;Database=shopDB;User Id=sa;Password=12345678;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categorys");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Categorys)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categorys");
            entity.Property(e => e.Order).HasColumnName("order");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("member");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Account)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Passwd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("passwd");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Contents)
                .IsUnicode(false)
                .HasColumnName("contents");
            entity.Property(e => e.Img)
                .IsUnicode(false)
                .HasColumnName("img");
            entity.Property(e => e.PAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("p_account");
            entity.Property(e => e.PName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("p_name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
