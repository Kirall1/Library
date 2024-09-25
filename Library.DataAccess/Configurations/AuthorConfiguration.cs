﻿using Library.DataAccess.Configurations.Constants;
using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
       public class AuthorConfiguration : IEntityTypeConfiguration<Author>
       {
              public void Configure(EntityTypeBuilder<Author> builder)
              {
                     builder.Property(p => p.Name)
                            .IsRequired()
                            .HasMaxLength(EntityConfigurationRestricts.MaxAuthorNameLength);

                     builder.Property(p => p.Surname)
                            .IsRequired()
                            .HasMaxLength(EntityConfigurationRestricts.MaxAuthorNameLength);

                     builder.Property(p => p.BirthDate)
                            .IsRequired();

                     builder.Property(p => p.Country)
                            .IsRequired()
                            .HasMaxLength(EntityConfigurationRestricts.MaxCountryLength);
              }
       }
}