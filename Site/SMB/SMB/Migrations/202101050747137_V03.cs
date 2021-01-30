namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V03 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        UrlParameter = c.String(),
                        ImageUrl = c.String(),
                        Summery = c.String(),
                        Body = c.String(nullable: false, storeType: "ntext"),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Services", "ServiceGroupId", c => c.Guid());
            CreateIndex("dbo.Services", "ServiceGroupId");
            AddForeignKey("dbo.Services", "ServiceGroupId", "dbo.ServiceGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "ServiceGroupId", "dbo.ServiceGroups");
            DropIndex("dbo.Services", new[] { "ServiceGroupId" });
            DropColumn("dbo.Services", "ServiceGroupId");
            DropTable("dbo.ServiceGroups");
        }
    }
}
