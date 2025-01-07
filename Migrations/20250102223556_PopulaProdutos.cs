using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Calça Hugo Boss', 100.50,1)");
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Sapato Hugo Boss', 400.50,2)");
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Cinto Hubo Boss', 900.50,3)");
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Camisa Hugo Boss', 810.50,4)");
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Carteira Hugo Boss', 210.50,5)");
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Tênis Lacoste', 610.50,6)");
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Tênis Hugo Boss', 910.50,7)");
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Camisa Lacoste', 110.50,8)");
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Calça Lacoste', 210.50,9)");
            mb.Sql("Insert into Produtos (Nome,Preco,IdVendedor) Values('Cinto Lacoste', 510.50,10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
