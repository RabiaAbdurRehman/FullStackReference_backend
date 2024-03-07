using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostingAPI.Models;

namespace PostingAPI.Data
{
    public class PostingConfig : IEntityTypeConfiguration<Posting>
    {
        public void Configure(EntityTypeBuilder<Posting> builder)
        {
            builder.ToTable("Posting");
            builder.HasKey(x => x.PostingId);
            builder.Property(x => x.PostingId).UseIdentityColumn();

            builder.Property(n=>n.UserId).IsRequired();
            builder.Property(n => n.PostContent).HasMaxLength(2500).IsRequired(false);
            builder.Property(n => n.PostingDate).IsRequired();
            builder.Property(n => n.DeletionFlag).IsRequired(false);
        }
    }
}
