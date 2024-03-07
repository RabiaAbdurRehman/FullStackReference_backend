using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostingAPI.Models.Dto;

namespace PostingAPI.Data
{
    public class PostingDetailsConfig : IEntityTypeConfiguration<PostingDetails>
    {
        public void Configure(EntityTypeBuilder<PostingDetails> builder)
        {
            builder.ToTable("PostingDetails");
            builder.HasKey(x => x.PostingDetailsId);
            builder.Property(x => x.PostingDetailsId).UseIdentityColumn();

            builder.Property(n => n.LikeStatus).IsRequired(false);
            builder.Property(n => n.ShareStatus).HasMaxLength(250).IsRequired(false);
            builder.Property(n => n.EntryDate).IsRequired();
            builder.Property(n => n.UserId).IsRequired(false);

            builder.HasOne(n => n.Posting)
                .WithMany(n => n.PostDetails)
                .HasForeignKey(n => n.PostingId)
                .HasConstraintName("FK_POSTING_ID");
            

        }
    }
}
