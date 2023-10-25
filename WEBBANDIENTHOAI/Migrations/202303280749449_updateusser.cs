namespace WEBBANDIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "User_id", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "User_id");
        }
    }
}
