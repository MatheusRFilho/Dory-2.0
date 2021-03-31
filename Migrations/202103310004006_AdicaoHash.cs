namespace Dory2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicaoHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.res_responsavel", "pes_hash", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.res_responsavel", "pes_hash");
        }
    }
}
