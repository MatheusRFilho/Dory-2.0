namespace Dory2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alteracao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.res_responsavel", "res_email", c => c.String(unicode: false));
            AlterColumn("dbo.res_responsavel", "res_senha", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.res_responsavel", "res_senha", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.res_responsavel", "res_email", c => c.String(nullable: false, maxLength: 200, storeType: "nvarchar"));
        }
    }
}
