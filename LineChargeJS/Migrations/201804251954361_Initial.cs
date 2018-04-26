namespace LineChargeJS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Charge",
                c => new
                    {
                        ChargeID = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        ChargeTypeID = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChargeID)
                .ForeignKey("dbo.ChargeType", t => t.ChargeTypeID, cascadeDelete: true)
                .Index(t => t.ChargeTypeID);
            
            CreateTable(
                "dbo.ChargeType",
                c => new
                    {
                        ChargeTypeID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ChargeTypeID);

            Sql("INSERT INTO ChargeType(Type) Values('Deposit')");
            Sql("INSERT INTO ChargeType(Type) Values('Fee')");
            Sql("INSERT INTO ChargeType(Type) Values('Expense')");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Charge", "ChargeTypeID", "dbo.ChargeType");
            DropIndex("dbo.Charge", new[] { "ChargeTypeID" });
            DropTable("dbo.ChargeType");
            DropTable("dbo.Charge");
        }
    }
}
