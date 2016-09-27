namespace NochDTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        ChannelID = c.Int(nullable: false, identity: true),
                        DomainID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChannelID)
                .ForeignKey("dbo.Domains", t => t.DomainID)
                .Index(t => t.DomainID);
            
            CreateTable(
                "dbo.Domains",
                c => new
                    {
                        DomainID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DomainID);
            
            CreateTable(
                "dbo.UserDomains",
                c => new
                    {
                        UserDomainID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        DomainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserDomainID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.Domains", t => t.DomainID)
                .Index(t => t.UserID)
                .Index(t => t.DomainID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 20, unicode: false),
                        LastName = c.String(maxLength: 50, unicode: false),
                        Username = c.String(maxLength: 20, unicode: false),
                        Password = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 50, unicode: false),
                        IsEmailConfirmed = c.Boolean(),
                        Phone = c.String(maxLength: 20, unicode: false),
                        Address = c.String(maxLength: 100, unicode: false),
                        City = c.String(maxLength: 50, unicode: false),
                        Province = c.String(maxLength: 50, unicode: false),
                        Country = c.String(maxLength: 50, unicode: false),
                        PostalCode = c.String(maxLength: 20, unicode: false),
                        IsAdmin = c.Boolean(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        ChannelID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Content = c.String(nullable: false, unicode: false),
                        Timestamp = c.DateTime(nullable: false),
                        IsEdited = c.Boolean(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.Channels", t => t.ChannelID)
                .Index(t => t.ChannelID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "ChannelID", "dbo.Channels");
            DropForeignKey("dbo.UserDomains", "DomainID", "dbo.Domains");
            DropForeignKey("dbo.UserDomains", "UserID", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserID", "dbo.Users");
            DropForeignKey("dbo.Channels", "DomainID", "dbo.Domains");
            DropIndex("dbo.Messages", new[] { "UserID" });
            DropIndex("dbo.Messages", new[] { "ChannelID" });
            DropIndex("dbo.UserDomains", new[] { "DomainID" });
            DropIndex("dbo.UserDomains", new[] { "UserID" });
            DropIndex("dbo.Channels", new[] { "DomainID" });
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.UserDomains");
            DropTable("dbo.Domains");
            DropTable("dbo.Channels");
        }
    }
}
