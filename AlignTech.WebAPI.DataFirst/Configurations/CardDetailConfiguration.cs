using AlignTech.WebAPI.DataFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlignTech.WebAPI.DataFirst.Configurations
{
    public class CardDetailConfiguration : IEntityTypeConfiguration<CardDetail>
    {
        public void Configure(EntityTypeBuilder<CardDetail> entity)
        {
            entity.HasKey(e => e.CardNumber).HasName("pk_CardNumber");

            entity.Property(e => e.CardNumber).HasColumnType("numeric(16, 0)");
            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CardType)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Cvvnumber)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("CVVNumber");
            entity.Property(e => e.NameOnCard)
                .HasMaxLength(40)
                .IsUnicode(false);
        }
    }
}
