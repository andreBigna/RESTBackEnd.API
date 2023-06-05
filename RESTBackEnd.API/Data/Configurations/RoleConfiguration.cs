using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTBackEnd.API.Common;

namespace RESTBackEnd.API.Data.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			builder.HasData(
				new IdentityRole()
				{
					Name = RoleNames.Administrator,
					NormalizedName = RoleNames.Administrator.ToUpper(),
				},
				new IdentityRole()
				{
					Name = RoleNames.User,
					NormalizedName = RoleNames.User.ToUpper()
				}
			);
		}
	}
}