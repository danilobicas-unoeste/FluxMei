namespace LivroCaixa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nvo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mei",
                c => new
                {
                    IdMei = c.Int(nullable: false, identity: true),
                    Login = c.String(maxLength: 100),
                    Senha = c.String(),
                    NomeEmpresa = c.String(nullable: false, maxLength: 100),
                    Logradouto = c.String(maxLength: 100),
                    Cnpj = c.String(nullable: false, maxLength: 14),
                    NomeProprietario = c.String(nullable: false, maxLength: 100),
                    Telefone = c.String(maxLength: 15),
                })
                .PrimaryKey(t => t.IdMei);

            CreateTable(
                "dbo.Movimento",
                c => new
                {
                    IdMovimento = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Descicao = c.String(),
                    Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Data = c.DateTime(nullable: false),
                    Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TipoMovimentoId = c.Int(nullable: false),
                    IdMei = c.Int(nullable: false),
                    userName = c.String(),
                })
                .PrimaryKey(t => t.IdMovimento)
                .ForeignKey("dbo.Mei", t => t.IdMei)
                .ForeignKey("dbo.TipoMovimento", t => t.TipoMovimentoId)
                .Index(t => t.TipoMovimentoId)
                .Index(t => t.IdMei);

            CreateTable(
                "dbo.TipoMovimento",
                c => new
                {
                    tipoid = c.Int(nullable: false, identity: true),
                    descricao = c.String(nullable: false, maxLength: 50),
                    receitadespesa = c.String(nullable: false, maxLength: 1),
                    IdMei = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.tipoid)
                .ForeignKey("dbo.Mei", t => t.IdMei)
                .Index(t => t.IdMei);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    IdMei = c.Int(nullable: false),
                    Nome = c.String(nullable: false, maxLength: 100),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Movimento", "TipoMovimentoId", "dbo.TipoMovimento");
            DropForeignKey("dbo.TipoMovimento", "IdMei", "dbo.Mei");
            DropForeignKey("dbo.Movimento", "IdMei", "dbo.Mei");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TipoMovimento", new[] { "IdMei" });
            DropIndex("dbo.Movimento", new[] { "IdMei" });
            DropIndex("dbo.Movimento", new[] { "TipoMovimentoId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TipoMovimento");
            DropTable("dbo.Movimento");
            DropTable("dbo.Mei");
        }
    }
}
