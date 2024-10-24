using Library.Domain;
using Library.DataAccess.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
       public class BookConfiguration : IEntityTypeConfiguration<Book>
       {
              public void Configure(EntityTypeBuilder<Book> builder)
              {
                     builder.Property(p => p.Title)
                            .IsRequired()
                            .HasMaxLength(EntityConfigurationRestricts.MaxTitleLength);

                     builder.Property(p => p.Description)
                            .IsRequired()
                            .HasMaxLength(EntityConfigurationRestricts.MaxDescriptionLength);
              }
       }
}
