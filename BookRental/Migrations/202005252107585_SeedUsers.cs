namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'258f044a-8a34-4d7a-89f2-280e7b704a5f', N'ommalor@gmail.com', 0, N'AHmA7i3GMCl+aydSpz/nXNGVBm4hQbVNUPT+3X838VbdOixEEELni3w5YZbZBE+Qzg==', N'e8f60ad1-e6a9-4fa3-af0c-0366bf5626a8', NULL, 0, 0, NULL, 1, 0, N'ommalor@gmail.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'61f6fa0e-790b-481a-972f-27f82245214c', N'admin@gmail.com', 0, N'ADdfNDhae8SOc2owhdugwYgcKEOEtiBEoCWtvbN2B19zTwMUnzf6Zj9XBqWnGmvLPg==', N'3bd05e8a-85d6-4f6e-9d76-717616ba46b7', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'fbf2eb5a-8973-4ffe-bbf1-d12f6db5d327', N'CanManageBooks')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'61f6fa0e-790b-481a-972f-27f82245214c', N'fbf2eb5a-8973-4ffe-bbf1-d12f6db5d327')

                ");
        }
        
        public override void Down()
        {
        }
    }
}
