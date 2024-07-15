using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Model;
using Entities.Model.AnyClasses;
using System.Runtime.Intrinsics.X86;

namespace Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    id = 1,
                    user_type_id = 1,
                    login = "family@admin.family",
                    password = PasswordManager.EncryptPassword(("family@admin.familyfamily").ToString()),
                    
                }
                );
            builder.HasIndex(u => u.login).IsUnique();


        }

    }

}
