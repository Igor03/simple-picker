using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace JIgor.Projects.SimplePicker.Api.Entities
{
    public partial class Event
    {
        internal class EntityConfiguration : IEntityTypeConfiguration<Event>
        {
            public void Configure(EntityTypeBuilder<Event> builder)
            {
                _ = builder ?? throw new ArgumentNullException(nameof(builder));

                _ = builder.ToTable("EVENT").HasKey(pk => pk.Id);

                _ = builder.Property(p => p.Id)
                    .HasColumnName("ID")
                    .HasColumnType("UNIQUEIDENTIFIER")
                    .HasDefaultValueSql("NEWID()")
                    .IsRequired();

                _ = builder.Property(p => p.Title)
                    .HasColumnName("TITLE")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(50)
                    .IsRequired();

                _ = builder.Property(p => p.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(255);

                _ = builder.Property(p => p.StartDate)
                    .HasColumnName("START_DATE")
                    .HasColumnType("DATETIME")
                    .IsRequired();

                _ = builder.Property(p => p.DueDate)
                    .HasColumnName("DUE_DATE")
                    .HasColumnType("DATETIME")
                    .IsRequired();

                _ = builder.Property(p => p.IsFinished)
                    .HasColumnName("IS_FINISHED")
                    .HasColumnType("BIT")
                    .IsRequired();
            }
        }
    }
}