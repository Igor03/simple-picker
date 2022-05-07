using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace JIgor.Projects.SimplePicker.Api.Entities
{
    public partial class EventValue
    {
        internal class EntityConfiguration : IEntityTypeConfiguration<EventValue>
        {
            public void Configure(EntityTypeBuilder<EventValue> builder)
            {
                _ = builder ?? throw new ArgumentNullException(nameof(builder));

                _ = builder.ToTable("EVENT_VALUE").HasKey(pk => pk.Id);

                _ = builder.Property(p => p.Id)
                    .HasColumnName("ID")
                    .HasColumnType("UNIQUEIDENTIFIER")
                    .HasDefaultValueSql("NEWID()")
                    .IsRequired();

                _ = builder.Property(p => p.EventId)
                    .HasColumnName("EVENT_ID")
                    .HasColumnType("UNIQUEIDENTIFIER")
                    .IsRequired();

                _ = builder.Property(p => p.Value)
                    .HasColumnName("VALUE")
                    .HasColumnType("VARCHAR(MAX)")
                    .IsRequired();

                _ = builder.Property(p => p.IsPicked)
                    .HasColumnName("IS_PICKED")
                    .HasColumnType("BIT")
                    .IsRequired();

                _ = builder.HasOne<Event>().WithMany(p => p.EventValues)
                    .HasForeignKey(p => p.EventId);
            }
        }
    }
}