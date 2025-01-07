using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulaVendedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('João', 'Silva')");
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('Maria', 'Oliveira')");
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('Carlos', 'Santos')");
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('Ana', 'Costa')");
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('Pedro', 'Ferreira')");
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('Paula', 'Lima')");
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('Bruno', 'Mendes')");
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('Laura','Gomes')");
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('Fernando', 'Alves')");
            mb.Sql("Insert into Vendedores (Nome, Sobrenome) Values('Juliana', 'Pereira')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Vendedores");
        }
    }
}
