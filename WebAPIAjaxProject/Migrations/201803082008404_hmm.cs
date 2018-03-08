namespace WebAPIAjaxProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hmm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmployeeID = c.Int(nullable: false),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Departments", new[] { "EmployeeID" });
            DropTable("dbo.Departments");
        }
    }
}
