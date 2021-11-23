namespace LivroCaixa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Movimento");
            AlterColumn("dbo.Movimento", "IdMovimento", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Movimento", "IdMovimento");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Movimento");
            AlterColumn("dbo.Movimento", "IdMovimento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddPrimaryKey("dbo.Movimento", "IdMovimento");
        }
    }
}
