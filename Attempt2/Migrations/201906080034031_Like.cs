namespace Attempt2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Like : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikedDate = c.DateTime(nullable: false),
                        Liked = c.Boolean(nullable: false),
                        ImageId = c.String(),
                        UserId = c.String(maxLength: 128),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Image_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "Image_Id", "dbo.Images");
            DropIndex("dbo.Likes", new[] { "Image_Id" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropTable("dbo.Likes");
        }
    }
}
